using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PMS.Models;

namespace PMS.Controllers
{
    public class TblBaseSalariesController : BaseController
    {
        private readonly PMSDbContext _context;

        public TblBaseSalariesController(PMSDbContext context)
        {
            _context = context;
        }

        // GET: TblBaseSalaries
        public async Task<IActionResult> Index()
        {
            return View(await _context.TblBaseSalaries.OrderByDescending(x=>x.Status).ToListAsync());
        }

        // GET: TblBaseSalaries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblBaseSalary = await _context.TblBaseSalaries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblBaseSalary == null)
            {
                return NotFound();
            }

            return View(tblBaseSalary);
        }

        // GET: TblBaseSalaries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TblBaseSalaries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Value,DateIssued,Description")] TblBaseSalary tblBaseSalary)
        {
            if (ModelState.IsValid)
            {
                // Cập nhật lại trạng thái mức lương cơ sở gần nhất về 0
                var objOld = _context.TblBaseSalaries.Where(x => x.Status == 1).FirstOrDefault();
                if (objOld != null)
                {
                    objOld.Status = 0;
                    _context.SaveChanges();
                }

                tblBaseSalary.Status = 1;
                _context.Add(tblBaseSalary);
                await _context.SaveChangesAsync();
                SetAlert("success", "Thêm dữ liệu thành công");
                return RedirectToAction(nameof(Index));
            }
            return View(tblBaseSalary);
        }

        // GET: TblBaseSalaries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblBaseSalary = await _context.TblBaseSalaries.FindAsync(id);
            if (tblBaseSalary == null)
            {
                return NotFound();
            }
            return View(tblBaseSalary);
        }

        // POST: TblBaseSalaries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Value,DateIssued,Description,Status")] TblBaseSalary tblBaseSalary)
        {
            if (id != tblBaseSalary.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblBaseSalary);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblBaseSalaryExists(tblBaseSalary.Id))
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
            return View(tblBaseSalary);
        }

        // GET: TblBaseSalaries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblBaseSalary = await _context.TblBaseSalaries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblBaseSalary == null)
            {
                return NotFound();
            }

            return View(tblBaseSalary);
        }

        // POST: TblBaseSalaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblBaseSalary = await _context.TblBaseSalaries.FindAsync(id);
            _context.TblBaseSalaries.Remove(tblBaseSalary);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblBaseSalaryExists(int id)
        {
            return _context.TblBaseSalaries.Any(e => e.Id == id);
        }
    }
}
