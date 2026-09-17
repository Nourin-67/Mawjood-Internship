using System.ComponentModel.DataAnnotations;

namespace BLogicLayer.ViewModels
{
    public class InternshipViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Location { get; set; } = string.Empty;

        [Required]
        public int CompanyId { get; set; }

        public string CompanyName { get; set; } = string.Empty;

        public List<int> SkillIds { get; set; } = new();

        public List<int> RequiredSkillIds { get; set; } = new();

        public List<int> SelectedSkillIds { get; set; } = new();
    }
}