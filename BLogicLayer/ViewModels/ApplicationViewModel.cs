using System.ComponentModel.DataAnnotations;
namespace BLogicLayer.ViewModels
{
    public class ApplicationViewModel
    {
        public int Id { get; set; }
        [Required]
        public int StudentId { get; set; }

        [Required]
        public int InternshipId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        // Display only
        public string StudentName { get; set; }

        // Display only
        public string InternshipTitle { get; set; }
    }
}