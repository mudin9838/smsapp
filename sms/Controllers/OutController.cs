using Syncfusion.EJ2.Base;
using System.Collections;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using sms.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Policy;
using sms.Data;

namespace sms.Controllers
{
  public class OutController : Controller
  {
        private ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;



        public OutController(ApplicationDbContext Context, UserManager<ApplicationUser> userManager)
        {
            this._context = Context;
            _userManager = userManager;
        }
     
        public async Task<IActionResult> dialogtemplate()
        {

            var user = await _userManager.GetUserAsync(HttpContext.User);
            var siteid = user.ParentId;
            ViewBag.ParentId = _context.Parents.Where(x => x.ParentId == siteid).ToList();
            ViewBag.DepartmentId = _context.Departments.ToList();
            ViewBag.EmployeeId = _context.Employees.ToList();
            ViewBag.StatusId = _context.Statuses.ToList();
            ViewBag.StatusdelId = _context.Statusdels.ToList();
            ViewBag.StockId = _context.StockItems.ToList();
            ViewBag.dataSource = _context.Outs.Where(x => x.StatusdelId == 1 && x.ParentId == siteid && x.User.Id == user.Id).ToList();
            return View();
        }
        public async Task<IActionResult> AddPartial([FromBody] CRUDModel<Out> value)
        {

            var user = await _userManager.GetUserAsync(HttpContext.User);

            var siteid = user.ParentId;

            ViewBag.ParentId = _context.Parents.Where(x => x.ParentId == siteid).ToList();
            ViewBag.DepartmentId = _context.Departments.ToList();
            ViewBag.EmployeeId = _context.Employees.ToList();
            ViewBag.StockId = _context.StockItems.Where(x => x.User.Id == user.Id).ToList();
            ViewBag.StatusId = _context.Statuses.ToList();
            ViewBag.dataSource = _context.Outs.Where(x => x.StatusdelId == 1 && x.ParentId == siteid && x.User.Id == user.Id).ToList();

            return PartialView("_DialogAddPartial", value.Value);
        }
        public async Task<IActionResult> UrlDatasource([FromBody]DataManagerRequest dm)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var siteid = user.ParentId;
            IEnumerable DataSource = _context.Outs.Where(x => x.StatusdelId == 1 && x.ParentId == siteid && x.User.Id == user.Id).ToList();

            DataOperations operation = new DataOperations();
            if (dm.Where != null && dm.Where.Count > 0) //Filtering
            {
                DataSource = operation.PerformFiltering(DataSource, dm.Where, dm.Where[0].Operator);
            }
            int count = DataSource.Cast<Out>().Count();
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
        public async Task<IActionResult> Insert([FromBody]CRUDModel<Out> value)
        {

            StockItem item = _context.StockItems.Find(value.Value.StockId);
            if (ModelState.IsValid)
            {
                if (value.Value.Quantity < item.Quantity)
                {                
                    item.Quantity = item.Quantity - value.Value.Quantity;

                    _context.Entry(item).State = EntityState.Modified;
                    var user = await _userManager.GetUserAsync(HttpContext.User);
                    value.Value.User = user;
                    //do stuff
                    _context.Add(value.Value);
                    _context.SaveChanges();

                }
                else
                {

                    string msgs = "አሁን ስቶክ ያለው " + item.Quantity + " ነው!! ስቶክ ካለው በላይ መጠየቅ አይችሉም!!!";
                    return Json(new { data = value, message = msgs });
                }
            }
            string msg = value.Value.Quantity +" " +value.Value.Stock.Model +""+ "ወጪ አድርገዋል!!";   //Message from server 
            return Json(new { data = value, message = msg });
        }

        public async Task<IActionResult> EditPartial([FromBody] CRUDModel<Out> value)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var siteid = user.ParentId;

            ViewBag.ParentId = _context.Parents.Where(x => x.ParentId == siteid).ToList();
            ViewBag.DepartmentId = _context.Departments.ToList();
            ViewBag.EmployeeId = _context.Employees.ToList();
            ViewBag.StatusId = _context.Statuses.ToList();
            ViewBag.StockId = _context.StockItems.Where(x => x.User.Id == user.Id).ToList();
            ViewBag.dataSource = _context.Outs.Where(x => x.ParentId == siteid && x.User.Id == user.Id).ToList();
            return PartialView("_DialogEditPartial", value.Value);
        }
        public ActionResult Update([FromBody]CRUDModel<Out> value)
        {

            //do stuff

            if (ModelState.IsValid)
            {
                Out oldSale = _context.Outs.Where(or => or.OutId == value.Value.OutId).AsNoTracking().FirstOrDefault();


                int balance = value.Value.Quantity - oldSale.Quantity;
                StockItem item = _context.StockItems.Find(value.Value.StockId);
                item.Quantity = item.Quantity - balance;
                _context.Entry(item).State = EntityState.Modified;
                _context.Entry(value.Value).State = EntityState.Modified;

                _context.SaveChanges();
            }

            string msg = "መረጃውን በትክክል አዘምነዋል!!";   //Message from server 
            return Json(new { data = value, message = msg });
        }

       // [Authorize(Roles = "SuperAdmin")]
        public ActionResult Remove([FromBody]CRUDModel<Out> value)
        {
            //do stuff
            var product = _context.Outs.FirstOrDefault(m => m.OutId == int.Parse(value.Key.ToString()));
            product.StatusdelId = 2;
            StockItem item = _context.StockItems.Find(product.StockId);
                item.Quantity = item.Quantity + product.Quantity;
                _context.Entry(item).State = EntityState.Modified;
            _context.Entry(product).State = EntityState.Modified;
           // _context.Outs.Remove(product);
                _context.SaveChanges();


            string msg = "መረጃው በትክክል ተሰርዞዋል!!";   //Message from server 
            return Json(new { data = product, message = msg });
        }
   }
}
