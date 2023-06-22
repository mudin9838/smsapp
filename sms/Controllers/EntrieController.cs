using Syncfusion.EJ2.Base;
using System.Collections;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using sms.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Policy;
using sms.Data;
using System;

namespace sms.Controllers
{
  public class EntrieController : Controller
  {
        private ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;



        public EntrieController(ApplicationDbContext Context, UserManager<ApplicationUser> userManager)
        {
            this._context = Context;
            _userManager = userManager;
        }
    
        public async Task<IActionResult> dialogtemplate()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var siteid = user.ParentId;
            ViewBag.ParentId = _context.Parents.Where(x => x.ParentId == siteid).ToList();
            ViewBag.StatusId = _context.Statuses.ToList();
            ViewBag.StockId = _context.StockItems.ToList();
            ViewBag.StatusdelId = _context.Statusdels.ToList();
            //ViewBag.dataSource = _context.StockItems.ToList();
            ViewBag.dataSource = _context.Entries.Where(x => x.StatusdelId == 1 && x.ParentId == siteid && x.User.Id == user.Id).ToList();
            return View();
        }

     
        public async Task<IActionResult> AddPartial([FromBody] CRUDModel<Entry> value)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var siteid = user.ParentId;

            ViewBag.ParentId = _context.Parents.Where(x => x.ParentId == siteid).ToList();
            ViewBag.StatusId = _context.Statuses.ToList();
            ViewBag.StatusdelId = _context.Statusdels.ToList();

            ViewBag.StockId = _context.StockItems.Where(x => x.User.Id == user.Id).ToList();
           ViewBag.dataSource = _context.Entries.Where(x => x.StatusdelId == 1 && x.ParentId == siteid && x.User.Id == user.Id).ToList();
            return PartialView("_DialogAddPartial", value.Value);
        }
        public async Task<IActionResult> UrlDatasource([FromBody]DataManagerRequest dm)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var siteid = user.ParentId;
            IEnumerable DataSource = _context.Entries.Where(x => x.StatusdelId == 1 && x.ParentId == siteid && x.User.Id == user.Id).ToList();

            DataOperations operation = new DataOperations();   
			if (dm.Where != null && dm.Where.Count > 0) //Filtering
            {
			    DataSource = operation.PerformFiltering(DataSource, dm.Where, dm.Where[0].Operator);
            }
            int count = DataSource.Cast<Entry>().Count();
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
        public async Task<IActionResult> Insert([FromBody] CRUDModel<Entry> value)
        {

            //do stuff
            // here you can do the insert action
            var vatValue = value.Value.EachPrice * value.Value.Quantity * (decimal)0.15;
            var totalPriceWithVat = value.Value.Quantity * value.Value.EachPrice + vatValue;

            
            StockItem item = _context.StockItems.Find(value.Value.StockId);
            item.Quantity = item.Quantity + value.Value.Quantity;
            //item.EachPrice =  value.Value.EachPrice;
            //item.Vat = vatValue;
            //item.TotalPrice = totalPriceWithVat;
            
            _context.Entry(item).State = EntityState.Modified; // I want updated result 
                                                               //do stuff
            var user = await _userManager.GetUserAsync(HttpContext.User);
            value.Value.User = user;
            _context.Add(value.Value);
            _context.SaveChanges(); //on save both changes(including modified) also will be saved
            string msg = value.Value.Quantity + " " + value.Value.Stock.SubCategory.SubCategoryName + "" + "ገቢ አድርገዋል!!";   //Message from server 
            return Json(new { data = value, message = msg });

        }
       
        public async Task<IActionResult> EditPartial([FromBody] CRUDModel<Entry> value)
        {

            var user = await _userManager.GetUserAsync(HttpContext.User);
            var siteid = user.ParentId;

            ViewBag.ParentId = _context.Parents.Where(x => x.ParentId == siteid).ToList();
            ViewBag.StatusId = _context.Statuses.ToList();
            ViewBag.StatusdelId = _context.Statusdels.ToList();

            ViewBag.StockId = _context.StockItems.Where(x=>x.User.Id == user.Id).ToList();
            ViewBag.dataSource = _context.Entries.Where(x => x.ParentId == siteid && x.User.Id == user.Id).ToList();
            return PartialView("_DialogEditPartial", value.Value);
        }
        public ActionResult Update([FromBody]CRUDModel<Entry>  value)
        {
            if (ModelState.IsValid)
            {
                Entry oldEntry = _context.Entries.Where(or => or.EntryId == value.Value.EntryId).AsNoTracking().FirstOrDefault();


                int balance = value.Value.Quantity - oldEntry.Quantity;
                StockItem item = _context.StockItems.Find(value.Value.StockId);
                item.Quantity = item.Quantity + balance;
                _context.Entry(item).State = EntityState.Modified;
               _context.Entry(value.Value).State = EntityState.Modified;

                _context.SaveChanges();
            }

            string msg = "መረጃውን በትክክል አዘምነዋል!!";   //Message from server 
            return Json(new { data = value, message = msg });
        }
    
        public ActionResult Remove([FromBody] CRUDModel<Entry> value)
        {
        //do stuff
        var product = _context.Entries.FirstOrDefault(m => m.EntryId == int.Parse(value.Key.ToString()));
            product.StatusdelId = 2;
            StockItem item = _context.StockItems.Find(product.StockId);
        item.Quantity = item.Quantity - product.Quantity;
        _context.Entry(item).State = EntityState.Modified;
            _context.Entry(product).State = EntityState.Modified;
          //  _context.Entries.Remove(product);
        _context.SaveChanges();


            string msg = "መረጃው በትክክል ተሰርዞዋል!!";   //Message from server 
            return Json(new { data = product, message = msg });
        }
    }
}
