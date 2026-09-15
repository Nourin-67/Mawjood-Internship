using System.ComponentModel.DataAnnotations;
namespace BLogicLayer.ViewModels
{
    public class CompanyViewModel
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
    }
}