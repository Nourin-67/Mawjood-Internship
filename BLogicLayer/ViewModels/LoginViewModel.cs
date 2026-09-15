//using System.ComponentModel.DataAnnotations;
//namespace BLogicLayer.ViewModels
//{
//    public class LoginViewModel
//    {
//        [Required][EmailAddress] public string Email { get; set; }
//        [Required]
//        [DataType(DataType.Password)]
//        public string Password { get; set; }

//        public bool RememberMe { get; set; }
//    }
//}


using System.ComponentModel.DataAnnotations;

namespace BLogicLayer.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        public bool RememberMe { get; set; }
    }
}