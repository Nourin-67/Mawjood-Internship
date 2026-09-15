using BLogicLayer.ViewModels;
namespace BLogicLayer.Interfaces
{
    public interface IInternshipService
    {
        Task<List<InternshipViewModel>> GetAll();
        Task<InternshipViewModel> GetById(int id);

        Task Add(InternshipViewModel model);

        Task Update(InternshipViewModel model);

        Task<bool> Delete(int id);
    }
}