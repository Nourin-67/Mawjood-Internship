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
        public InternshipController(
            IInternshipService internshipService)
        {
            _internshipService = internshipService;
        }

        // GET: Internship
        public async Task<IActionResult> Index()
        {
            var internships =
                await _internshipService.GetAll();

            return View(internships);
        }

        // GET: Internship/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var internship =
                await _internshipService.GetById(id);

            if (internship == null)
                return NotFound();

            return View(internship);
        }

        // GET: Internship/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Internship/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(
            InternshipViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _internshipService.Add(model);

            return RedirectToAction(nameof(Index));
        }

        // GET: Internship/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var internship =
                await _internshipService.GetById(id);

            if (internship == null)
                return NotFound();

            return View(internship);
        }

        // POST: Internship/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
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

        // GET: Internship/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var internship =
                await _internshipService.GetById(id);

            if (internship == null)
                return NotFound();

            return View(internship);
        }

        // POST: Internship/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
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