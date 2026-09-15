//using BLogicLayer.Interfaces;
//using BLogicLayer.ViewModels;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//namespace PerstionLayer.Controllers
//{
//    [Authorize]
//    public class StudentController : Controller
//    {
//        private readonly IStudentService _studentService;
//        public StudentController(IStudentService studentService)
//        {
//            _studentService = studentService;
//        }

//        // GET: Student
//        public async Task<IActionResult> Index()
//        {
//            var students = await _studentService.GetAll();

//            return View(students);
//        }

//        // GET: Student/Details/5
//        public async Task<IActionResult> Details(int id)
//        {
//            var student = await _studentService.GetById(id);

//            if (student == null)
//                return NotFound();

//            return View(student);
//        }

//        // GET: Student/Create
//        [Authorize(Roles = "Admin")]
//        public IActionResult Create()
//        {
//            return View();
//        }

//        // POST: Student/Create
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        [Authorize(Roles = "Admin")]
//        public async Task<IActionResult> Create(StudentViewModel model)
//        {
//            if (!ModelState.IsValid)
//                return View(model);

//            await _studentService.Add(model);

//            return RedirectToAction(nameof(Index));
//        }

//        // GET: Student/Edit/5
//        [Authorize(Roles = "Admin")]
//        public async Task<IActionResult> Edit(int id)
//        {
//            var student = await _studentService.GetById(id);

//            if (student == null)
//                return NotFound();

//            return View(student);
//        }

//        // POST: Student/Edit/5
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        [Authorize(Roles = "Admin")]
//        public async Task<IActionResult> Edit(
//            int id,
//            StudentViewModel model)
//        {
//            if (id != model.Id)
//                return BadRequest();

//            if (!ModelState.IsValid)
//                return View(model);

//            await _studentService.Update(model);

//            return RedirectToAction(nameof(Index));
//        }

//        // GET: Student/Delete/5
//        [Authorize(Roles = "Admin")]
//        public async Task<IActionResult> Delete(int id)
//        {
//            var student = await _studentService.GetById(id);

//            if (student == null)
//                return NotFound();

//            return View(student);
//        }

//        // POST: Student/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        [Authorize(Roles = "Admin")]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var deleted = await _studentService.Delete(id);

//            if (!deleted)
//                return NotFound();

//            return RedirectToAction(nameof(Index));
//        }
//    }
//}
using BLogicLayer.Interfaces;
using BLogicLayer.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;


namespace Mawjood_Internship.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        // =========================
        // My Profile
        // =========================

        [Authorize(Roles = "User")]
        public async Task<IActionResult> Profile()
        {
            var email = User.Identity?.Name;

            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Account");
            }

            var students = await _studentService.GetAll();

            var student = students.FirstOrDefault(
                x => x.Email.Equals(
                    email,
                    StringComparison.OrdinalIgnoreCase
                )
            );

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // =========================
        // Student Details
        // =========================
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var student = await _studentService.GetById(id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // =========================
        // Admin Create
        // =========================

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _studentService.Add(model);

            return RedirectToAction(nameof(Index));
        }

        // =========================
        // Students List
        // =========================
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var students = await _studentService.GetAll();

            return View(students);
        }

        // =========================
        // Admin Edit
        // =========================
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var student = await _studentService.GetById(id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            StudentViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _studentService.Update(model);

            return RedirectToAction(nameof(Index));
        }

        // =========================
        // Admin Delete
        // =========================
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _studentService.GetById(id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _studentService.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}