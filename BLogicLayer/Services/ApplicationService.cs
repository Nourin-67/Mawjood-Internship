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
        public ApplicationService(
            ApplicationDbContext context)
        {
            _context = context;
        }


        // =========================
        // GET ALL APPLICATIONS
        // =========================

        public async Task<List<ApplicationViewModel>>
            GetAll()
        {
            return await _context.Applications
                .AsNoTracking()
                .Include(x => x.Student)
                .Include(x => x.Internship)
                .Select(x => new ApplicationViewModel
                {
                    Id = x.Id,

                    StudentId =
                        x.StudentId,

                    InternshipId =
                        x.InternshipId,

                    Date =
                        x.Date,

                    Status =
                        x.Status,

                    StudentName =
                        x.Student.FName +
                        " " +
                        x.Student.LName,

                    InternshipTitle =
                        x.Internship.Title
                })
                .ToListAsync();
        }


        // =========================
        // GET BY ID
        // =========================

        public async Task<ApplicationViewModel>
            GetById(int id)
        {
            return await _context.Applications
                .AsNoTracking()
                .Include(x => x.Student)
                .Include(x => x.Internship)
                .Where(x => x.Id == id)
                .Select(x => new ApplicationViewModel
                {
                    Id = x.Id,

                    StudentId =
                        x.StudentId,

                    InternshipId =
                        x.InternshipId,

                    Date =
                        x.Date,

                    Status =
                        x.Status,

                    StudentName =
                        x.Student.FName +
                        " " +
                        x.Student.LName,

                    InternshipTitle =
                        x.Internship.Title
                })
                .FirstOrDefaultAsync();
        }


        // =========================
        // ADD APPLICATION
        // =========================

        public async Task Add(
            ApplicationViewModel model)
        {
            var application =
                new Application
                {
                    StudentId =
                        model.StudentId,

                    InternshipId =
                        model.InternshipId,

                    Date =
                        model.Date,

                    Status =
                        string.IsNullOrWhiteSpace(
                            model.Status)
                        ? "Pending"
                        : model.Status
                };


            _context.Applications.Add(
                application);

            await _context.SaveChangesAsync();
        }


        // =========================
        // UPDATE
        // =========================

        public async Task Update(
            ApplicationViewModel model)
        {
            var application =
                await _context.Applications
                    .FindAsync(model.Id);


            if (application == null)
                return;


            application.StudentId =
                model.StudentId;

            application.InternshipId =
                model.InternshipId;

            application.Date =
                model.Date;

            application.Status =
                model.Status;


            await _context.SaveChangesAsync();
        }


        // =========================
        // DELETE
        // =========================

        public async Task<bool> Delete(int id)
        {
            var application =
                await _context.Applications
                    .FindAsync(id);


            if (application == null)
                return false;


            _context.Applications.Remove(
                application);

            await _context.SaveChangesAsync();

            return true;
        }


        // =========================
        // REJECT
        // =========================
        public async Task<bool> Reject(int id)
        {
            var application =
                await _context.Applications
                    .FindAsync(id);


            if (application == null)
                return false;


            application.Status =
                "Rejected";


            await _context.SaveChangesAsync();

            return true;
        }
    }
}