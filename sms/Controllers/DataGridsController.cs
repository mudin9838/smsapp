﻿using System.Linq;
using Microsoft.AspNetCore.Mvc;
using sms.Models;
using sms.Data;
using static sms.Controllers.HomeController;
using Syncfusion.EJ2.Base;
using Syncfusion.EJ2;
using System.Security.Policy;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace sms.Controllers
{

   public class DataGridsController : Controller
   {
        private ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;



        public DataGridsController(ApplicationDbContext Context, UserManager<ApplicationUser> userManager)
        {
            this._context = Context;
            _userManager = userManager;
        }

        public async Task<IActionResult> dialogtemplate()
        {

            var user = await _userManager.GetUserAsync(HttpContext.User);
            var siteid = user.ParentId;
            ViewBag.ParentId = _context.Parents.Where(x => x.ParentId == siteid).ToList();
            
            ViewBag.CarId = _context.Cars.ToList();
            ViewBag.TargaId = _context.Targas.ToList();
            ViewBag.DriverId = _context.Drivers.ToList();
            ViewBag.FuelId = _context.Fuels.ToList();
            ViewBag.YearId = _context.Years.ToList();
            ViewBag.MonthId = _context.Months.ToList();


            //    ViewBag.GeneralId = _context.Generals.Where(x => x.User.Id == user.Id).ToList();
            ViewBag.CustomerDataSource = _context.Litres.Where(x => x.ParentId == siteid && x.User.Id == user.Id).ToList();
            ViewBag.DataSource = _context.Generals.Where(x => x.ParentId == siteid && x.User.Id == user.Id).ToList();

            return View();
        }
        public async Task<IActionResult> AddPartial([FromBody] CRUDModel<Litre> value)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var siteid = user.ParentId;
            ViewBag.ParentId = _context.Parents.Where(x => x.ParentId == siteid).ToList();
            ViewBag.YearId = _context.Years.ToList();
            ViewBag.MonthId = _context.Months.ToList();
            ViewBag.GeneralId = _context.Generals.Where(x => x.User.Id == user.Id).ToList();
            // ViewBag.StatusdelId = _context.Statusdels.ToList();
            //ViewBag.dataSource = _context.Litres.ToList();
            ViewBag.dataSource = _context.Litres.Where(x => x.ParentId == siteid && x.User.Id == user.Id).ToList();
            return PartialView("_DialogAddPartial", value.Value);
        }
        public async Task<IActionResult> UrlDatasource([FromBody] DataManagerRequest dm)
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
        [HttpPost]
        public async Task<IActionResult> UrlDatasource1([FromBody] DataManagerRequest dm)
        {

            var user = await _userManager.GetUserAsync(HttpContext.User);

            var siteid = user.ParentId;
            IEnumerable DataSource = _context.Litres.Where(x => x.ParentId == siteid && x.User.Id == user.Id).ToList();

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
            int count = DataSource.Cast<Litre>().Count();
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

        public async Task<IActionResult> EditPartial([FromBody] CRUDModel<Litre> value)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var siteid = user.ParentId;
            ViewBag.ParentId = _context.Parents.Where(x => x.ParentId == siteid).ToList();
            ViewBag.YearId = _context.Years.ToList();
            ViewBag.MonthId = _context.Months.ToList();
            ViewBag.GeneralId = _context.Generals.Where(x => x.User.Id == user.Id).ToList();
            // ViewBag.StatusdelId = _context.Statusdels.ToList();
            //ViewBag.dataSource = _context.Litres.ToList();
            ViewBag.dataSource = _context.Litres.Where(x => x.ParentId == siteid && x.User.Id == user.Id).ToList();
            return PartialView("_DialogEditPartial", value.Value);
        }
        public ActionResult EditUpdate([FromBody] CRUDModel<General> value)
        {
            //do stuff
            var ord = value;

            General val = _context.Generals.Where(or => or.GeneralId == ord.Value.GeneralId).FirstOrDefault();
            val.GeneralId = ord.Value.GeneralId;
            val.ParentId = ord.Value.ParentId;
            val.CarId = ord.Value.CarId;
            val.TargaId = ord.Value.TargaId;
            val.DriverId = ord.Value.DriverId;
            val.RegisteredDate = ord.Value.RegisteredDate;
            val.FuelId = ord.Value.FuelId;
            _context.SaveChanges();
            string msg = "መረጃውን በትክክል አዘምነዋል!!";   //Message from server 
            return Json(new { data = value, message = msg });
        }
        public async Task<IActionResult> EditInsert([FromBody] CRUDModel<General> value)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            value.Value.User = user;
            _context.Add(value.Value);
            _context.SaveChanges();
            string msg = "በትክክል መዝግበዋል!!";   //Message from server 
            return Json(new { data = value, message = msg });
        }
        public ActionResult EditDelete([FromBody] CRUDModel<General> value)
        {
            var product = _context.Generals.FirstOrDefault(m => m.GeneralId == int.Parse(value.Key.ToString()));

            _context.Generals.Remove(product);
            _context.SaveChanges();
            string msg = "መረጃው በትክክል ተሰርዞዋል!!";   //Message from server 
            return Json(new { data = product, message = msg });
        }
        public async Task<IActionResult> CellEditInsert([FromBody] CRUDModel<Litre> value)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            value.Value.User = user;
            _context.Add(value.Value);
            _context.SaveChanges(); //Message from server 
            string msg = "በትክክል መዝግበዋል!!";   //Message from server 
            return Json(new { data = value, message = msg });
        }
        public ActionResult CellEditUpdate([FromBody] CRUDModel<Litre> value)
        {
            //do stuff
            var ord = value;

            Litre val = _context.Litres.Where(or => or.LitreId == ord.Value.LitreId).FirstOrDefault();
            val.LitreId = ord.Value.LitreId;
            val.MonthId = ord.Value.MonthId;
            val.YearId = ord.Value.YearId;
            val.ParentId = ord.Value.ParentId;
            val.Birr = ord.Value.Birr;
            val.Litre1 = ord.Value.Litre1;
            val.Awaited = ord.Value.Awaited;
            val.Totallitre = ord.Value.Totallitre;
            val.Startkm = ord.Value.Startkm;
            val.Endkm = ord.Value.Endkm;
            val.Differencekm = ord.Value.Differencekm;
            val.Litreused = ord.Value.Litreused;
            val.Birrused = ord.Value.Birrused;
            val.Litreremain = ord.Value.Litreremain;
            val.Birrremain = ord.Value.Birrremain;
            val.Description = ord.Value.Description;
            val.GeneralId = ord.Value.GeneralId;
            val.RegisteredDate = ord.Value.RegisteredDate;
            //val.General=ord.Value.General;
            //val.Month=ord.Value.Month;
            //val.Parent=ord.Value.Parent;
            //val.Year=ord.Value.Year;
            // val.User=ord.Value.User;
            _context.SaveChanges();
            string msg = "Successfully performed editing the record";   //Message from server 
            return Json(new { data = value, message = msg });
        }
  
        public ActionResult CellEditDelete([FromBody] CRUDModel<Litre> value)
        {
            //do stuff
            var product = _context.Litres.FirstOrDefault(m => m.LitreId == int.Parse(value.Key.ToString()));

            _context.Litres.Remove(product);
            _context.SaveChanges();
            string msg = "መረጃው በትክክል ተሰርዞዋል!!";   //Message from server 
            return Json(new { data = product, message = msg });
        }

    }
}

