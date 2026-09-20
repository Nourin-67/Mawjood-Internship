using BLogicLayer.ViewModels;

namespace BLogicLayer.Interfaces
{
    public interface IApplicationService
    {
        Task<List<ApplicationViewModel>> GetAll();

        Task<ApplicationViewModel?> GetById(int id);

        Task Add(ApplicationViewModel model);

        Task Update(ApplicationViewModel model);

        Task<bool> Delete(int id);

        Task<bool> Reject(int id);

        Task<bool> Accept(int id);

        Task<(bool Success, string Message)> Apply(
            int studentId,
            int internshipId);
        Task<bool> HasApplied(int studentId, int internshipId);
    }
}