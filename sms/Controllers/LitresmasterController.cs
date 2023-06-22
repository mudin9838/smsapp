//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using sms.Data;
//using sms.Models;
//using X.PagedList;
//using static sms.Controllers.HomeController;

//namespace sms.Controllers
//{
//    public class LitresmasterController : Controller
//    {
//        private readonly ApplicationDbContext _context;

//        public LitresmasterController(ApplicationDbContext context)
//        {
//            _context = context;
//        }
//        [HttpGet]
//        public JsonResult LoadFisicalYears()
//        {
//            var fisicalYears = _context.Years
//                .Select(m => new { value = m.YearId, text = m.YearName }).ToList();
//            return Json(fisicalYears);

//        }

//        public JsonResult LoadMonths()
//        {
//            var months = _context.Months
//                .Select(m => new { value = m.MonthId, text = m.MonthName }).ToList();
//            return Json(months);
//        }
//        public JsonResult GetMonthList(int FisicalYearId)
//        {
//            List<Month> StateList = _context.Months.Where(x => x.YearId == FisicalYearId).ToList();
//            return Json(StateList);

//        }

//        // GET: Litresmaster
//        [HttpGet]
//        public async Task<IActionResult> Index(int? page)
//        {
//            IPagedList<General> applicationDbContext  = (IPagedList<General>)_context.Litres.Include(l => l.General).Include(l => l.Month).Include(l => l.Parent).Include(l => l.Year).OrderBy(c => c.ParentId).ThenByDescending(c => c.MonthId).ToPagedList(page ?? 1, 20); 
//            return View(applicationDbContext);
//        }





//        // POST: Litresmaster/Create
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost] // Add New And Edit 
//        [ValidateAntiForgeryToken]
//async Task<IActionResult> SaveOrder(string GeneralId, string FuelId,string ParentId, string Targasno, string CarId, string TargaId, DateTime RegisteredDate, Litre[] orders)
//        {
//            {
//                string result = "መረጃው ተመዝግቧል!!";
//                // New Entry
//                if (string.IsNullOrEmpty(GeneralId))
//                {
//                    if (RegisteredDate != null && !string.IsNullOrEmpty(ParentId.Trim()) && !string.IsNullOrEmpty(FuelId.Trim()) && !string.IsNullOrEmpty(Targasno.Trim()) && !string.IsNullOrEmpty(TargaId.Trim()) && !string.IsNullOrEmpty(CarId.Trim()) && orders != null)
//                    {
//                        var customerId = Guid.NewGuid();
//                        General customer = new General
//                        {
//                            ParentId = ParentId,
//                            FuelId = int.Parse(FuelId),
//                            Targasno = Targasno,
//                            CarId = int.Parse(CarId),
//                            RegisteredDate = RegisteredDate,
//                            GeneralId = int.Parse(GeneralId)
//                        };
//                        _context.Generals.Add(customer);

//                        foreach (var o in orders)
//                        {
//                          //  var orderId = Guid.NewGuid();
//                            Litre order = new Litre();
//                            order.GeneralId = int.Parse(GeneralId);
//                            order.Amountyplan = o.Amountyplan;
//                            order.Amountyperformance = o.Amountyperformance;
//                            //order.Amount = o.Amount;
//                            order.ChartOfAccountId = o.ChartOfAccountId;
//                            order.RevenueCategoryId = o.RevenueCategoryId;
//                          //  order.OrderId = Guid.NewGuid();
//                            db.Orderms.Add(order);

//                            //
//                        }
//                        db.SaveChanges();
//                    }
//                }
//                // Edit Orders 
//                else
//                {
//                    //var customerGuid = Guid.Parse(id);
//                    var customerGuid = Guid.Parse(id);
//                    var customerInDb = db.Generals.FirstOrDefault(c => c.CustomerId == customerGuid);
//                    customerInDb.ParentId = parentid;
//                    customerInDb.ChildId = childid;
//                    customerInDb.FisicalYearId = fisicalyearid;
//                    customerInDb.MonthId = monthid;
//                    customerInDb.OrderDate = orderdate;
//                    //db.Customers.Add(customerInDb);

