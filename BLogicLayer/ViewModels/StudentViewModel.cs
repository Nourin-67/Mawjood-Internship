using System.ComponentModel.DataAnnotations;
namespace BLogicLayer.ViewModels
{
    public class StudentViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string FName { get; set; }

        [Required]
        [StringLength(50)]
        public string LName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

        [StringLength(100)]
        public string University { get; set; }

        [StringLength(100)]
        public string Major { get; set; }

        public string CV { get; set; }

        [StringLength(100)]
        public string Education { get; set; }

        [Range(1, 5)]
        public int? AcademicYear { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        [StringLength(100)]
        public string Government { get; set; }
    }
}