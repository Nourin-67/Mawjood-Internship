namespace BLogicLayer.ViewModels
{
    public class ApplicationWorkflowViewModel
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public string StudentName { get; set; } = string.Empty;

        public string StudentEmail { get; set; } = string.Empty;

        public int InternshipId { get; set; }

        public string InternshipTitle { get; set; } = string.Empty;

        public string CompanyName { get; set; } = string.Empty;

        public DateTime Date { get; set; }

        public string Status { get; set; } = "Pending";
    }
}