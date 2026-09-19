using BLogicLayer.Interfaces;
using BLogicLayer.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mawjood_Internship77.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
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
        // EDIT - GET
        // =========================

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var student = await _studentService.GetById(id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // =========================
        // EDIT - POST
        // =========================

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            StudentViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            // CV is optional for Student Edit.
            // The ViewModel is kept unchanged.
            ModelState.Remove(nameof(StudentViewModel.CV));

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _studentService.Update(model);

            return RedirectToAction(nameof(Index));
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