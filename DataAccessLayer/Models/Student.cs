
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Text;

//namespace Mawjood_Internship.Models
//{
//    public class Student
//    {
//        public int Id { get; set; }
//        [Required]
//        [StringLength(50)]
//        public string FName { get; set; }

//        [Required]
//        [StringLength(50)]
//        public string LName { get; set; }

//        [Required]
//        [EmailAddress]
//        public string Email { get; set; }

//        [Required]
//        [StringLength(100)]
//        public string Password { get; set; }

//        [StringLength(100)]
//        public string University { get; set; }

//        [StringLength(100)]
//        public string Major { get; set; }

//        public string CV { get; set; }

//        [StringLength(100)]
//        public string Education { get; set; }

//        [Range(1, 5)]
//        public int AcademicYear { get; set; }

//        [StringLength(100)]
//        public string City { get; set; }

//        [StringLength(100)]
//        public string Government { get; set; }

//        // Navigation Properties
//        public ICollection<StudentSkill> StudentSkills { get; set; }

//        public ICollection<Application> Applications { get; set; }
//    }
//}

//using Mawjood_Internship.Models;
//using System.ComponentModel.DataAnnotations;
//namespace DataAccessLayer.Models;

//public class Student
//{
//    public int Id { get; set; }
//    [Required]
//    [StringLength(50)]
//    public string FName { get; set; }

//    [Required]
//    [StringLength(50)]
//    public string LName { get; set; }

//    [Required]
//    [EmailAddress]
//    public string Email { get; set; }

//    [Required]
//    [StringLength(200)]
//    public string Password { get; set; }

//    [StringLength(100)]
//    public string University { get; set; }

//    [StringLength(100)]
//    public string Major { get; set; }

//    public string CV { get; set; }

//    [StringLength(100)]
//    public string Education { get; set; }

//    [Range(1, 5)]
//    public int AcademicYear { get; set; }

//    [StringLength(100)]
//    public string City { get; set; }

//    [StringLength(100)]
//    public string Government { get; set; }

//    public ICollection<StudentSkill> StudentSkills { get; set; }
//        = new List<StudentSkill>();

//    public ICollection<Application> Applications { get; set; }
//        = new List<Application>();
//}
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

        // CV Link or Internal CV content
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

        public ICollection<Application> Applications { get; set; }
            = new List<Application>();

        public ICollection<StudentSkill> StudentSkills { get; set; }
            = new List<StudentSkill>();
    }
}