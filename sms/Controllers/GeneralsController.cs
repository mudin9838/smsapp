using Syncfusion.EJ2.Base;
using System.Collections;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using sms.Models;
using sms.Data;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace sms.Controllers
{
  public class GeneralsController : Controller
  {

        private ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;



        public GeneralsController(ApplicationDbContext Context, UserManager<ApplicationUser> userManager)
        {
            this._context = Context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var siteid = user.ParentId;
            ViewBag.ParentId = _context.Parents.Where(x => x.ParentId == siteid).ToList();
            ViewBag.CarId = _context.Cars.ToList();
            ViewBag.TargaId = _context.Targas.ToList();
            ViewBag.DriverId = _context.Drivers.ToList();
            ViewBag.FuelId = _context.Fuels.ToList();
            //ViewBag.dataSource = _context.StockItems.ToList();
            ViewBag.dataSource = _context.Generals.Where(x => x.ParentId == siteid && x.User.Id == user.Id).ToList();
            return View();
        }
        public async Task<IActionResult> UrlDatasource([FromBody]DataManagerRequest dm)
        {

            var user = await _userManager.GetUserAsync(HttpContext.User);

            var siteid = user.ParentId;

            IEnumerable DataSource = _context.Generals.Where(x => x.ParentId == siteid && x.User.Id == user.Id).ToList();
            DataOperations operation = new DataOperations();   
            if (dm.Sorted != null && dm.Sorted.Count > 0) //Sorting
            {
               DataSource = operation.PerformSorting(DataSource, dm.Sorted);
            }
	       if (dm.Search != null && dm.Search.Count > 0)
           {
               DataSource = operation.PerformSearching(DataSource, dm.Search);  //Search
           }
			if (dm.Where != null && dm.Where.Count > 0) //Filtering
            {
			    DataSource = operation.PerformFiltering(DataSource, dm.Where, dm.Where[0].Operator);
            }
            int count = DataSource.Cast<General>().Count();
            if (dm.Skip != 0)//Paging
            {
                DataSource = operation.PerformSkip(DataSource, dm.Skip);         
            }
            if (dm.Take != 0)
            {
                DataSource = operation.PerformTake(DataSource, dm.Take);
            }
            return dm.RequiresCounts ? Json(new { result = DataSource, count = count }) : Json(DataSource);
        }
        public async Task<IActionResult> Insert([FromBody]CRUDModel<General>  value)
        {
            //do stuff
            var user = await _userManager.GetUserAsync(HttpContext.User);
            value.Value.User = user;
            _context.Generals.Add(value.Value);
            _context.SaveChanges();
            string msg = "በትክክል መዝግበዋል!!";   //Message from server 
            return Json(new { data = value, message = msg });

        }
        public ActionResult Update([FromBody]CRUDModel<General>  value)
        {
            //do stuff
			var ord = value;

           General val = _context.Generals.Where(or => or.GeneralId == ord.Value.GeneralId).FirstOrDefault();
            val.GeneralId=ord.Value.GeneralId;
            val.ParentId=ord.Value.ParentId;
            val.CarId=ord.Value.CarId;
            val.TargaId=ord.Value.TargaId;
            val.DriverId=ord.Value.DriverId;
            val.RegisteredDate=ord.Value.RegisteredDate;
            val.FuelId=ord.Value.FuelId;
            //val.Car=ord.Value.Car;
            //val.Driver=ord.Value.Driver;
            //val.Fuel=ord.Value.Fuel;
            //val.Parent=ord.Value.Parent;
            //val.Targa=ord.Value.Targa;
            //val.Litres=ord.Value.Litres;
            //val.User=ord.Value.User;
            _context.SaveChanges();
            return Json(value);
        }
        public ActionResult Delete([FromBody]CRUDModel<General> value)
        {
            //do stuff
            var product = _context.Generals.FirstOrDefault(m => m.GeneralId == int.Parse(value.Key.ToString()));

            _context.Generals.Remove(product);
            _context.SaveChanges();
            string msg = "መረጃው በትክክል ተሰርዞዋል!!";   //Message from server 
            return Json(new { data = product, message = msg });
        }
   }
}
