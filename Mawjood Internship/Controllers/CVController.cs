using BLogicLayer.Interfaces;
using BLogicLayer.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mawjood_Internship.Controllers
{
    [Authorize(Roles = "Student")]
    public class CVController : Controller
    {
        private readonly IStudentService _studentService;

        public CVController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        private async Task<StudentViewModel?> GetCurrentStudent()
        {
            var email = User.Identity?.Name;

            if (string.IsNullOrWhiteSpace(email))
                return null;

            var students = await _studentService.GetAll();

            return students.FirstOrDefault(x =>
                x.Email.Equals(
                    email,
                    StringComparison.OrdinalIgnoreCase));
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var student = await GetCurrentStudent();

            if (student == null)
                return RedirectToAction("Login", "Account");

            var cv = await _studentService.GetCV(student.Id);

            if (cv == null)
                return NotFound();

            return View(cv);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var student = await GetCurrentStudent();

            if (student == null)
                return RedirectToAction("Login", "Account");

            var cv = await _studentService.GetCV(student.Id);

            if (cv == null)
            {
                cv = new CVViewModel
                {
                    StudentId = student.Id,
                    FName = student.FName,
                    LName = student.LName,
                    Email = student.Email,
                    University = student.University,
                    Major = student.Major,
                    AcademicYear = student.AcademicYear,
                    Education = student.Education,
                    City = student.City,
                    Government = student.Government
                };
            }

            return View(cv);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveInternalCV(
            CVViewModel model)
        {
            var student = await GetCurrentStudent();

            if (student == null)
                return RedirectToAction("Login", "Account");

            model.StudentId = student.Id;

            // These values come from the authenticated student.
            model.FName = student.FName;
            model.LName = student.LName;
            model.Email = student.Email;

            if (!ModelState.IsValid)
                return View("Create", model);

            var success =
                await _studentService.SaveInternalCV(
                    student.Id,
                    model);

            if (!success)
            {
                ModelState.AddModelError(
                    "",
                    "Could not save the CV.");

                return View("Create", model);
            }

            TempData["Success"] =
                "Your CV has been saved successfully.";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveExternalCV(
            string cvLink)
        {
            var student = await GetCurrentStudent();

            if (student == null)
                return RedirectToAction("Login", "Account");

            if (string.IsNullOrWhiteSpace(cvLink))
            {
                TempData["Error"] =
                    "Please enter a CV link.";

                return RedirectToAction(nameof(Index));
            }

            var success =
                await _studentService.SaveExternalCV(
                    student.Id,
                    cvLink);

            if (!success)
            {
                TempData["Error"] =
                    "Could not save the CV link.";

                return RedirectToAction(nameof(Index));
            }

            TempData["Success"] =
                "Your CV link has been saved successfully.";

            return RedirectToAction(nameof(Index));
        }
    }
}