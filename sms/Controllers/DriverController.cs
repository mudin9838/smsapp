using Syncfusion.EJ2.Base;
using System.Collections;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using sms.Models;
using sms.Data;
namespace sms.Controllers
{
  public class DriverController : Controller
  {
	    private ApplicationDbContext _context;

		public DriverController(ApplicationDbContext Context)
		{
            this._context=Context;
		}
        public ActionResult Index()
        {
	        return View();
        }
        public ActionResult UrlDatasource([FromBody]DataManagerRequest dm)
        {

            IEnumerable DataSource = _context.Drivers.ToList();

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
            int count = DataSource.Cast<Driver>().Count();
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
        public ActionResult Insert([FromBody]CRUDModel<Driver>  value)
        {
            //do stuff
            _context.Add(value.Value);
            _context.SaveChanges();
            return Json(value);

        }
        public ActionResult Update([FromBody]CRUDModel<Driver>  value)
        {
            //do stuff
			var ord = value;

           Driver val = _context.Drivers.Where(or => or.DriverId == ord.Value.DriverId).FirstOrDefault();
            val.DriverId=ord.Value.DriverId;
            val.DriverName=ord.Value.DriverName;
            //val.Generals=ord.Value.Generals;
            //val.User=ord.Value.User;
            _context.SaveChanges();
            return Json(value);
        }
        public ActionResult Delete([FromBody]CRUDModel<Driver> value)
        {
            //do stuff
            var product = _context.Drivers.FirstOrDefault(m => m.DriverId == int.Parse(value.Key.ToString()));

            _context.Drivers.Remove(product);
            _context.SaveChanges();
            return Json(product);
        }
   }
}
