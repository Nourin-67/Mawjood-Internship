//using System;
//using System.Collections.Generic;
//using System.Text;

//using System.ComponentModel.DataAnnotations;

//namespace Mawjood_Internship.Models
//{
//    public class Application
//    {
//        public int Id { get; set; }
//        public int StudentId { get; set; }

//        public int InternshipId { get; set; }

//        [Required]
//        public DateTime Date { get; set; }

//        [StringLength(50)]
//        public string Status { get; set; }

//        // Navigation Properties
//        public Student Student { get; set; }

//        public Internship Internship { get; set; }
//    }
//}

using System.ComponentModel.DataAnnotations;
namespace DataAccessLayer.Models;

public class Application
{
    public int Id { get; set; }
    public int StudentId { get; set; }

    public int InternshipId { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    [StringLength(50)]
    public string Status { get; set; }

    public Student Student { get; set; }

    public Internship Internship { get; set; }
}