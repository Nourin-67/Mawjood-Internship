using BLogicLayer.ViewModels;
namespace BLogicLayer.Interfaces
{
    public interface IApplicationService
    {
        Task<List<ApplicationViewModel>> GetAll();
        Task<ApplicationViewModel> GetById(int id);

        Task Add(ApplicationViewModel model);

        Task Update(ApplicationViewModel model);

        Task<bool> Delete(int id);

        Task<bool> Reject(int id);
    }
}