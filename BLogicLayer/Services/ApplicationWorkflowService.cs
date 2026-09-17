using BLogicLayer.Interfaces;
using BLogicLayer.ViewModels;
using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace BLogicLayer.Services
{
    public class ApplicationWorkflowService
        : IApplicationWorkflowService
    {
        private readonly ApplicationDbContext _context;

        public ApplicationWorkflowService(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<(bool Succeeded, string Error)>
            ApplyAsync(
                int studentId,
                int internshipId)
        {
            var studentExists =
                await _context.Students
                    .AnyAsync(s => s.Id == studentId);

            if (!studentExists)
                return (false, "Student was not found.");

            var internshipExists =
                await _context.Internships
                    .AnyAsync(i => i.Id == internshipId);

            if (!internshipExists)
                return (false, "Internship was not found.");

            var alreadyApplied =
                await _context.Applications
                    .AnyAsync(a =>
                        a.StudentId == studentId &&
                        a.InternshipId == internshipId);

            if (alreadyApplied)
            {
                return (
                    false,
                    "You have already applied for this internship.");
            }

            var application = new Application
            {
                StudentId = studentId,
                InternshipId = internshipId,
                Date = DateTime.Now,
                Status = "Pending"
            };

            _context.Applications.Add(application);

            await _context.SaveChangesAsync();

            return (true, "");
        }

        public async Task<List<ApplicationWorkflowViewModel>>
            GetForStudentAsync(int studentId)
        {
            return await _context.Applications
                .AsNoTracking()
                .Where(a => a.StudentId == studentId)
                .Include(a => a.Student)
                .Include(a => a.Internship)
                    .ThenInclude(i => i.Company)
                .OrderByDescending(a => a.Date)
                .Select(a =>
                    new ApplicationWorkflowViewModel
                    {
                        Id = a.Id,

                        StudentId = a.StudentId,

                        StudentName =
                            a.Student.FName + " " +
                            a.Student.LName,

                        StudentEmail =
                            a.Student.Email,

                        InternshipId =
                            a.InternshipId,

                        InternshipTitle =
                            a.Internship.Title,

                        CompanyName =
                            a.Internship.Company.Name,

                        Date = a.Date,

                        Status = a.Status
                    })
                .ToListAsync();
        }

        public async Task<List<ApplicationWorkflowViewModel>>
            GetAllAsync()
        {
            return await _context.Applications
                .AsNoTracking()
                .Include(a => a.Student)
                .Include(a => a.Internship)
                    .ThenInclude(i => i.Company)
                .OrderByDescending(a => a.Date)
                .Select(a =>
                    new ApplicationWorkflowViewModel
                    {
                        Id = a.Id,

                        StudentId = a.StudentId,

                        StudentName =
                            a.Student.FName + " " +
                            a.Student.LName,

                        StudentEmail =
                            a.Student.Email,

                        InternshipId =
                            a.InternshipId,

                        InternshipTitle =
                            a.Internship.Title,

                        CompanyName =
                            a.Internship.Company.Name,

                        Date = a.Date,

                        Status = a.Status
                    })
                .ToListAsync();
        }

        public async Task<bool> AcceptAsync(int id)
        {
            var application =
                await _context.Applications
                    .FirstOrDefaultAsync(a => a.Id == id);

            if (application == null)
                return false;

            application.Status = "Accepted";

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RejectAsync(int id)
        {
            var application =
                await _context.Applications
                    .FirstOrDefaultAsync(a => a.Id == id);

            if (application == null)
                return false;

            application.Status = "Rejected";

            await _context.SaveChangesAsync();

            return true;
        }
    }
}