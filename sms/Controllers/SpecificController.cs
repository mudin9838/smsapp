using Syncfusion.EJ2.Base;
using System.Collections;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using sms.Models;
using sms.Data;
using System.Security.Policy;

namespace sms.Controllers
{
    public class SpecificController : Controller
    {
        private ApplicationDbContext _context;

        public SpecificController(ApplicationDbContext Context)
        {
            this._context = Context;
        }
        public ActionResult Index()
        {

            ViewBag.CategoryId = _context.Categories.ToList();
            ViewBag.SubCategoryId = _context.SubCategories.ToList();
            ViewBag.dataSource = _context.SubCategories.ToList();

            return View();
        }
        public ActionResult UrlDatasource([FromBody] DataManagerRequest dm)
        {

            IEnumerable DataSource = _context.SubCategories.ToList();

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
            int count = DataSource.Cast<SubCategory>().Count();
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
        public ActionResult Insert([FromBody] CRUDModel<SubCategory> value)
        {

                   ViewBag.CategoryId = _context.Categories.ToList();
           // ViewBag.SubCategoryId = _context.SubCategories.ToList();
            ViewBag.dataSource = _context.SubCategories.ToList();
            //do stuff
            _context.SubCategories.Add(value.Value);
            _context.SaveChanges();
            return Json(value);

        }
        public ActionResult Update([FromBody] CRUDModel<SubCategory> value)
        {
            //do stuff
            var ord = value;

            SubCategory val = _context.SubCategories.Where(or => or.SubCategoryId == ord.Value.SubCategoryId).FirstOrDefault();
            val.SubCategoryId = ord.Value.SubCategoryId;
            val.SubCategoryName = ord.Value.SubCategoryName;
            val.CategoryId = ord.Value.CategoryId;
            _context.SaveChanges();
            return Json(value);
        }
        public ActionResult Delete([FromBody] CRUDModel<SubCategory> value)
        {
            //do stuff
            SubCategory order = _context.SubCategories.Where(c => c.SubCategoryId == (int)value.Key).FirstOrDefault();
            _context.SubCategories.Remove(order);
            _context.SaveChanges();
            return Json(order);
        }
    }
}
