//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Mawjood_Internship.Models
//{
//    public class InternshipSkill
//    {
//        public int InternshipId { get; set; }
//        public int SkillId { get; set; }

//        public bool IsRequired { get; set; }

//        // Navigation Properties
//        public Internship Internship { get; set; }

//        public Skill Skill { get; set; }
//    }
//}

namespace DataAccessLayer.Models;

public class InternshipSkill
{
    public int InternshipId { get; set; }
    public int SkillId { get; set; }

    public bool IsRequired { get; set; }

    public Internship Internship { get; set; }

    public Skill Skill { get; set; }
}