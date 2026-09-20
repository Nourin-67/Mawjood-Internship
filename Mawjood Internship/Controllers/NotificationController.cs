using BLogicLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mawjood_Internship.Controllers
{
    [Authorize(Roles = "User")]
    public class NotificationController : Controller
    {
        private readonly INotificationService _notificationService;
        private readonly IStudentService _studentService;

        public NotificationController(
            INotificationService notificationService,
            IStudentService studentService)
        {
            _notificationService = notificationService;
            _studentService = studentService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var student = await GetCurrentStudent();

            if (student == null)
            {
                return NotFound();
            }

            var notifications =
                await _notificationService.GetForStudent(
                    student.Id);

            return View(notifications);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var student = await GetCurrentStudent();

            if (student == null)
            {
                return NotFound();
            }

            await _notificationService.MarkAsRead(
                id,
                student.Id);

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkAllAsRead()
        {
            var student = await GetCurrentStudent();

            if (student == null)
            {
                return NotFound();
            }

            await _notificationService.MarkAllAsRead(
                student.Id);

            return RedirectToAction(nameof(Index));
        }


        private async Task<BLogicLayer.ViewModels.StudentViewModel?>
            GetCurrentStudent()
        {
            var email = User.Identity?.Name;

            if (string.IsNullOrEmpty(email))
            {
                return null;
            }

            var students =
                await _studentService.GetAll();

            return students.FirstOrDefault(x =>
                x.Email.Equals(
                    email,
                    StringComparison.OrdinalIgnoreCase));
        }
    }
}