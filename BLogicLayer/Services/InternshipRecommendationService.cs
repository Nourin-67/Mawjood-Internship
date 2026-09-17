using BLogicLayer.Interfaces;
using BLogicLayer.ViewModels;
using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;

namespace BLogicLayer.Services
{
    public class InternshipRecommendationService
        : IInternshipRecommendationService
    {
        private readonly ApplicationDbContext _context;

        public InternshipRecommendationService(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<InternshipBrowseViewModel>
            GetForStudentAsync(int studentId)
        {
            var student = await _context.Students
                .AsNoTracking()
                .Include(s => s.StudentSkills)
                    .ThenInclude(ss => ss.Skill)
                .FirstOrDefaultAsync(s => s.Id == studentId);

            if (student == null)
            {
                return new InternshipBrowseViewModel();
            }

            var studentSkillNames =
                new HashSet<string>(
                    StringComparer.OrdinalIgnoreCase);

            // Structured StudentSkill records
            foreach (var studentSkill in student.StudentSkills)
            {
                if (studentSkill.Skill != null &&
                    !string.IsNullOrWhiteSpace(
                        studentSkill.Skill.Name))
                {
                    studentSkillNames.Add(
                        Normalize(studentSkill.Skill.Name));
                }
            }

            // Skills written inside the CV
            if (!string.IsNullOrWhiteSpace(student.Skills))
            {
                var cvSkills = student.Skills
                    .Split(
                        new[] { ',', ';', '\n', '\r', '|' },
                        StringSplitOptions.RemoveEmptyEntries);

                foreach (var skill in cvSkills)
                {
                    var normalized =
                        Normalize(skill);

                    if (!string.IsNullOrWhiteSpace(normalized))
                        studentSkillNames.Add(normalized);
                }
            }

            var internships = await _context.Internships
                .AsNoTracking()
                .Include(i => i.Company)
                .Include(i => i.InternshipSkills)
                    .ThenInclude(isx => isx.Skill)
                .Include(i => i.Applications)
                .ToListAsync();

            var result = new InternshipBrowseViewModel();

            foreach (var internship in internships)
            {
                var requiredSkills =
                    internship.InternshipSkills
                        .Where(x => x.IsRequired)
                        .Select(x => x.Skill)
                        .Where(x => x != null)
                        .Select(x => x.Name)
                        .Where(x =>
                            !string.IsNullOrWhiteSpace(x))
                        .Distinct(
                            StringComparer.OrdinalIgnoreCase)
                        .ToList();

                // If no skills were marked required,
                // use all internship skills.
                if (requiredSkills.Count == 0)
                {
                    requiredSkills =
                        internship.InternshipSkills
                            .Select(x => x.Skill)
                            .Where(x => x != null)
                            .Select(x => x.Name)
                            .Where(x =>
                                !string.IsNullOrWhiteSpace(x))
                            .Distinct(
                                StringComparer.OrdinalIgnoreCase)
                            .ToList();
                }

                var matchingSkills = requiredSkills
                    .Count(skill =>
                        studentSkillNames.Contains(
                            Normalize(skill)));

                var application =
                    internship.Applications
                        .FirstOrDefault(a =>
                            a.StudentId == studentId);

                var item =
                    new RecommendedInternshipViewModel
                    {
                        Id = internship.Id,
                        Title = internship.Title,
                        Location = internship.Location,
                        CompanyName =
                            internship.Company?.Name
                            ?? "Unknown",

                        Skills = requiredSkills,

                        MatchingSkills = matchingSkills,

                        TotalSkills = requiredSkills.Count,

                        HasApplied =
                            application != null,

                        ApplicationStatus =
                            application?.Status
                    };

                result.AllInternships.Add(item);

                // Partial matching:
                // at least ONE matching skill.
                if (matchingSkills > 0)
                {
                    result.RecommendedInternships.Add(item);
                }
            }

            result.RecommendedInternships =
                result.RecommendedInternships
                    .OrderByDescending(
                        x => x.MatchPercentage)
                    .ThenBy(x => x.Title)
                    .ToList();

            result.AllInternships =
                result.AllInternships
                    .OrderBy(x => x.Title)
                    .ToList();

            return result;
        }

        private static string Normalize(string value)
        {
            return value
                .Trim()
                .ToLowerInvariant()
                .Replace(" ", "");
        }
    }
}