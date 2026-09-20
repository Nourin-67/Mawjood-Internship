using BLogicLayer.Interfaces;
using BLogicLayer.Services;
using BLogicLayer.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mawjood_Internship77.Controllers
{
    [Authorize]
    public class InternshipController : Controller
    {
        private readonly IInternshipService _internshipService;
        private readonly IApplicationService _applicationService;
        private readonly IStudentService _studentService;

        public InternshipController(
            IInternshipService internshipService,
            IApplicationService applicationService,
            IStudentService studentService)
        {
            _internshipService = internshipService;
            _applicationService = applicationService;
            _studentService = studentService;
        }


        // =========================================================
        // INDEX
        // =========================================================

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var internships = await _internshipService.GetAll();

            return View(internships);
        }


        // =========================================================
        // DETAILS
        // =========================================================

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var internship =
                await _internshipService.GetById(id);

            if (internship == null)
            {
                return NotFound();
            }

            if (User.Identity?.IsAuthenticated == true &&
                User.IsInRole("User"))
            {
                var email = User.Identity.Name;

                if (!string.IsNullOrEmpty(email))
                {
                    var students =
                        await _studentService.GetAll();

                    var student =
                        students.FirstOrDefault(x =>
                            x.Email.Equals(
                                email,
                                StringComparison.OrdinalIgnoreCase));

                    if (student != null)
                    {
                        internship.HasApplied =
                            await _applicationService.HasApplied(
                                student.Id,
                                internship.Id);
                    }
                }
            }

            return View(internship);
        }

        // =========================================================
        // CREATE - GET
        // =========================================================

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        // =========================================================
        // CREATE - POST
        // =========================================================

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InternshipViewModel model)
        {
            // CompanyName is Display Only.
            // It is not entered by the user and should not be required
            // during Create.
            ModelState.Remove(nameof(InternshipViewModel.CompanyName));

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _internshipService.Add(model);

            return RedirectToAction(nameof(Index));
        }


        // =========================================================
        // EDIT - GET
        // =========================================================

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var internship = await _internshipService.GetById(id);

            if (internship == null)
            {
                return NotFound();
            }

            return View(internship);
        }


        // =========================================================
        // EDIT - POST
        // =========================================================

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            InternshipViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            // CompanyName is Display Only.
            // It is not edited by the user and should not be required.
            ModelState.Remove(nameof(InternshipViewModel.CompanyName));

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _internshipService.Update(model);

            return RedirectToAction(nameof(Index));
        }


        // =========================================================
        // DELETE - GET
        // =========================================================

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var internship = await _internshipService.GetById(id);

            if (internship == null)
            {
                return NotFound();
            }

            return View(internship);
        }


        // =========================================================
        // DELETE - POST
        // =========================================================

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var internship = await _internshipService.GetById(id);

            if (internship == null)
            {
                return NotFound();
            }

            await _internshipService.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}