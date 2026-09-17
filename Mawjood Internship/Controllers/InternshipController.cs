using BLogicLayer.Interfaces;
using BLogicLayer.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mawjood_Internship.Controllers
{
    [Authorize]
    public class InternshipController : Controller
    {
        private readonly IInternshipService _internshipService;
        private readonly IInternshipRecommendationService
            _recommendationService;

        public InternshipController(
            IInternshipService internshipService,
            IInternshipRecommendationService recommendationService)
        {
            _internshipService = internshipService;
            _recommendationService = recommendationService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Student"))
            {
                var email = User.Identity?.Name;

                if (string.IsNullOrWhiteSpace(email))
                    return RedirectToAction(
                        "Login",
                        "Account");

                var studentService =
                    HttpContext.RequestServices
                        .GetRequiredService<IStudentService>();

                var students =
                    await studentService.GetAll();

                var student =
                    students.FirstOrDefault(x =>
                        x.Email.Equals(
                            email,
                            StringComparison.OrdinalIgnoreCase));

                if (student == null)
                    return NotFound();

                var browse =
                    await _recommendationService
                        .GetForStudentAsync(student.Id);

                return View(browse);
            }

            var internships =
                await _internshipService.GetAll();

            var adminBrowse =
                new InternshipBrowseViewModel
                {
                    AllInternships =
                        internships.Select(x =>
                            new RecommendedInternshipViewModel
                            {
                                Id = x.Id,
                                Title = x.Title,
                                Location = x.Location,
                                CompanyName =
                                    x.CompanyName
                            }).ToList()
                };

            return View(adminBrowse);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var internship =
                await _internshipService.GetById(id);

            if (internship == null)
                return NotFound();

            return View(internship);
        }

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
            InternshipViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _internshipService.Add(model);

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var internship =
                await _internshipService.GetById(id);

            if (internship == null)
                return NotFound();

            return View(internship);
        }

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
                return View(model);

            await _internshipService.Update(model);

            return RedirectToAction(nameof(Index));
        }

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

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(
            int id)
        {
            var deleted =
                await _internshipService.Delete(id);

            if (!deleted)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}