using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PMS.Common;
using PMS.Models;

namespace PMS.Controllers
{
    public class LoginController : Controller
    {
        private readonly PMSDbContext _context;

        public LoginController(PMSDbContext context)
        {
            _context = context;
        }

        // GET: Login
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string txtUser, string txtPass)
        {

            var result = await _context.TblUsers.Where(x => x.AccountName == txtUser && x.Password == txtPass && x.Status == 1).FirstOrDefaultAsync();
            if (result != null)
            {
                HttpContext.Session.SetString(CommonConstants.UserName, result.Name);
                HttpContext.Session.SetString(CommonConstants.UserId, result.Id.ToString());
                return RedirectToAction("Index","Home"); 
            }
            ModelState.AddModelError("", "Thông tin tài khoản hoặc mật khẩu chưa chính xác");
            return View();
        }
            // GET: Login/Details/5
            public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblUser = await _context.TblUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblUser == null)
            {
                return NotFound();
            }

            return View(tblUser);
        }

        // GET: Login/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Login/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,UserCode,Dob,BankNo,SalaryConfficient,EmploymentDate,DateIssued,AccountName,Password,EmailAddress,Description,OrderNumber,Status,CreatedDate,CreatedUser,ModifiedDate,ModifiedUser")] TblUser tblUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblUser);
        }

        // GET: Login/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblUser = await _context.TblUsers.FindAsync(id);
            if (tblUser == null)
            {
                return NotFound();
            }
            return View(tblUser);
        }

        // POST: Login/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,UserCode,Dob,BankNo,SalaryConfficient,EmploymentDate,DateIssued,AccountName,Password,EmailAddress,Description,OrderNumber,Status,CreatedDate,CreatedUser,ModifiedDate,ModifiedUser")] TblUser tblUser)
        {
            if (id != tblUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblUserExists(tblUser.Id))
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
            return View(tblUser);
        }

        // GET: Login/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblUser = await _context.TblUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblUser == null)
            {
                return NotFound();
            }

            return View(tblUser);
        }

        // POST: Login/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblUser = await _context.TblUsers.FindAsync(id);
            _context.TblUsers.Remove(tblUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblUserExists(int id)
        {
            return _context.TblUsers.Any(e => e.Id == id);
        }
    }
}
