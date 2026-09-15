
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Text;

//namespace Mawjood_Internship.Models
//{
//    public class Skill
//    {
//        public int Id { get; set; }
//        [Required]
//        [StringLength(50)]
//        public string Name { get; set; }

//        // Navigation Properties
//        public ICollection<StudentSkill> StudentSkills { get; set; }

//        public ICollection<InternshipSkill> InternshipSkills { get; set; }
//    }
//}


using System.ComponentModel.DataAnnotations;
namespace DataAccessLayer.Models;

public class Skill
{
    public int Id { get; set; }
    [Required]
    [StringLength(50)]
    public string Name { get; set; }

    public ICollection<StudentSkill> StudentSkills { get; set; }
        = new List<StudentSkill>();

    public ICollection<InternshipSkill> InternshipSkills { get; set; }
        = new List<InternshipSkill>();
}