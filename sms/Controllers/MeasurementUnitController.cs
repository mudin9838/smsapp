using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using sms.Data;
using sms.Models;

namespace sms.Controllers
{
    public class MeasurementUnitController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MeasurementUnitController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MeasurementUnit
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MeasurementUnits.Include(m => m.SubCategory);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MeasurementUnit/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MeasurementUnits == null)
            {
                return NotFound();
            }

            var measurementUnit = await _context.MeasurementUnits
                .Include(m => m.SubCategory)
                .FirstOrDefaultAsync(m => m.MeasurementUnitId == id);
            if (measurementUnit == null)
            {
                return NotFound();
            }

            return View(measurementUnit);
        }

        // GET: MeasurementUnit/Create
        public IActionResult Create()
        {
            ViewData["SubCategoryId"] = new SelectList(_context.SubCategories, "SubCategoryId", "SubCategoryName");
            return View();
        }

        // POST: MeasurementUnit/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MeasurementUnitId,MeasurementUnitName,SubCategoryId")] MeasurementUnit measurementUnit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(measurementUnit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SubCategoryId"] = new SelectList(_context.SubCategories, "SubCategoryId", "SubCategoryId", measurementUnit.SubCategoryId);
            return View(measurementUnit);
        }

        // GET: MeasurementUnit/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MeasurementUnits == null)
            {
                return NotFound();
            }

            var measurementUnit = await _context.MeasurementUnits.FindAsync(id);
            if (measurementUnit == null)
            {
                return NotFound();
            }
            ViewData["SubCategoryId"] = new SelectList(_context.SubCategories, "SubCategoryId", "SubCategoryId", measurementUnit.SubCategoryId);
            return View(measurementUnit);
        }

        // POST: MeasurementUnit/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MeasurementUnitId,MeasurementUnitName,SubCategoryId")] MeasurementUnit measurementUnit)
        {
            if (id != measurementUnit.MeasurementUnitId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(measurementUnit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MeasurementUnitExists(measurementUnit.MeasurementUnitId))
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
            ViewData["SubCategoryId"] = new SelectList(_context.SubCategories, "SubCategoryId", "SubCategoryId", measurementUnit.SubCategoryId);
            return View(measurementUnit);
        }

        // GET: MeasurementUnit/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MeasurementUnits == null)
            {
                return NotFound();
            }

            var measurementUnit = await _context.MeasurementUnits
                .Include(m => m.SubCategory)
                .FirstOrDefaultAsync(m => m.MeasurementUnitId == id);
            if (measurementUnit == null)
            {
                return NotFound();
            }

            return View(measurementUnit);
        }

        // POST: MeasurementUnit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MeasurementUnits == null)
            {
                return Problem("Entity set 'ApplicationDbContext.MeasurementUnits'  is null.");
            }
            var measurementUnit = await _context.MeasurementUnits.FindAsync(id);
            if (measurementUnit != null)
            {
                _context.MeasurementUnits.Remove(measurementUnit);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MeasurementUnitExists(int id)
        {
          return _context.MeasurementUnits.Any(e => e.MeasurementUnitId == id);
        }
    }
}
