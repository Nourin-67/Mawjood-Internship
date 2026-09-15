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
        public ApplicationController(
            IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        // GET: Application
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var applications =
                await _applicationService.GetAll();

            return View(applications);
        }

        // GET: Application/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var application =
                await _applicationService.GetById(id);

            if (application == null)
                return NotFound();

            return View(application);
        }

        // GET: Application/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Application/Create
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

        // GET: Application/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var application =
                await _applicationService.GetById(id);

            if (application == null)
                return NotFound();

            return View(application);
        }

        // POST: Application/Edit/5
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

        // GET: Application/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var application =
                await _applicationService.GetById(id);

            if (application == null)
                return NotFound();

            return View(application);
        }

        // POST: Application/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deleted =
                await _applicationService.Delete(id);

            if (!deleted)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

        // POST: Application/Reject/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Reject(int id)
        {
            var rejected =
                await _applicationService.Reject(id);

            if (!rejected)
                return NotFound();

            TempData["Success"] =
                "Application rejected successfully.";

            return RedirectToAction(nameof(Index));
        }
    }
}