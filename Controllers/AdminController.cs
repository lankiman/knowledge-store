using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using e_learning.Data;
using e_learning.Models;
using e_learning.Services.Interfaces;
using Microsoft.Identity.Client;

namespace e_learning.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILessonService _lessonService;
        private readonly IUserService _userService;

        public AdminController(ILessonService lessonService, IUserService userService)
        {
            _lessonService = lessonService;
            _userService = userService;
        }

        // GET: AdminModels
        public async Task<IActionResult> Index()
        {
            return View(await _lessonService.GetAllLessonAsync());
        }

    //     // GET: AdminModels/Details/5
    //     public async Task<IActionResult> Details(Guid? id)
    //     {
    //         if (id == null)
    //         {
    //             return NotFound();
    //         }
    //
    //         var adminModel = await _context.Administrators
    //             .FirstOrDefaultAsync(m => m.Id == id);
    //         if (adminModel == null)
    //         {
    //             return NotFound();
    //         }
    //
    //         return View(adminModel);
    //     }
    //
    //     // GET: AdminModels/Create
    //     public IActionResult Create()
    //     {
    //         return View();
    //     }
    //
    //     // POST: AdminModels/Create
    //     // To protect from overposting attacks, enable the specific properties you want to bind to.
    //     // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    //     [HttpPost]
    //     [ValidateAntiForgeryToken]
    //     public async Task<IActionResult> Create(
    //         [Bind("IsAdmin,Id,Username,FirstName,LastName,MiddleName,Email,Password,PhoneNumber")]
    //         AdminModel adminModel)
    //     {
    //         if (ModelState.IsValid)
    //         {
    //             adminModel.Id = Guid.NewGuid();
    //             _context.Add(adminModel);
    //             await _context.SaveChangesAsync();
    //             return RedirectToAction(nameof(Index));
    //         }
    //
    //         return View(adminModel);
    //     }
    //
    //     // GET: AdminModels/Edit/5
    //     public async Task<IActionResult> Edit(Guid? id)
    //     {
    //         if (id == null)
    //         {
    //             return NotFound();
    //         }
    //
    //         var adminModel = await _context.Administrators.FindAsync(id);
    //         if (adminModel == null)
    //         {
    //             return NotFound();
    //         }
    //
    //         return View(adminModel);
    //     }
    //
    //     // POST: AdminModels/Edit/5
    //     // To protect from overposting attacks, enable the specific properties you want to bind to.
    //     // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    //     [HttpPost]
    //     [ValidateAntiForgeryToken]
    //     public async Task<IActionResult> Edit(Guid id,
    //         [Bind("IsAdmin,Id,Username,FirstName,LastName,MiddleName,Email,Password,PhoneNumber")]
    //         AdminModel adminModel)
    //     {
    //         if (id != adminModel.Id)
    //         {
    //             return NotFound();
    //         }
    //
    //         if (ModelState.IsValid)
    //         {
    //             try
    //             {
    //                 _context.Update(adminModel);
    //                 await _context.SaveChangesAsync();
    //             }
    //             catch (DbUpdateConcurrencyException)
    //             {
    //                 if (!AdminModelExists(adminModel.Id))
    //                 {
    //                     return NotFound();
    //                 }
    //                 else
    //                 {
    //                     throw;
    //                 }
    //             }
    //
    //             return RedirectToAction(nameof(Index));
    //         }
    //
    //         return View(adminModel);
    //     }
    //
    //     // GET: AdminModels/Delete/5
    //     public async Task<IActionResult> Delete(Guid? id)
    //     {
    //         if (id == null)
    //         {
    //             return NotFound();
    //         }
    //
    //         var adminModel = await _context.Administrators
    //             .FirstOrDefaultAsync(m => m.Id == id);
    //         if (adminModel == null)
    //         {
    //             return NotFound();
    //         }
    //
    //         return View(adminModel);
    //     }
    //
    //     // POST: AdminModels/Delete/5
    //     [HttpPost, ActionName("Delete")]
    //     [ValidateAntiForgeryToken]
    //     public async Task<IActionResult> DeleteConfirmed(Guid id)
    //     {
    //         var adminModel = await _context.Administrators.FindAsync(id);
    //         if (adminModel != null)
    //         {
    //             _context.Administrators.Remove(adminModel);
    //         }
    //
    //         await _context.SaveChangesAsync();
    //         return RedirectToAction(nameof(Index));
    //     }
    //
    //     private bool AdminModelExists(Guid id)
    //     {
    //         return _context.Administrators.Any(e => e.Id == id);
    //     }
    // }
}