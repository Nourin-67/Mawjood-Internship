using BLogicLayer.ViewModels;

namespace BLogicLayer.Interfaces
{
    public interface IInternshipRecommendationService
    {
        Task<InternshipBrowseViewModel>
            GetForStudentAsync(int studentId);
    }
}