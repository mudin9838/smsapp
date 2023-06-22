using Syncfusion.EJ2.Base;
using System.Collections;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using sms.Models;
using sms.Data;
namespace sms.Controllers
{
  public class CarController : Controller
  {
	    private ApplicationDbContext _context;

		public CarController(ApplicationDbContext Context)
		{
            this._context=Context;
		}
        public ActionResult Index()
        {
	        return View();
        }
        public ActionResult UrlDatasource([FromBody]DataManagerRequest dm)
        {

            IEnumerable DataSource = _context.Cars.ToList();

            DataOperations operation = new DataOperations();   
            if (dm.Sorted != null && dm.Sorted.Count > 0) //Sorting
            {
               DataSource = operation.PerformSorting(DataSource, dm.Sorted);
            }
	       if (dm.Search != null && dm.Search.Count > 0)
           {
               DataSource = operation.PerformSearching(DataSource, dm.Search);  //Search
           }
            int count = DataSource.Cast<Car>().Count();
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
        public ActionResult Insert([FromBody]CRUDModel<Car>  value)
        {
            //do stuff
            _context.Add(value.Value);
            _context.SaveChanges();
            return Json(value);

        }
        public ActionResult Update([FromBody]CRUDModel<Car>  value)
        {
            //do stuff
			var ord = value;

           Car val = _context.Cars.Where(or => or.CarId == ord.Value.CarId).FirstOrDefault();
            val.CarId=ord.Value.CarId;
            val.CarName=ord.Value.CarName;
            //val.Generals=ord.Value.Generals;
            _context.SaveChanges();
            return Json(value);
        }
        public ActionResult Remove([FromBody]CRUDModel<Car> value)
        {
            //do stuff
            var product = _context.Cars.FirstOrDefault(m => m.CarId == int.Parse(value.Key.ToString()));

            _context.Cars.Remove(product);
            _context.SaveChanges();
            return Json(product);
        }
   }
}
