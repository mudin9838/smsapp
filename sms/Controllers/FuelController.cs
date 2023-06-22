using Syncfusion.EJ2.Base;
using System.Collections;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using sms.Models;
using sms.Data;
namespace sms.Controllers
{
  public class FuelController : Controller
  {
	    private ApplicationDbContext _context;

		public FuelController(ApplicationDbContext Context)
		{
            this._context=Context;
		}
        public ActionResult Index()
        {
	        return View();
        }
        public ActionResult UrlDatasource([FromBody]DataManagerRequest dm)
        {

            IEnumerable DataSource = _context.Fuels.ToList();

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
            int count = DataSource.Cast<Fuel>().Count();
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
        public ActionResult Insert([FromBody]CRUDModel<Fuel>  value)
        {
            //do stuff
            _context.Add(value.Value);
            _context.SaveChanges();
            return Json(value);

        }
        public ActionResult Update([FromBody]CRUDModel<Fuel>  value)
        {
            //do stuff
			var ord = value;

           Fuel val = _context.Fuels.Where(or => or.FuelId == ord.Value.FuelId).FirstOrDefault();
            val.FuelId=ord.Value.FuelId;
            val.FuelName=ord.Value.FuelName;
            //val.Generals=ord.Value.Generals;
            _context.SaveChanges();
            return Json(value);
        }
        public ActionResult Remove([FromBody]CRUDModel<Fuel> value)
        {
            //do stuff
            var product = _context.Fuels.FirstOrDefault(m => m.FuelId == int.Parse(value.Key.ToString()));

            _context.Fuels.Remove(product);
            _context.SaveChanges();
            return Json(product);
        }
   }
}
