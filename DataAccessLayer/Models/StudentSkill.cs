//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Mawjood_Internship.Models
//{
//    public class StudentSkill
//    {
//        public int StudentId { get; set; }
//        public int SkillId { get; set; }

//        // Navigation Properties
//        public Student Student { get; set; }

//        public Skill Skill { get; set; }
//    }
//}

namespace DataAccessLayer.Models;

public class StudentSkill
{
    public int StudentId { get; set; }
    public int SkillId { get; set; }

    public Student Student { get; set; }

    public Skill Skill { get; set; }
}