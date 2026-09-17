using BLogicLayer.Interfaces;
using BLogicLayer.ViewModels;
using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace BLogicLayer.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly ApplicationDbContext _context;

        public ApplicationService(ApplicationDbContext context)
        {
            _context = context;
        }

        // =========================================================
        // GET ALL - ADMIN
        // =========================================================

        public async Task<List<ApplicationViewModel>> GetAll()
        {
            return await _context.Applications
                .AsNoTracking()
                .Include(a => a.Student)
                .Include(a => a.Internship)
                .Select(a => new ApplicationViewModel
                {
                    Id = a.Id,
                    StudentId = a.StudentId,
                    InternshipId = a.InternshipId,
                    Date = a.Date,
                    Status = a.Status,
                    MatchingScore = a.MatchingScore,

                    StudentName =
                        a.Student.FName + " " + a.Student.LName,

                    InternshipTitle =
                        a.Internship.Title,

                    CVLink = a.Student.CVType == "External"
                        ? a.Student.CV
                        : a.Student.CVFilePath
                })
                .OrderByDescending(a => a.Date)
                .ToListAsync();
        }


        // =========================================================
        // GET BY ID
        // =========================================================

        public async Task<ApplicationViewModel?> GetById(int id)
        {
            return await _context.Applications
                .AsNoTracking()
                .Include(a => a.Student)
                .Include(a => a.Internship)
                .Where(a => a.Id == id)
                .Select(a => new ApplicationViewModel
                {
                    Id = a.Id,
                    StudentId = a.StudentId,
                    InternshipId = a.InternshipId,
                    Date = a.Date,
                    Status = a.Status,
                    MatchingScore = a.MatchingScore,

                    StudentName =
                        a.Student.FName + " " + a.Student.LName,

                    InternshipTitle =
                        a.Internship.Title,

                    CVLink = a.Student.CVType == "External"
                        ? a.Student.CV
                        : a.Student.CVFilePath
                })
                .FirstOrDefaultAsync();
        }


        // =========================================================
        // APPLY
        // =========================================================

        public async Task<(bool Success, string Message)> Apply(
            int studentId,
            int internshipId)
        {
            var student = await _context.Students
                .Include(s => s.StudentSkills)
                .ThenInclude(ss => ss.Skill)
                .FirstOrDefaultAsync(s => s.Id == studentId);

            if (student == null)
            {
                return (false, "Student was not found.");
            }

            var internship = await _context.Internships
                .Include(i => i.InternshipSkills)
                .ThenInclude(isk => isk.Skill)
                .FirstOrDefaultAsync(i => i.Id == internshipId);

            if (internship == null)
            {
                return (false, "Internship was not found.");
            }

            // Prevent duplicate application.
            var existingApplication =
                await _context.Applications
                    .FirstOrDefaultAsync(a =>
                        a.StudentId == studentId &&
                        a.InternshipId == internshipId);

            if (existingApplication != null)
            {
                return (false, "You have already applied for this internship.");
            }

            // =====================================================
            // MATCHING SCORE
            // =====================================================

            var studentSkillIds = student.StudentSkills
                .Select(x => x.SkillId)
                .Distinct()
                .ToHashSet();

            var requiredSkillIds = internship.InternshipSkills
                .Where(x => x.IsRequired)
                .Select(x => x.SkillId)
                .Distinct()
                .ToList();

            // If there are no required skills,
            // there is no skill mismatch to calculate.
            double matchingScore;

            if (requiredSkillIds.Count == 0)
            {
                matchingScore = 0;
            }
            else
            {
                var matchedSkills =
                    requiredSkillIds.Count(studentSkillIds.Contains);

                matchingScore =
                    Math.Round(
                        (double)matchedSkills /
                        requiredSkillIds.Count *
                        100,
                        2);
            }

            // =====================================================
            // CREATE APPLICATION
            // =====================================================

            var application = new Application
            {
                StudentId = studentId,
                InternshipId = internshipId,
                Date = DateTime.Now,
                Status = "Pending",
                MatchingScore = matchingScore
            };

            _context.Applications.Add(application);

            await _context.SaveChangesAsync();

            return (true, "Application submitted successfully.");
        }


        // =========================================================
        // ADD - KEEP EXISTING ADMIN FUNCTIONALITY
        // =========================================================

        public async Task Add(ApplicationViewModel model)
        {
            var application = new Application
            {
                StudentId = model.StudentId,
                InternshipId = model.InternshipId,
                Date = model.Date,
                Status = string.IsNullOrWhiteSpace(model.Status)
                    ? "Pending"
                    : model.Status,
                MatchingScore = model.MatchingScore
            };

            _context.Applications.Add(application);

            await _context.SaveChangesAsync();
        }


        // =========================================================
        // UPDATE - KEEP EXISTING ADMIN FUNCTIONALITY
        // =========================================================

        public async Task Update(ApplicationViewModel model)
        {
            var application =
                await _context.Applications
                    .FirstOrDefaultAsync(a => a.Id == model.Id);

            if (application == null)
            {
                return;
            }

            application.StudentId = model.StudentId;
            application.InternshipId = model.InternshipId;
            application.Date = model.Date;
            application.Status = model.Status;
            application.MatchingScore = model.MatchingScore;

            await _context.SaveChangesAsync();
        }


        // =========================================================
        // DELETE
        // =========================================================

        public async Task<bool> Delete(int id)
        {
            var application =
                await _context.Applications
                    .FirstOrDefaultAsync(a => a.Id == id);

            if (application == null)
            {
                return false;
            }

            _context.Applications.Remove(application);

            await _context.SaveChangesAsync();

            return true;
        }


        // =========================================================
        // ACCEPT
        // =========================================================

        public async Task<bool> Accept(int id)
        {
            var application =
                await _context.Applications
                    .Include(a => a.Student)
                    .Include(a => a.Internship)
                    .FirstOrDefaultAsync(a => a.Id == id);

            if (application == null)
            {
                return false;
            }

            application.Status = "Accepted";

            var notification = new Notification
            {
                StudentId = application.StudentId,
                Message =
                    $"Your application for \"{application.Internship.Title}\" has been accepted.",
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            };

            _context.Notifications.Add(notification);

            await _context.SaveChangesAsync();

            return true;
        }


        // =========================================================
        // REJECT
        // =========================================================

        public async Task<bool> Reject(int id)
        {
            var application =
                await _context.Applications
                    .Include(a => a.Student)
                    .Include(a => a.Internship)
                    .FirstOrDefaultAsync(a => a.Id == id);

            if (application == null)
            {
                return false;
            }

            application.Status = "Rejected";

            var notification = new Notification
            {
                StudentId = application.StudentId,
                Message =
                    $"Your application for \"{application.Internship.Title}\" has been rejected.",
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            };

            _context.Notifications.Add(notification);

            await _context.SaveChangesAsync();

            return true;

            }
        public async Task<bool> HasApplied(
            int studentId,
            int internshipId)
        {
            return await _context.Applications.AnyAsync(a =>
                a.StudentId == studentId &&
                a.InternshipId == internshipId);
        }
    }
}