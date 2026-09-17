using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public class Notification
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        [Required]
        [StringLength(500)]
        public string Message { get; set; } = string.Empty;

        public bool IsRead { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Student Student { get; set; }
    }
}