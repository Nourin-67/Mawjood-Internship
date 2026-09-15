//using System.ComponentModel.DataAnnotations;

//namespace BLogicLayer.ViewModels
//{
//    public class CVViewModel
//    {
//        public int StudentId { get; set; }

//        public string? ExistingCVLink { get; set; }

//        public bool HasExistingCV { get; set; }

//        [Required]
//        [StringLength(50)]
//        public string FName { get; set; } = string.Empty;

//        [Required]
//        [StringLength(50)]
//        public string LName { get; set; } = string.Empty;

//        [Required]
//        [EmailAddress]
//        public string Email { get; set; } = string.Empty;

//        [StringLength(100)]
//        public string? University { get; set; }

//        [StringLength(100)]
//        public string? Major { get; set; }

//        public int? AcademicYear { get; set; }

//        [StringLength(200)]
//        public string? Education { get; set; }

//        [StringLength(100)]
//        public string? City { get; set; }

//        [StringLength(100)]
//        public string? Government { get; set; }

//        [StringLength(1000)]
//        public string? AboutMe { get; set; }

//        [StringLength(1000)]
//        public string? Skills { get; set; }

//        [StringLength(1000)]
//        public string? Courses { get; set; }

//        [StringLength(1000)]
//        public string? Experience { get; set; }
//    }
//}

using System.ComponentModel.DataAnnotations;

namespace BLogicLayer.ViewModels
{
    public class CVViewModel
    {
        public int StudentId { get; set; }

        // =========================
        // Existing CV
        // =========================

        public string? ExistingCVLink { get; set; }

        public bool HasExistingCV { get; set; }


        // =========================
        // Personal Information
        // =========================

        [Required]
        [StringLength(50)]
        public string FName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string LName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;


        // =========================
        // Education
        // =========================

        [StringLength(100)]
        public string? University { get; set; }

        [StringLength(100)]
        public string? Major { get; set; }

        public int? AcademicYear { get; set; }

        [StringLength(200)]
        public string? Education { get; set; }


        // =========================
        // Location
        // =========================

        [StringLength(100)]
        public string? City { get; set; }

        [StringLength(100)]
        public string? Government { get; set; }


        // =========================
        // CV Information
        // =========================

        [StringLength(1000)]
        public string? AboutMe { get; set; }

        [StringLength(1000)]
        public string? Skills { get; set; }

        [StringLength(1000)]
        public string? Courses { get; set; }

        [StringLength(1000)]
        public string? Experience { get; set; }
    }
}