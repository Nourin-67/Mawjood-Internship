using BLogicLayer.Interfaces;
using BLogicLayer.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mawjood_Internship.Controllers
{
    [Authorize]
    public class ApplicationController : Controller
    {
        private readonly IApplicationService _applicationService;
        private readonly IStudentService _studentService;

        public ApplicationController(
            IApplicationService applicationService,
            IStudentService studentService)
        {
            _applicationService = applicationService;
            _studentService = studentService;
        }


        // =========================================================
        // ADMIN - INDEX
        // =========================================================

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var applications =
                await _applicationService.GetAll();

            return View(applications);
        }


        // =========================================================
        // DETAILS
        // =========================================================

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var application =
                await _applicationService.GetById(id);

            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }


        // =========================================================
        // CREATE - ADMIN
        // =========================================================

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            ApplicationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _applicationService.Add(model);

            return RedirectToAction(nameof(Index));
        }


        // =========================================================
        // EDIT - ADMIN
        // =========================================================

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var application =
                await _applicationService.GetById(id);

            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            ApplicationViewModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _applicationService.Update(model);

            return RedirectToAction(nameof(Index));
        }


        // =========================================================
        // DELETE - ADMIN
        // =========================================================

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var application =
                await _applicationService.GetById(id);

            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deleted =
                await _applicationService.Delete(id);

            if (!deleted)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }


        // =========================================================
        // APPLY - STUDENT
        // =========================================================

        [Authorize(Roles = "User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Apply(int internshipId)
        {
            var email = User.Identity?.Name;

            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction(
                    "Login",
                    "Account");
            }

            var students =
                await _studentService.GetAll();

            var student =
                students.FirstOrDefault(x =>
                    x.Email.Equals(
                        email,
                        StringComparison.OrdinalIgnoreCase));

            if (student == null)
            {
                return NotFound();
            }

            var result =
                await _applicationService.Apply(
                    student.Id,
                    internshipId);

            TempData[result.Success
                ? "Success"
                : "Error"] = result.Message;

            return RedirectToAction(
                "Details",
                "Internship",
                new
                {
                    id = internshipId
                });
        }


        // =========================================================
        // ACCEPT - ADMIN
        // =========================================================

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Accept(int id)
        {
            var accepted =
                await _applicationService.Accept(id);

            if (!accepted)
            {
                return NotFound();
            }

            TempData["Success"] =
                "Application accepted successfully.";

            return RedirectToAction(nameof(Index));
        }


        // =========================================================
        // REJECT - ADMIN
        // =========================================================

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reject(int id)
        {
            var rejected =
                await _applicationService.Reject(id);

            if (!rejected)
            {
                return NotFound();
            }

            TempData["Success"] =
                "Application rejected successfully.";

            return RedirectToAction(nameof(Index));
        }
    }
}