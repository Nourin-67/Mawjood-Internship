using BLogicLayer.ViewModels;
namespace BLogicLayer.Interfaces
{
    public interface ISkillService
    {
        Task<List<SkillViewModel>> GetAll();
        Task<SkillViewModel> GetById(int id);

        Task Add(SkillViewModel model);

        Task Update(SkillViewModel model);

        Task<bool> Delete(int id);
    }
}