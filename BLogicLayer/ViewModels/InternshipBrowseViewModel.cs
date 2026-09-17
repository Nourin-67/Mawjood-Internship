namespace BLogicLayer.ViewModels
{
    public class InternshipBrowseViewModel
    {
        public List<RecommendedInternshipViewModel>
            RecommendedInternships
        { get; set; } = new();

        public List<RecommendedInternshipViewModel>
            AllInternships
        { get; set; } = new();

        public List<InternshipViewModel> AdminInternships { get; set; } = new();
    }
}
