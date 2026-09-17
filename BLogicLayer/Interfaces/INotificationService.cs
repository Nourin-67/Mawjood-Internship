using BLogicLayer.ViewModels;

namespace BLogicLayer.Interfaces
{
    public interface INotificationService
    {
        Task<List<NotificationViewModel>> GetForStudent(int studentId);

        Task<int> GetUnreadCount(int studentId);

        Task<bool> MarkAsRead(int id, int studentId);

        Task<bool> MarkAllAsRead(int studentId);
    }
}