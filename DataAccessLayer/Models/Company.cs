//using Microsoft.AspNetCore.Identity;
//using System;
//using System.Collections.Generic;
//using System.Text;




//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Text;

//namespace Mawjood_Internship.Models
//{
//    public class Company
//    {
//        public int Id { get; set; }
//        [Required]
//        [StringLength(100)]
//        public string Name { get; set; }

//        [Required]
//        [EmailAddress]
//        public string Email { get; set; }

//        [StringLength(500)]
//        public string Description { get; set; }

//        [StringLength(100)]
//        public string City { get; set; }

//        [StringLength(100)]
//        public string Government { get; set; }

//        // Navigation Property
//        public ICollection<Internship> Internships { get; set; }
//    }
//}


using System.ComponentModel.DataAnnotations;
namespace DataAccessLayer.Models;

public class Company
{
    public int Id { get; set; }
    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [StringLength(500)]
    public string Description { get; set; }

    [StringLength(100)]
    public string City { get; set; }

    [StringLength(100)]
    public string Government { get; set; }

    public ICollection<Internship> Internships { get; set; }
        = new List<Internship>();
}