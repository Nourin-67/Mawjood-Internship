using BLogicLayer.ViewModels;
namespace BLogicLayer.Interfaces
{
    public interface IAccountService
    {
        Task<(bool Succeeded, string[] Errors)> RegisterStudent(RegisterViewModel model);
        Task<(
            bool Succeeded,
            int UserId,
            string Name,
            string Role,
            string Error)>
            Login(LoginViewModel model);
    }
}