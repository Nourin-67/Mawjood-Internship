using BLogicLayer.Interfaces;
using BLogicLayer.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mawjood_Internship77.Controllers
{
    [Authorize]
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        // =========================================================
        // INDEX
        // =========================================================

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var companies = await _companyService.GetAll();

            return View(companies);
        }

        // =========================================================
        // DETAILS
        // =========================================================

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var company = await _companyService.GetById(id);

            if (company == null)
            {
                return NotFound();
            }

            return View(company);
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
        public async Task<IActionResult> Create(CompanyViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _companyService.Add(model);

            return RedirectToAction(nameof(Index));
        }

        // =========================================================
        // EDIT - GET
        // =========================================================

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var company = await _companyService.GetById(id);

            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // =========================================================
        // EDIT - POST
        // =========================================================

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            CompanyViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _companyService.Update(model);

            return RedirectToAction(nameof(Index));
        }

        // =========================================================
        // DELETE - GET
        // =========================================================

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var company = await _companyService.GetById(id);

            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // =========================================================
        // DELETE - POST
        // =========================================================

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var company = await _companyService.GetById(id);

            if (company == null)
            {
                return NotFound();
            }

            await _companyService.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}