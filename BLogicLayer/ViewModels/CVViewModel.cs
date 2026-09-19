using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BLogicLayer.ViewModels
{
    public class CVViewModel
    {
        public int StudentId { get; set; }

        // =========================================
        // Existing CV
        // =========================================

        public bool HasExistingCV { get; set; }

        public string? ExistingCVLink { get; set; }

        // =========================================
        // NEW - Uploaded PDF
        // =========================================

        public string? CVFilePath { get; set; }

        public IFormFile? CVFile { get; set; }

        // =========================================
        // NEW - External CV Link
        // =========================================

        [Url]
        [StringLength(1000)]
        public string? ExternalCVLink { get; set; }

        // =========================================
        // Personal Information
        // =========================================

        [Required]
        [StringLength(50)]
        public string FName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string LName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        // =========================================
        // Education
        // =========================================

        [StringLength(100)]
        public string? University { get; set; }

        [StringLength(100)]
        public string? Major { get; set; }

        public int? AcademicYear { get; set; }

        [StringLength(200)]
        public string? Education { get; set; }

        // =========================================
        // Location
        // =========================================

        [StringLength(100)]
        public string? City { get; set; }

        [StringLength(100)]
        public string? Government { get; set; }

        // =========================================
        // Internal CV Information
        // =========================================

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