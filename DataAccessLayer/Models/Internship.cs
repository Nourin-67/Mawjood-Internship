
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Text;

//namespace Mawjood_Internship.Models
//{
//    public class Internship
//    {
//        public int Id { get; set; }
//        [Required]
//        [StringLength(150)]
//        public string Title { get; set; }

//        [Required]
//        [StringLength(100)]
//        public string Location { get; set; }

//        // Foreign Key
//        public int CompanyId { get; set; }

//        // Navigation Property
//        public Company Company { get; set; }

//        public ICollection<Application> Applications { get; set; }

//        public ICollection<InternshipSkill> InternshipSkills { get; set; }
//    }
//}


using System.ComponentModel.DataAnnotations;
namespace DataAccessLayer.Models;

public class Internship
{
    public int Id { get; set; }
    [Required]
    [StringLength(150)]
    public string Title { get; set; }

    [Required]
    [StringLength(100)]
    public string Location { get; set; }

    public int CompanyId { get; set; }

    public Company Company { get; set; }

    public ICollection<Application> Applications { get; set; }
        = new List<Application>();

    public ICollection<InternshipSkill> InternshipSkills { get; set; }
        = new List<InternshipSkill>();
}