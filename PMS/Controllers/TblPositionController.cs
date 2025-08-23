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
            var positions = await _context.TblPositions.Where(x=> x.Status == 1).ToListAsync();
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
        public async Task<IActionResult> Create([Bind("Name,ShortName,TypeObject,BasicPosiontConfficient,ResponsibilityCoefficient,PosiontConfficient,DateIssued,Description,Note")] TblPosition tblPosition)
        {
            if (ModelState.IsValid)
            {
                // Check if position with same name already exists and is active
                var existingPosition = await _context.TblPositions
                    .FirstOrDefaultAsync(p => p.Name.ToLower() == tblPosition.Name.ToLower() && p.Status == 1);
                
                if (existingPosition != null)
                {
                    ModelState.AddModelError("Name", $"Chức vụ '{tblPosition.Name}' đã tồn tại và đang hoạt động. Vui lòng đặt tên chức vụ khác hoặc hết hiệu lực chức vụ hiện tại trước!");
                    return View(tblPosition);
                }
                
                // Set default status to active (1)
                tblPosition.Status = 1;
                
                _context.Add(tblPosition);
                await _context.SaveChangesAsync();
                
                TempData["AlertMessage"] = "Thêm mới chức vụ thành công!";
                TempData["AlertType"] = "success";
                
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
                // Check if position with same name already exists and is active (excluding current record)
                var existingPosition = await _context.TblPositions
                    .FirstOrDefaultAsync(p => p.Name.ToLower() == tblPosition.Name.ToLower() && p.Status == 1 && p.Id != tblPosition.Id);
                
                if (existingPosition != null)
                {
                    ModelState.AddModelError("Name", $"Chức vụ '{tblPosition.Name}' đã tồn tại và đang hoạt động. Vui lòng đặt tên chức vụ khác hoặc hết hiệu lực chức vụ hiện tại trước!");
                    return View(tblPosition);
                }
                
                try
                {
                    _context.Update(tblPosition);
                    await _context.SaveChangesAsync();
                    
                    TempData["AlertMessage"] = "Cập nhật chức vụ thành công!";
                    TempData["AlertType"] = "success";
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

        // POST: TblPosition/Deactivate/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deactivate(int id)
        {
            var tblPosition = await _context.TblPositions.FindAsync(id);
            if (tblPosition != null)
            {
                tblPosition.Status = 0; // Set status to inactive
                _context.Update(tblPosition);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: TblPosition/CheckPositionExists
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckPositionExists(string positionName, int? excludeId = null)
        {
            var query = _context.TblPositions.Where(p => p.Name.ToLower() == positionName.ToLower() && p.Status == 1);
            
            if (excludeId.HasValue)
            {
                query = query.Where(p => p.Id != excludeId.Value);
            }
            
            var existingPosition = await query.FirstOrDefaultAsync();
            
            var result = new
            {
                exists = existingPosition != null,
                isActive = existingPosition != null
            };
            
            return Json(result);
        }

        private bool TblPositionExists(int id)
        {
            return _context.TblPositions.Any(e => e.Id == id);
        }
    }
}
