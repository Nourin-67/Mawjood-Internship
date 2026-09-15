//using Microsoft.AspNetCore.Identity;
//using System;
//using System.Collections.Generic;
//using System.Text;



//namespace Mawjood_Internship.Models
//{
//    public class ApplicationUser : IdentityUser
//    {
//        public string FullName { get; set; }
//    }
//}
using Microsoft.AspNetCore.Identity;

namespace DataAccessLayer.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
    }
}