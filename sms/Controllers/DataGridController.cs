using Syncfusion.EJ2.Base;
using System.Collections;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using sms.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using sms.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using sms.Enums;

namespace sms.Controllers
{
    public class DataGridController : Controller
    {
        private ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;



        public DataGridController(ApplicationDbContext Context, UserManager<ApplicationUser> userManager)
        {
            this._context = Context;
            _userManager = userManager;
        }
     
     



        public async Task<IActionResult> dialogtemplate()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var siteid = user.ParentId;
            ViewBag.ParentId = _context.Parents.Where(x=>x.ParentId== siteid).ToList();
            ViewBag.CategoryId = _context.Categories.ToList();
            ViewBag.SubCategoryId = _context.SubCategories.ToList();
            ViewBag.MeasurementUnitId = _context.MeasurementUnits.ToList();
            ViewBag.StatusdelId = _context.Statusdels.ToList();
            //ViewBag.dataSource = _context.StockItems.ToList();
            ViewBag.dataSource = _context.StockItems.Where(x => x.StatusdelId == 1 && x.ParentId == siteid && x.User.Id == user.Id).ToList();
            return View(ViewBag.dataSource);
        }
    
        public async Task<IActionResult> AddPartial([FromBody] CRUDModel<StockItem> value)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var siteid = user.ParentId;

            ViewBag.ParentId = _context.Parents.Where(x => x.ParentId == siteid).ToList();
            ViewBag.CategoryId = _context.Categories.ToList();
            ViewBag.SubCategoryId = _context.SubCategories.ToList();
            ViewBag.MeasurementUnitId = _context.MeasurementUnits.ToList();
            ViewBag.dataSource = _context.StockItems.Where(x => x.StatusdelId == 1 && x.ParentId == siteid && x.User.Id == user.Id).ToList();
            return PartialView("_DialogAddPartial", value.Value);
        }
        public async Task<IActionResult> UrlDatasource([FromBody] DataManagerRequest dm)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var siteid = user.ParentId;
            IEnumerable DataSource = _context.StockItems.Where(x => x.StatusdelId == 1 && x.ParentId == siteid && x.User.Id == user.Id).ToList();

            DataOperations operation = new DataOperations();
            if (dm.Where != null && dm.Where.Count > 0) //Filtering
            {
                DataSource = operation.PerformFiltering(DataSource, dm.Where, dm.Where[0].Operator);
            }
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
        public async Task<IActionResult> Insert([FromBody] CRUDModel<StockItem> value)
        {
            //do stuff
            // here you can do the insert action
            var user = await _userManager.GetUserAsync(HttpContext.User);
            value.Value.User= user;
            _context.Add(value.Value);
            _context.SaveChanges();
            string msg = value.Value.Quantity + " " + "" + " በትክክል መዝግበዋል!!";   //Message from server 
            return Json(new { data = value, message = msg });

        }
        public async Task<IActionResult> EditPartial([FromBody] CRUDModel<StockItem> value)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var siteid = user.ParentId;

            ViewBag.ParentId = _context.Parents.Where(x => x.ParentId == siteid).ToList();
            ViewBag.CategoryId = _context.Categories.ToList();
            ViewBag.SubCategoryId = _context.SubCategories.ToList();
            ViewBag.MeasurementUnitId = _context.MeasurementUnits.ToList();
            ViewBag.dataSource = _context.StockItems.Where(x=>x.ParentId == siteid && x.User.Id == user.Id).ToList();
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
            string msg = "መረጃውን በትክክል አዘምነዋል!!";   //Message from server 
            return Json(new { data = value, message = msg });
        }
        [Authorize(Roles = "SuperAdmin")]
        public ActionResult Remove([FromBody] CRUDModel<StockItem> value)
        {
            //do stuff
            var product = _context.StockItems.FirstOrDefault(m => m.StockId == int.Parse(value.Key.ToString()));
            product.StatusdelId = 2;
            _context.Entry(product).State = EntityState.Modified;
            // _context.StockItems.Remove(product);
            _context.SaveChanges();


            string msg = "መረጃው በትክክል ተሰርዞዋል!!";   //Message from server 
            return Json(new { data = product, message = msg });
        }
    }
}
