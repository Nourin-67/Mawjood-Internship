namespace BLogicLayer.ViewModels
{
    public class RecommendedInternshipViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public string CompanyName { get; set; } = string.Empty;

        public List<string> Skills { get; set; } = new();

        public int MatchingSkills { get; set; }

        public int TotalSkills { get; set; }

        public bool HasApplied { get; set; }

        public string? ApplicationStatus { get; set; }

        public int MatchPercentage
        {
            get
            {
                if (TotalSkills == 0)
                    return 0;

                return (int)Math.Round(
                    MatchingSkills * 100.0 / TotalSkills);
            }
        }

        public bool IsRecommended =>
            MatchingSkills > 0;
    }
}
