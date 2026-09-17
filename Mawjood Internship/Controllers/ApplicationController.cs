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
        private readonly IApplicationWorkflowService
            _workflowService;
        private readonly IStudentService _studentService;

        public ApplicationController(
            IApplicationService applicationService,
            IApplicationWorkflowService workflowService,
            IStudentService studentService)
        {
            _applicationService = applicationService;
            _workflowService = workflowService;
            _studentService = studentService;
        }

        // ================================
        // ADMIN APPLICATIONS
        // ================================

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var applications =
                await _workflowService.GetAllAsync();

            return View(applications);
        }

        // ================================
        // STUDENT MY APPLICATIONS
        // ================================

        [Authorize(Roles = "Student")]
        [HttpGet]
        public async Task<IActionResult> MyApplications()
        {
            var email = User.Identity?.Name;

            if (string.IsNullOrWhiteSpace(email))
                return RedirectToAction(
                    "Login",
                    "Account");

            var students =
                await _studentService.GetAll();

            var student =
                students.FirstOrDefault(x =>
                    x.Email.Equals(
                        email,
                        StringComparison.OrdinalIgnoreCase));

            if (student == null)
                return NotFound();

            var applications =
                await _workflowService
                    .GetForStudentAsync(student.Id);

            return View(applications);
        }

        // ================================
        // STUDENT APPLY
        // ================================

        [Authorize(Roles = "Student")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Apply(
            int internshipId)
        {
            var email = User.Identity?.Name;

            if (string.IsNullOrWhiteSpace(email))
                return RedirectToAction(
                    "Login",
                    "Account");

            var students =
                await _studentService.GetAll();

            var student =
                students.FirstOrDefault(x =>
                    x.Email.Equals(
                        email,
                        StringComparison.OrdinalIgnoreCase));

            if (student == null)
                return NotFound();

            var result =
                await _workflowService.ApplyAsync(
                    student.Id,
                    internshipId);

            if (!result.Succeeded)
            {
                TempData["Error"] = result.Error;

                return RedirectToAction(
                    "Index",
                    "Internship");
            }

            TempData["Success"] =
                "Application submitted successfully.";

            return RedirectToAction(
                "MyApplications");
        }

        // ================================
        // ADMIN DETAILS
        // ================================

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var application =
                await _applicationService.GetById(id);

            if (application == null)
                return NotFound();

            return View(application);
        }

        // ================================
        // ADMIN CREATE
        // ================================

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
                return View(model);

            await _applicationService.Add(model);

            return RedirectToAction(nameof(Index));
        }

        // ================================
        // ADMIN EDIT
        // ================================

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var application =
                await _applicationService.GetById(id);

            if (application == null)
                return NotFound();

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
                return BadRequest();

            if (!ModelState.IsValid)
                return View(model);

            await _applicationService.Update(model);

            return RedirectToAction(nameof(Index));
        }

        // ================================
        // ADMIN DELETE
        // ================================

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var application =
                await _applicationService.GetById(id);

            if (application == null)
                return NotFound();

            return View(application);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(
            int id)
        {
            var deleted =
                await _applicationService.Delete(id);

            if (!deleted)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

        // ================================
        // ADMIN ACCEPT
        // ================================

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Accept(int id)
        {
            var accepted =
                await _workflowService.AcceptAsync(id);

            if (!accepted)
                return NotFound();

            TempData["Success"] =
                "Application accepted successfully.";

            return RedirectToAction(nameof(Index));
        }

        // ================================
        // ADMIN REJECT
        // ================================

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reject(int id)
        {
            var rejected =
                await _workflowService.RejectAsync(id);

            if (!rejected)
                return NotFound();

            TempData["Success"] =
                "Application rejected successfully.";

            return RedirectToAction(nameof(Index));
        }
    }
}