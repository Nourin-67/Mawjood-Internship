



//using BLogicLayer.Interfaces;
//using BLogicLayer.ViewModels;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace Mawjood_Internship.Controllers
//{
//    [Authorize(Roles = "User")]
//    public class CVController : Controller
//    {
//        private readonly IStudentService _studentService;

//        public CVController(IStudentService studentService)
//        {
//            _studentService = studentService;
//        }

//        // =========================================
//        // MY CV
//        // =========================================

//        [HttpGet]
//        public async Task<IActionResult> Index()
//        {
//            var email = User.Identity?.Name;

//            if (string.IsNullOrEmpty(email))
//            {
//                return RedirectToAction(
//                    "Login",
//                    "Account"
//                );
//            }

//            var students =
//                await _studentService.GetAll();

//            var student =
//                students.FirstOrDefault(
//                    x => x.Email.Equals(
//                        email,
//                        StringComparison.OrdinalIgnoreCase
//                    )
//                );

//            if (student == null)
//            {
//                return NotFound();
//            }

//            var cv =
//                await _studentService.GetCV(
//                    student.Id
//                );

//            if (cv == null)
//            {
//                cv = new CVViewModel
//                {
//                    StudentId = student.Id,
//                    FName = student.FName,
//                    LName = student.LName,
//                    Email = student.Email,
//                    University = student.University,
//                    Major = student.Major,
//                    AcademicYear = student.AcademicYear,
//                    Education = student.Education,
//                    City = student.City,
//                    Government = student.Government
//                };
//            }

//            return View(cv);
//        }


//        // =========================================
//        // CREATE CV
//        // =========================================

//        [HttpGet]
//        public async Task<IActionResult> Create()
//        {
//            var email = User.Identity?.Name;

//            if (string.IsNullOrEmpty(email))
//            {
//                return RedirectToAction(
//                    "Login",
//                    "Account"
//                );
//            }

//            var students =
//                await _studentService.GetAll();

//            var student =
//                students.FirstOrDefault(
//                    x => x.Email.Equals(
//                        email,
//                        StringComparison.OrdinalIgnoreCase
//                    )
//                );

//            if (student == null)
//            {
//                return NotFound();
//            }

//            var model = new CVViewModel
//            {
//                StudentId = student.Id,
//                FName = student.FName,
//                LName = student.LName,
//                Email = student.Email,
//                University = student.University,
//                Major = student.Major,
//                AcademicYear = student.AcademicYear,
//                Education = student.Education,
//                City = student.City,
//                Government = student.Government
//            };

//            return View(model);
//        }


