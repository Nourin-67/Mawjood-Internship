using System.ComponentModel.DataAnnotations;

namespace BLogicLayer.ViewModels
{
    public class StudentViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string LName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6)]
        public string? Password { get; set; }

        [StringLength(100)]
        public string? University { get; set; }

        [StringLength(100)]
        public string? Major { get; set; }

        public string? CV { get; set; }

        public string? CVType { get; set; }

        [StringLength(200)]
        public string? Education { get; set; }

        [Range(1, 5)]
        public int? AcademicYear { get; set; }

        [StringLength(100)]
        public string? City { get; set; }

        [StringLength(100)]
        public string? Government { get; set; }

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