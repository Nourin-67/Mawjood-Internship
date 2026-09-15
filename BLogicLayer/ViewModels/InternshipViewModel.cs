using System.ComponentModel.DataAnnotations;
namespace BLogicLayer.ViewModels
{
    public class InternshipViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string Title { get; set; }

        [Required]
        [StringLength(100)]
        public string Location { get; set; }

        [Required]
        public int CompanyId { get; set; }

        public string CompanyName { get; set; }
    }
}