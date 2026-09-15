//using System.ComponentModel.DataAnnotations;
//namespace BLogicLayer.ViewModels
//{
//    public class RegisterViewModel
//    {
//        [Required][StringLength(50)] public string FName { get; set; }
//        [Required]
//        [StringLength(50)]
//        public string LName { get; set; }

//        [Required]
//        [EmailAddress]
//        public string Email { get; set; }

//        [Required]
//        [DataType(DataType.Password)]
//        [StringLength(100, MinimumLength = 6)]
//        public string Password { get; set; }

//        [Required]
//        [DataType(DataType.Password)]
//        [Compare("Password")]
//        public string ConfirmPassword { get; set; }

//        [StringLength(100)]
//        public string University { get; set; }

//        [StringLength(100)]
//        public string Major { get; set; }

//        [StringLength(100)]
//        public string Education { get; set; }

//        [Range(1, 5)]
//        public int AcademicYear { get; set; }

//        [StringLength(100)]
//        public string City { get; set; }

//        [StringLength(100)]
//        public string Government { get; set; }
//    }
//}



using System.ComponentModel.DataAnnotations;

namespace BLogicLayer.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(50)]
        public string FName { get; set; } = string.Empty;


        [Required]
        [StringLength(50)]
        public string LName { get; set; } = string.Empty;


        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;


        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 8)]
        public string Password { get; set; } = string.Empty;


        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; } = string.Empty;


        [StringLength(100)]
        public string? University { get; set; }


        [StringLength(100)]
        public string? Major { get; set; }


        [StringLength(100)]
        public string? Education { get; set; }


        [Range(1, 5)]
        public int AcademicYear { get; set; }


        [StringLength(100)]
        public string? City { get; set; }


        [StringLength(100)]
        public string? Government { get; set; }
    }
}