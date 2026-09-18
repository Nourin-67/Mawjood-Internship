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

        private readonly IWebHostEnvironment _environment;

        public CVController(
            IStudentService studentService,
            IWebHostEnvironment environment)
        {
            _studentService =
                studentService;

            _environment =
                environment;
        }

        // =========================================
        // MY CV / PROFILE
        // =========================================

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var email =
                User.Identity?.Name;

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
                    x =>
                        x.Email.Equals(
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
                    StudentId =
                        student.Id,

                    FName =
                        student.FName,

                    LName =
                        student.LName,

                    Email =
                        student.Email,

                    University =
                        student.University,

                    Major =
                        student.Major,

                    AcademicYear =
                        student.AcademicYear,

                    Education =
                        student.Education,

                    City =
                        student.City,

                    Government =
                        student.Government
                };
            }

            return View(cv);
        }

        // =========================================
        // CREATE / EDIT CV
        // =========================================

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var email =
                User.Identity?.Name;

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
                    x =>
                        x.Email.Equals(
                            email,
                            StringComparison.OrdinalIgnoreCase
                        )
                );

            if (student == null)
            {
                return NotFound();
            }

            var existingCV =
                await _studentService.GetCV(
                    student.Id
                );

            if (existingCV != null)
            {
                return View(existingCV);
            }

            var model =
                new CVViewModel
                {
                    StudentId =
                        student.Id,

                    FName =
                        student.FName,

                    LName =
                        student.LName,

                    Email =
                        student.Email,

                    University =
                        student.University,

                    Major =
                        student.Major,

                    AcademicYear =
                        student.AcademicYear,

                    Education =
                        student.Education,

                    City =
                        student.City,

                    Government =
                        student.Government
                };

            return View(model);
        }

        // =========================================
        // SAVE CV FILE + EXTERNAL LINK
        // =========================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveCV(
            CVViewModel model)
        {
            // =====================================
            // Get logged-in user's email
            // =====================================

            var email =
                User.Identity?.Name;

            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction(
                    "Login",
                    "Account"
                );
            }

            // =====================================
            // Find student
            // =====================================

            var students =
                await _studentService.GetAll();

            var student =
                students.FirstOrDefault(
                    x =>
                        x.Email.Equals(
                            email,
                            StringComparison.OrdinalIgnoreCase
                        )
                );

            if (student == null)
            {
                return NotFound();
            }

            // NEVER trust StudentId from form

            model.StudentId =
                student.Id;

            // Keep personal information
            // from logged-in student

            model.FName =
                student.FName;

            model.LName =
                student.LName;

            model.Email =
                student.Email;

            // =====================================
            // Validate External Link
            // =====================================

            if (!string.IsNullOrWhiteSpace(
                model.ExternalCVLink))
            {
                if (!Uri.TryCreate(
                        model.ExternalCVLink.Trim(),
                        UriKind.Absolute,
                        out var uri)
                    ||
                    (uri.Scheme != Uri.UriSchemeHttp &&
                     uri.Scheme != Uri.UriSchemeHttps))
                {
                    ModelState.AddModelError(
                        nameof(model.ExternalCVLink),
                        "Please enter a valid CV link."
                    );
                }
            }

            // =====================================
            // Validate PDF
            // =====================================

            if (model.CVFile != null &&
                model.CVFile.Length > 0)
            {
                // Maximum 5 MB

                const long maxFileSize =
                    5 * 1024 * 1024;

                if (model.CVFile.Length >
                    maxFileSize)
                {
                    ModelState.AddModelError(
                        nameof(model.CVFile),
                        "CV file size cannot exceed 5 MB."
                    );
                }

                // Get extension

                var extension =
                    Path.GetExtension(
                        model.CVFile.FileName
                    );

                if (!extension.Equals(
                        ".pdf",
                        StringComparison.OrdinalIgnoreCase))
                {
                    ModelState.AddModelError(
                        nameof(model.CVFile),
                        "Only PDF files are allowed."
                    );
                }
            }

            // =====================================
            // Model validation
            // =====================================

            if (!ModelState.IsValid)
            {
                return View(
                    "Create",
                    model
                );
            }

            // =====================================
            // Save External CV Link
            // =====================================

            if (!string.IsNullOrWhiteSpace(
                model.ExternalCVLink))
            {
                var linkSuccess =
                    await _studentService.SaveExternalCV(
                        student.Id,
                        model.ExternalCVLink.Trim()
                    );

                if (!linkSuccess)
                {
                    ModelState.AddModelError(
                        "",
                        "Could not save the external CV link."
                    );

                    return View(
                        "Create",
                        model
                    );
                }
            }

            // =====================================
            // Save Uploaded PDF
            // =====================================

            if (model.CVFile != null &&
                model.CVFile.Length > 0)
            {
                // wwwroot/uploads/cvs

                var uploadFolder =
                    Path.Combine(
                        _environment.WebRootPath,
                        "uploads",
                        "cvs"
                    );

                // Create folder if it doesn't exist

                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(
                        uploadFolder
                    );
                }

                // Generate safe unique file name

                var uniqueFileName =
                    Guid.NewGuid().ToString()
                    + ".pdf";

                var physicalPath =
                    Path.Combine(
                        uploadFolder,
                        uniqueFileName
                    );

                // Save file

                using (var stream =
                       new FileStream(
                           physicalPath,
                           FileMode.Create))
                {
                    await model.CVFile.CopyToAsync(
                        stream
                    );
                }

                // Path saved in database

                var relativePath =
                    "/uploads/cvs/"
                    + uniqueFileName;

                var fileSuccess =
                    await _studentService.SaveCVFile(
                        student.Id,
                        relativePath
                    );

                if (!fileSuccess)
                {
                    // Delete uploaded file
                    // if DB save failed

                    if (System.IO.File.Exists(
                        physicalPath))
                    {
                        System.IO.File.Delete(
                            physicalPath
                        );
                    }

                    ModelState.AddModelError(
                        "",
                        "Could not save the CV file."
                    );

                    return View(
                        "Create",
                        model
                    );
                }
            }

            TempData["Success"] =
                "Your CV has been saved successfully.";

            return RedirectToAction(
                nameof(Index)
            );
        }

        // =========================================
        // OLD SAVE INTERNAL CV
        // Keep it because your existing
        // Internal CV system uses it.
        // =========================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveInternalCV(
            CVViewModel model)
        {
            var email =
                User.Identity?.Name;

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
                    x =>
                        x.Email.Equals(
                            email,
                            StringComparison.OrdinalIgnoreCase
                        )
                );

            if (student == null)
            {
                return NotFound();
            }

            model.StudentId =
                student.Id;

            model.FName =
                student.FName;

            model.LName =
                student.LName;

            model.Email =
                student.Email;

            if (!ModelState.IsValid)
            {
                return View(
                    "Create",
                    model
                );
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

                return View(
                    "Create",
                    model
                );
            }

            TempData["Success"] =
                "Your CV has been created successfully.";

            return RedirectToAction(
                nameof(Index)
            );
        }

        // =========================================
        // OLD EXTERNAL CV ACTION
        // Keep it for compatibility
        // =========================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveExternalCV(
            string cvLink)
        {
            var email =
                User.Identity?.Name;

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
                    x =>
                        x.Email.Equals(
                            email,
                            StringComparison.OrdinalIgnoreCase
                        )
                );

            if (student == null)
            {
                return NotFound();
            }

            if (string.IsNullOrWhiteSpace(
                cvLink))
            {
                TempData["Error"] =
                    "Please enter a CV link.";

                return RedirectToAction(
                    nameof(Index)
                );
            }

            if (!Uri.TryCreate(
                    cvLink.Trim(),
                    UriKind.Absolute,
                    out var uri)
                ||
                (uri.Scheme != Uri.UriSchemeHttp &&
                 uri.Scheme != Uri.UriSchemeHttps))
            {
                TempData["Error"] =
                    "Please enter a valid CV link.";

                return RedirectToAction(
                    nameof(Index)
                );
            }

            var success =
                await _studentService.SaveExternalCV(
                    student.Id,
                    cvLink.Trim()
                );

            if (!success)
            {
                TempData["Error"] =
                    "Could not save the CV link.";

                return RedirectToAction(
                    nameof(Index)
                );
            }

            TempData["Success"] =
                "Your CV link has been saved successfully.";

            return RedirectToAction(
                nameof(Index)
            );
        }
    }
}