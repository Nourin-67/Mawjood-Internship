//using BLogicLayer.ViewModels;
//namespace BLogicLayer.Interfaces
//{
//    public interface IStudentService
//    {
//        Task<List<StudentViewModel>> GetAll();
//        Task<StudentViewModel> GetById(int id);

//        Task Add(StudentViewModel model);

//        Task Update(StudentViewModel model);

//        Task<bool> Delete(int id);
//    }
//}
using BLogicLayer.ViewModels;

namespace BLogicLayer.Interfaces
{
    public interface IStudentService
    {
        Task<List<StudentViewModel>> GetAll();

        Task<StudentViewModel> GetById(int id);

        Task Add(StudentViewModel model);

        Task Update(StudentViewModel model);

        Task<bool> Delete(int id);

        Task<CVViewModel?> GetCV(int studentId);

        Task<bool> SaveExternalCV(int studentId, string cvLink);

        Task<bool> SaveInternalCV(int studentId, CVViewModel model);
    }
}