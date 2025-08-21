using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PMS.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Controllers
{
    public class TblPositionController : BaseController
    {
        private readonly PMSDbContext _context;

        public TblPositionController(PMSDbContext context)
        {
            _context = context;
        }

        // GET: TblPosition
        public async Task<IActionResult> Index()
        {
            var positions = await _context.TblPositions.ToListAsync();
            return View(positions);
        }

        // GET: TblPosition/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TblPosition/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,ShortName,TypeObject,BasicPosiontConfficient,ResponsibilityCoefficient,PosiontConfficient,DateIssued,Description,Note,Status")] TblPosition tblPosition)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblPosition);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblPosition);
        }

        // GET: TblPosition/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblPosition = await _context.TblPositions.FindAsync(id);
            if (tblPosition == null)
            {
                return NotFound();
            }
            return View(tblPosition);
        }

        // POST: TblPosition/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ShortName,TypeObject,BasicPosiontConfficient,ResponsibilityCoefficient,PosiontConfficient,DateIssued,Description,Note,Status")] TblPosition tblPosition)
        {
            if (id != tblPosition.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblPosition);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblPositionExists(tblPosition.Id))
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
            return View(tblPosition);
        }

        // GET: TblPosition/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblPosition = await _context.TblPositions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblPosition == null)
            {
                return NotFound();
            }

            return View(tblPosition);
        }

        // POST: TblPosition/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblPosition = await _context.TblPositions.FindAsync(id);
            _context.TblPositions.Remove(tblPosition);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: TblPosition/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblPosition = await _context.TblPositions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblPosition == null)
            {
                return NotFound();
            }

            return View(tblPosition);
        }

        private bool TblPositionExists(int id)
        {
            return _context.TblPositions.Any(e => e.Id == id);
        }
    }
}