//                    foreach (var o in orders)
//                    {
//                        var dbOrder = db.Orderms.FirstOrDefault(odr => odr.OrderId == o.OrderId);
//                        if (dbOrder != null)
//                        {
//                            dbOrder.Amountyperformance = o.Amountyperformance;
//                            dbOrder.Amountyplan = o.Amountyplan;
//                            dbOrder.ChartOfAccountId = o.ChartOfAccountId;
//                            // dbOrder.Amount = o.Amount;
//                            dbOrder.RevenueCategoryId = o.RevenueCategoryId;
//                        }
//                        else
//                        {
//                            Orderm order = new Orderm();
//                            order.OrderId = Guid.NewGuid();
//                            order.Amountyperformance = o.Amountyperformance;
//                            order.Amountyplan = o.Amountyplan;
//                            order.RevenueCategoryId = o.RevenueCategoryId;
//                            order.ChartOfAccountId = o.ChartOfAccountId;
//                            //  order.Quantity = o.Quantity;
//                            order.CustomerId = customerGuid;
//                            db.Orderms.Add(order);
//                        }
//                    }
//                    db.SaveChanges();
//                    result = "መረጃው ተስተካክሏል..";
//                }

//                return Json(result);
//            }
//            //if (ModelState.IsValid)
//            //{
//            //    _context.Add(litre);
//            //    await _context.SaveChangesAsync();
//            //    return RedirectToAction(nameof(Index));
//            //}
//            //ViewData["GeneralId"] = new SelectList(_context.Generals, "GeneralId", "GeneralId", litre.GeneralId);
//            //ViewData["MonthId"] = new SelectList(_context.Months, "MonthId", "MonthId", litre.MonthId);
//            //ViewData["ParentId"] = new SelectList(_context.Parents, "ParentId", "ParentId", litre.ParentId);
//            //ViewData["YearId"] = new SelectList(_context.Years, "YearId", "YearId", litre.YearId);
//            //return View(litre);
//        }

//        // GET: Litresmaster/Edit/5
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null || _context.Litres == null)
//            {
//                return NotFound();
//            }

//            var litre = await _context.Litres.FindAsync(id);
//            if (litre == null)
//            {
//                return NotFound();
//            }
//            ViewData["GeneralId"] = new SelectList(_context.Generals, "GeneralId", "GeneralId", litre.GeneralId);
//            ViewData["MonthId"] = new SelectList(_context.Months, "MonthId", "MonthId", litre.MonthId);
//            ViewData["ParentId"] = new SelectList(_context.Parents, "ParentId", "ParentId", litre.ParentId);
//            ViewData["YearId"] = new SelectList(_context.Years, "YearId", "YearId", litre.YearId);
//            return View(litre);
//        }

//        // POST: Litresmaster/Edit/5
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, [Bind("LitreId,MonthId,YearId,ParentId,Birr,Litre1,Awaited,Totallitre,Startkm,Endkm,Differencekm,Litreused,Birrused,Litreremain,Birrremain,Description,RegisteredDate,GeneralId")] Litre litre)
//        {
//            if (id != litre.LitreId)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(litre);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!LitreExists(litre.LitreId))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));
//            }
//            ViewData["GeneralId"] = new SelectList(_context.Generals, "GeneralId", "GeneralId", litre.GeneralId);
//            ViewData["MonthId"] = new SelectList(_context.Months, "MonthId", "MonthId", litre.MonthId);
//            ViewData["ParentId"] = new SelectList(_context.Parents, "ParentId", "ParentId", litre.ParentId);
//            ViewData["YearId"] = new SelectList(_context.Years, "YearId", "YearId", litre.YearId);
//            return View(litre);
//        }

//        // GET: Litresmaster/Delete/5
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null || _context.Litres == null)
//            {
//                return NotFound();
//            }

//            var litre = await _context.Litres
//                .Include(l => l.General)
//                .Include(l => l.Month)
//                .Include(l => l.Parent)
//                .Include(l => l.Year)
//                .FirstOrDefaultAsync(m => m.LitreId == id);
//            if (litre == null)
//            {
//                return NotFound();
//            }

//            return View(litre);
//        }

//        // POST: Litresmaster/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            if (_context.Litres == null)
//            {
//                return Problem("Entity set 'ApplicationDbContext.Litres'  is null.");
//            }
//            var litre = await _context.Litres.FindAsync(id);
//            if (litre != null)
//            {
//                _context.Litres.Remove(litre);
//            }
            
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        private bool LitreExists(int id)
//        {
//          return _context.Litres.Any(e => e.LitreId == id);
//        }
//    }
//}
