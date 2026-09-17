using BLogicLayer.Interfaces;
using BLogicLayer.ViewModels;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Mawjood_Internship77.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly UserManager<ApplicationUser> _userManager;

        public StudentController(
            IStudentService studentService,
            UserManager<ApplicationUser> userManager)
        {
            _studentService = studentService;
            _userManager = userManager;
        }

        // =========================
        // STUDENT PROFILE
        // =========================

        [Authorize(Roles = "User")]
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var email = User.Identity?.Name;

            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Account");
            }

            var students = await _studentService.GetAll();

            var student = students.FirstOrDefault(
                x => x.Email.Equals(
                    email,
                    StringComparison.OrdinalIgnoreCase
                )
            );

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // =========================
        // DETAILS
        // =========================

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var student = await _studentService.GetById(id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // =========================
        // CREATE - GET
        // =========================

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // =========================
        // CREATE - POST
        // =========================

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentViewModel model)
        {
            // CV is optional for Student Create.
            // The ViewModel is kept unchanged.
            ModelState.Remove(nameof(StudentViewModel.CV));

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // =========================================
            // CHECK EMAIL IN STUDENTS TABLE
            // =========================================

            var students = await _studentService.GetAll();

            var emailExistsInStudents = students.Any(
                x => x.Email.Equals(
                    model.Email,
                    StringComparison.OrdinalIgnoreCase
                )
            );

            if (emailExistsInStudents)
            {
                ModelState.AddModelError(
                    nameof(StudentViewModel.Email),
                    "This email is already registered."
                );

                return View(model);
            }

            // =========================================
            // CHECK EMAIL IN ASP.NET IDENTITY
            // =========================================

            var identityUser = await _userManager.FindByEmailAsync(
                model.Email
            );

            if (identityUser != null)
            {
                ModelState.AddModelError(
                    nameof(StudentViewModel.Email),
                    "This email is already registered."
                );

                return View(model);
            }

            // =========================================
            // ADD STUDENT
            // =========================================

            await _studentService.Add(model);

            return RedirectToAction(nameof(Index));
        }

        // =========================
        // INDEX
        // =========================

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var students = await _studentService.GetAll();

            return View(students);
        }

        // =========================
        // DELETE - GET
        // =========================

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _studentService.GetById(id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // =========================
        // DELETE - POST
        // =========================

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _studentService.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}