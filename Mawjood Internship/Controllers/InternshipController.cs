using BLogicLayer.Interfaces;
using BLogicLayer.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Mawjood_Internship.Controllers
{
    [Authorize]
    public class InternshipController : Controller
    {
        private readonly IInternshipService _internshipService;
        private readonly IInternshipRecommendationService _recommendationService;
        private readonly ISkillService _skillService;

        public InternshipController(
            IInternshipService internshipService,
            IInternshipRecommendationService recommendationService,
            ISkillService skillService)
        {
            _internshipService = internshipService;
            _recommendationService = recommendationService;
            _skillService = skillService;
        }

        // =========================================================
        // INDEX
        // =========================================================

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // -----------------------------------------------------
            // ADMIN
            // -----------------------------------------------------
            if (User.IsInRole("Admin"))
            {
                var internships =
                    await _internshipService.GetAll();

                var model = new InternshipBrowseViewModel
                {
                    AdminInternships = internships
                };

                return View(model);
            }

            // -----------------------------------------------------
            // STUDENT
            // -----------------------------------------------------
            if (User.IsInRole("Student"))
            {
                var userIdClaim =
                    User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (!int.TryParse(userIdClaim, out int studentId))
                {
                    return RedirectToAction(
                        "Login",
                        "Account");
                }

                var model =
                    await _recommendationService
                        .GetForStudentAsync(studentId);

                return View(model);
            }

            return Forbid();
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
                return NotFound();

            ViewBag.Skills =
                await _skillService.GetAll();

            return View(internship);
        }


        // =========================================================
        // CREATE - GET
        // =========================================================

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Skills =
                await _skillService.GetAll();

            return View();
        }


        // =========================================================
        // CREATE - POST
        // =========================================================

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            InternshipViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Skills =
                    await _skillService.GetAll();

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
            var internship =
                await _internshipService.GetById(id);

            if (internship == null)
                return NotFound();

            ViewBag.Skills =
                await _skillService.GetAll();

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
                return BadRequest();

            if (!ModelState.IsValid)
            {
                ViewBag.Skills =
                    await _skillService.GetAll();

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
            var internship =
                await _internshipService.GetById(id);

            if (internship == null)
                return NotFound();

            return View(internship);
        }


        // =========================================================
        // DELETE - POST
        // =========================================================

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deleted =
                await _internshipService.Delete(id);

            if (!deleted)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}