using BLogicLayer.ViewModels;
namespace BLogicLayer.Interfaces
{
    public interface ICompanyService
    {
        Task<List<CompanyViewModel>> GetAll();
        Task<CompanyViewModel> GetById(int id);

        Task Add(CompanyViewModel model);

        Task Update(CompanyViewModel model);

        Task<bool> Delete(int id);
    }
}