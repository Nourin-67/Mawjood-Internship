using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public class Student
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

        [Required]
        public string Password { get; set; } = string.Empty;

        [StringLength(100)]
        public string? University { get; set; }

        [StringLength(100)]
        public string? Major { get; set; }

        // Internal CV content or external CV link
        public string? CV { get; set; }

        [StringLength(20)]
        public string? CVType { get; set; }

        [StringLength(200)]
        public string? Education { get; set; }

        public int? AcademicYear { get; set; }

        [StringLength(100)]
        public string? City { get; set; }

        [StringLength(100)]
        public string? Government { get; set; }

        // Additional CV information
        [StringLength(1000)]
        public string? AboutMe { get; set; }

        [StringLength(1000)]
        public string? Skills { get; set; }

        [StringLength(1000)]
        public string? Courses { get; set; }

        [StringLength(1000)]
        public string? Experience { get; set; }

        public ICollection<Application> Applications { get; set; }
            = new List<Application>();

        public ICollection<StudentSkill> StudentSkills { get; set; }
            = new List<StudentSkill>();
    }
}