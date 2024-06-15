using e_learning.DataTransfersObjects;
using e_learning.Models;
using Microsoft.AspNetCore.Mvc;
using e_learning.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace e_learning.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController(
        ILessonService lessonService,
        IUserService userService,
        IAdminService adminService,
        UserManager<UserModel> userManager) : Controller
    {
        // GET: AdminModels
        public async Task<IActionResult> AdminDashboard()
        {
            var user = await adminService.GetAuthenticatedAdmin();

            return View(user);
        }

        // GET: AdminModels
        // public async Task<IActionResult> Index()
        // {
        //     return View(await _lessonService.GetAllLessonAsync());
        // }

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
}