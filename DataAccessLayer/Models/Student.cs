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

        // =========================================
        // Old CV fields
        // =========================================

        // Internal CV content
        public string? CV { get; set; }

        [StringLength(20)]
        public string? CVType { get; set; }

        // =========================================
        // NEW CV FIELDS
        // =========================================

        // Physical uploaded PDF file path
        [StringLength(500)]
        public string? CVFilePath { get; set; }

        // External CV URL
        [StringLength(1000)]
        public string? ExternalCVLink { get; set; }

        // =========================================
        // Education
        // =========================================

        [StringLength(200)]
        public string? Education { get; set; }

        public int? AcademicYear { get; set; }

        // =========================================
        // Location
        // =========================================

        [StringLength(100)]
        public string? City { get; set; }

        [StringLength(100)]
        public string? Government { get; set; }

        // =========================================
        // Relationships
        // =========================================

        public ICollection<Application> Applications { get; set; }
            = new List<Application>();

        public ICollection<StudentSkill> StudentSkills { get; set; }
            = new List<StudentSkill>();
        public ICollection<Notification> Notifications { get; set; }
    = new List<Notification>();

    }
}