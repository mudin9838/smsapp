using Syncfusion.EJ2.Base;
using System.Collections;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using sms.Models;
namespace sms.Controllers
{
    public class DataGridController : Controller
    {
        private EserviceContext _context;

        public DataGridController(EserviceContext Context)
        {
            this._context = Context;
        }
        public ActionResult dialogtemplate()
        {

            ViewBag.ParentId = _context.Parents.ToList();
            ViewBag.CategoryId = _context.Categories.ToList();
            ViewBag.SubCategoryId = _context.SubCategories.ToList();
            ViewBag.MeasurementUnitId = _context.MeasurementUnits.ToList();
            ViewBag.dataSource = _context.StockItems.ToList();
            return View();
        }
        public IActionResult AddPartial([FromBody] CRUDModel<StockItem> value)
        {
            ViewBag.ParentId = _context.Parents.ToList();
            ViewBag.CategoryId = _context.Categories.ToList();
            ViewBag.SubCategoryId = _context.SubCategories.ToList();
            ViewBag.MeasurementUnitId = _context.MeasurementUnits.ToList();
            ViewBag.dataSource = _context.StockItems.ToList();
            return PartialView("_DialogAddPartial", value.Value);
        }
        public ActionResult UrlDatasource([FromBody] DataManagerRequest dm)
        {

            IEnumerable DataSource = _context.StockItems.ToList();

            DataOperations operation = new DataOperations();
            int count = DataSource.Cast<StockItem>().Count();
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
        public ActionResult Insert([FromBody] CRUDModel<StockItem> value)
        {
            //do stuff
            // here you can do the insert action

            _context.Add(value.Value);
            _context.SaveChanges();
            return Json(value);

        }
        public IActionResult EditPartial([FromBody] CRUDModel<StockItem> value)
        {
            ViewBag.ParentId = _context.Parents.ToList();
            ViewBag.CategoryId = _context.Categories.ToList();
            ViewBag.SubCategoryId = _context.SubCategories.ToList();
            ViewBag.MeasurementUnitId = _context.MeasurementUnits.ToList();
            ViewBag.dataSource = _context.StockItems.ToList();
            return PartialView("_DialogEditPartial", value.Value);
        }
        public ActionResult Update([FromBody] CRUDModel<StockItem> value)
        {
            //do stuff
            var ord = value;

            StockItem val = _context.StockItems.Where(or => or.StockId == ord.Value.StockId).FirstOrDefault();
            val.StockId = ord.Value.StockId;
            val.ParentId = ord.Value.ParentId;
            val.Serie = ord.Value.Serie;
            val.CategoryId = ord.Value.CategoryId;
            val.SubCategoryId = ord.Value.SubCategoryId;
            val.MeasurementUnitId = ord.Value.MeasurementUnitId;
            val.Quantity = ord.Value.Quantity;
            val.EachPrice = ord.Value.EachPrice;
            val.Vat = ord.Value.Vat;
            val.TotalPrice = ord.Value.TotalPrice;
            val.RegisteredDate = ord.Value.RegisteredDate;
            val.Category = ord.Value.Category;
            val.MeasurementUnit = ord.Value.MeasurementUnit;
            val.Parent = ord.Value.Parent;
            val.SubCategory = ord.Value.SubCategory;
            // val.Entries=ord.Value.Entries;
            // val.Outs=ord.Value.Outs;
            _context.SaveChanges();
            return Json(value);
        }
        public ActionResult Remove([FromBody] CRUDModel<StockItem> value)
        {
            //do stuff
            var product = _context.StockItems.FirstOrDefault(m => m.StockId == int.Parse(value.Key.ToString()));

            _context.StockItems.Remove(product);
            _context.SaveChanges();
            return Json(product);
        }
    }
}
