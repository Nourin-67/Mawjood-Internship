using BLogicLayer.Interfaces;
using BLogicLayer.ViewModels;
using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;

namespace BLogicLayer.Services
{
    public class NotificationService : INotificationService
    {
        private readonly ApplicationDbContext _context;

        public NotificationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<NotificationViewModel>> GetForStudent(
            int studentId)
        {
            return await _context.Notifications
                .AsNoTracking()
                .Where(n => n.StudentId == studentId)
                .OrderByDescending(n => n.CreatedAt)
                .Select(n => new NotificationViewModel
                {
                    Id = n.Id,
                    Message = n.Message,
                    IsRead = n.IsRead,
                    CreatedAt = n.CreatedAt
                })
                .ToListAsync();
        }

        public async Task<int> GetUnreadCount(int studentId)
        {
            return await _context.Notifications
                .CountAsync(n =>
                    n.StudentId == studentId &&
                    !n.IsRead);
        }

        public async Task<bool> MarkAsRead(
            int id,
            int studentId)
        {
            var notification =
                await _context.Notifications
                    .FirstOrDefaultAsync(n =>
                        n.Id == id &&
                        n.StudentId == studentId);

            if (notification == null)
            {
                return false;
            }

            notification.IsRead = true;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> MarkAllAsRead(int studentId)
        {
            var notifications =
                await _context.Notifications
                    .Where(n =>
                        n.StudentId == studentId &&
                        !n.IsRead)
                    .ToListAsync();

            foreach (var notification in notifications)
            {
                notification.IsRead = true;
            }

            await _context.SaveChangesAsync();

            return true;
        }
    }
}