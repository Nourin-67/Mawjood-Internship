using BLogicLayer.ViewModels;

namespace BLogicLayer.Interfaces
{
    public interface IApplicationWorkflowService
    {
        Task<(bool Succeeded, string Error)>
            ApplyAsync(int studentId, int internshipId);

        Task<List<ApplicationWorkflowViewModel>>
            GetForStudentAsync(int studentId);

        Task<List<ApplicationWorkflowViewModel>>
            GetAllAsync();

        Task<bool> AcceptAsync(int id);

        Task<bool> RejectAsync(int id);
    }
}