//        // =========================================
//        // SAVE INTERNAL CV
//        // =========================================

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> SaveInternalCV(
//            CVViewModel model)
//        {
//            var email = User.Identity?.Name;

//            if (string.IsNullOrEmpty(email))
//            {
//                return RedirectToAction(
//                    "Login",
//                    "Account"
//                );
//            }

//            var students =
//                await _studentService.GetAll();

//            var student =
//                students.FirstOrDefault(
//                    x => x.Email.Equals(
//                        email,
//                        StringComparison.OrdinalIgnoreCase
//                    )
//                );

//            if (student == null)
//            {
//                return NotFound();
//            }

//            model.StudentId = student.Id;
//            model.FName = student.FName;
//            model.LName = student.LName;
//            model.Email = student.Email;

//            if (!ModelState.IsValid)
//            {
//                return View("Create", model);
//            }

//            var success =
//                await _studentService.SaveInternalCV(
//                    student.Id,
//                    model
//                );

//            if (!success)
//            {
//                ModelState.AddModelError(
//                    "",
//                    "Could not save the CV."
//                );

//                return View("Create", model);
//            }

//            return RedirectToAction(nameof(Index));
//        }


//        // =========================================
//        // SAVE EXTERNAL CV LINK
//        // =========================================

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> SaveExternalCV(
//            string cvLink)
//        {
//            var email = User.Identity?.Name;

//            if (string.IsNullOrEmpty(email))
//            {
//                return RedirectToAction(
//                    "Login",
//                    "Account"
//                );
//            }

//            var students =
//                await _studentService.GetAll();

//            var student =
//                students.FirstOrDefault(
//                    x => x.Email.Equals(
//                        email,
//                        StringComparison.OrdinalIgnoreCase
//                    )
//                );

//            if (student == null)
//            {
//                return NotFound();
//            }

//            if (string.IsNullOrWhiteSpace(cvLink))
//            {
//                TempData["Error"] =
//                    "Please enter a CV link.";

//                return RedirectToAction(nameof(Index));
//            }

//            var success =
//                await _studentService.SaveExternalCV(
//                    student.Id,
//                    cvLink
//                );

//            if (!success)
//            {
//                TempData["Error"] =
//                    "Could not save the CV link.";

//                return RedirectToAction(nameof(Index));
//            }

//            return RedirectToAction(nameof(Index));
//        }
//    }
using BLogicLayer.Interfaces;
using BLogicLayer.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mawjood_Internship.Controllers
{
    [Authorize(Roles = "User")]
    public class CVController : Controller
    {
        private readonly IStudentService _studentService;

        public CVController(
            IStudentService studentService)
        {
            _studentService = studentService;
        }

        // =========================================
        // MY CV
        // =========================================

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var email = User.Identity?.Name;

            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction(
                    "Login",
                    "Account"
                );
            }

            var students =
                await _studentService.GetAll();

            var student =
                students.FirstOrDefault(
                    x => x.Email.Equals(
                        email,
                        StringComparison.OrdinalIgnoreCase
                    )
                );

            if (student == null)
            {
                return NotFound();
            }

            var cv =
                await _studentService.GetCV(
                    student.Id
                );

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


        // =========================================
        // CREATE CV PAGE
        // =========================================

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var email = User.Identity?.Name;

            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction(
                    "Login",
                    "Account"
                );
            }

            var students =
                await _studentService.GetAll();

            var student =
                students.FirstOrDefault(
                    x => x.Email.Equals(
                        email,
                        StringComparison.OrdinalIgnoreCase
                    )
                );

            if (student == null)
            {
                return NotFound();
            }

            var model = new CVViewModel
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

            return View(model);
        }


        // =========================================
        // SAVE INTERNAL CV
        // =========================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveInternalCV(
            CVViewModel model)
        {
            var email = User.Identity?.Name;

            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction(
                    "Login",
                    "Account"
                );
            }

            var students =
                await _studentService.GetAll();

            var student =
                students.FirstOrDefault(
                    x => x.Email.Equals(
                        email,
                        StringComparison.OrdinalIgnoreCase
                    )
                );

            if (student == null)
            {
                return NotFound();
            }

            // Never trust StudentId coming from the form
            model.StudentId = student.Id;

            model.FName = student.FName;
            model.LName = student.LName;
            model.Email = student.Email;

            if (!ModelState.IsValid)
            {
                return View("Create", model);
            }

            var success =
                await _studentService.SaveInternalCV(
                    student.Id,
                    model
                );

            if (!success)
            {
                ModelState.AddModelError(
                    "",
                    "Could not save the CV."
                );

                return View("Create", model);
            }

            TempData["Success"] =
                "Your CV has been created successfully.";

            return RedirectToAction(nameof(Index));
        }


        // =========================================
        // SAVE EXTERNAL CV LINK
        // =========================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveExternalCV(
            string cvLink)
        {
            var email = User.Identity?.Name;

            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction(
                    "Login",
                    "Account"
                );
            }

            var students =
                await _studentService.GetAll();

            var student =
                students.FirstOrDefault(
                    x => x.Email.Equals(
                        email,
                        StringComparison.OrdinalIgnoreCase
                    )
                );

            if (student == null)
            {
                return NotFound();
            }

            if (string.IsNullOrWhiteSpace(cvLink))
            {
                TempData["Error"] =
                    "Please enter a CV link.";

                return RedirectToAction(nameof(Index));
            }

            var success =
                await _studentService.SaveExternalCV(
                    student.Id,
                    cvLink
                );

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