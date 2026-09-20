using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public class Application
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int InternshipId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Pending";

        // Matching percentage between student skills
        // and internship required skills.
        [Range(0, 100)]
        public double MatchingScore { get; set; }

        public Student Student { get; set; }

        public Internship Internship { get; set; }
    }
}