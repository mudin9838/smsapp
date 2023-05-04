using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using sms.Data;
using sms.Models;

namespace sms.Controllers
{
    public class OutsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OutsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Outs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Outs.Include(e => e.Department).Include(e => e.Employee).Include(e => e.Parent).Include(e => e.Status).Include(e => e.Statusdel).Include(e => e.Stock);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Outs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Outs == null)
            {
                return NotFound();
            }

            var eout = await _context.Outs
                .Include(e => e.Department)
                .Include(e => e.Employee)
                .Include(e => e.Parent)
                .Include(e => e.Status)
                .Include(e => e.Statusdel)
                .Include(e => e.Stock)
                .FirstOrDefaultAsync(m => m.OutId == id);
            if (eout == null)
            {
                return NotFound();
            }

            return View(eout);
        }

        // GET: Outs/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentId");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId");
            ViewData["ParentId"] = new SelectList(_context.Parents, "ParentId", "ParentId");
            ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "StatusId");
            ViewData["StatusdelId"] = new SelectList(_context.Statusdels, "StatusdelId", "StatusdelId");
            ViewData["StockId"] = new SelectList(_context.StockItems, "StockId", "StockId");
            return View();
        }

        // POST: Outs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OutId,RecieptNo,ParentId,DepartmentId,EmployeeId,From,To,Serie,Quantity,EachPrice,Vat,TotalPrice,OutDate,StockId,StatusId,StatusdelId")] Out eout)
        {
            if (ModelState.IsValid)
            {
               // Out outd = _context.Outs.Where(x=>x.StockId== eout.StockId).FirstOrDefault();
                StockItem item = _context.StockItems.Find(eout.StockId);

                if (eout.Quantity < item.Quantity)
                {
                    _context.Add(eout);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                                  ViewBag.Message = "error";

                }
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentId", eout.DepartmentId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", eout.EmployeeId);
            ViewData["ParentId"] = new SelectList(_context.Parents, "ParentId", "ParentId", eout.ParentId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "StatusId", eout.StatusId);
            ViewData["StatusdelId"] = new SelectList(_context.Statusdels, "StatusdelId", "StatusdelId", eout.StatusdelId);
            ViewData["StockId"] = new SelectList(_context.StockItems, "StockId", "StockId", eout.StockId);
            return View(eout);
        }

        // GET: Outs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Outs == null)
            {
                return NotFound();
            }

            var eout = await _context.Outs.FindAsync(id);
            if (eout == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentId", eout.DepartmentId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", eout.EmployeeId);
            ViewData["ParentId"] = new SelectList(_context.Parents, "ParentId", "ParentId", eout.ParentId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "StatusId", eout.StatusId);
            ViewData["StatusdelId"] = new SelectList(_context.Statusdels, "StatusdelId", "StatusdelId", eout.StatusdelId);
            ViewData["StockId"] = new SelectList(_context.StockItems, "StockId", "StockId", eout.StockId);
            return View(eout);
        }

        // POST: Outs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OutId,RecieptNo,ParentId,DepartmentId,EmployeeId,From,To,Serie,Quantity,EachPrice,Vat,TotalPrice,OutDate,StockId,StatusId,StatusdelId")] Out eout)
        {
            if (id != eout.OutId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eout);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OutExists(eout.OutId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentId", eout.DepartmentId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", eout.EmployeeId);
            ViewData["ParentId"] = new SelectList(_context.Parents, "ParentId", "ParentId", eout.ParentId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "StatusId", eout.StatusId);
            ViewData["StatusdelId"] = new SelectList(_context.Statusdels, "StatusdelId", "StatusdelId", eout.StatusdelId);
            ViewData["StockId"] = new SelectList(_context.StockItems, "StockId", "StockId", eout.StockId);
            return View(eout);
        }

        // GET: Outs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Outs == null)
            {
                return NotFound();
            }

            var eout = await _context.Outs
                .Include(e => e.Department)
                .Include(e => e.Employee)
                .Include(e => e.Parent)
                .Include(e => e.Status)
                .Include(e => e.Statusdel)
                .Include(e => e.Stock)
                .FirstOrDefaultAsync(m => m.OutId == id);
            if (eout == null)
            {
                return NotFound();
            }

            return View(eout);
        }

        // POST: Outs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Outs == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Outs'  is null.");
            }
            var eout = await _context.Outs.FindAsync(id);
            if (eout != null)
            {
                _context.Outs.Remove(eout);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OutExists(int id)
        {
          return _context.Outs.Any(e => e.OutId == id);
        }
    }
}
