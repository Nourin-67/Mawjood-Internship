//using System;
//using System.Collections.Generic;
//using System.Text;

//using System.ComponentModel.DataAnnotations;

//namespace DataAccessLayer.Models
//{
//    public class Admin
//    {
//        public int Id { get; set; }

//        [Required]
//        [StringLength(50)]
//        public string Name { get; set; }

//        [Required]
//        [EmailAddress]
//        public string Email { get; set; }
//    }
//}

using System.ComponentModel.DataAnnotations;
namespace DataAccessLayer.Models;

public class Admin
{
    public int Id { get; set; }
    [Required]
    [StringLength(50)]
    public string Name { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [StringLength(200)]
    public string Password { get; set; }
}