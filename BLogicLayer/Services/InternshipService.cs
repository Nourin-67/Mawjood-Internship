using BLogicLayer.Interfaces;
using BLogicLayer.ViewModels;
using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
namespace BLogicLayer.Services
{
    public class InternshipService : IInternshipService
    {
        private readonly ApplicationDbContext _context;
        public InternshipService(
            ApplicationDbContext context)
        {
            _context = context;
        }


        // GET ALL

        public async Task<List<InternshipViewModel>>
            GetAll()
        {
            return await _context.Internships
                .AsNoTracking()
                .Include(x => x.Company)
                .Select(x => new InternshipViewModel
                {
                    Id = x.Id,

                    Title = x.Title,

                    Location =
                        x.Location,

                    CompanyId =
                        x.CompanyId,

                    CompanyName =
                        x.Company.Name
                })
                .ToListAsync();
        }


        // GET BY ID

        public async Task<InternshipViewModel>
            GetById(int id)
        {
            return await _context.Internships
                .AsNoTracking()
                .Include(x => x.Company)
                .Where(x => x.Id == id)
                .Select(x => new InternshipViewModel
                {
                    Id = x.Id,

                    Title = x.Title,

                    Location =
                        x.Location,

                    CompanyId =
                        x.CompanyId,

                    CompanyName =
                        x.Company.Name
                })
                .FirstOrDefaultAsync();
        }


        // ADD

        public async Task Add(
            InternshipViewModel model)
        {
            var internship = new Internship
            {
                Title =
                    model.Title,

                Location =
                    model.Location,

                CompanyId =
                    model.CompanyId
            };


            _context.Internships.Add(
                internship);

            await _context.SaveChangesAsync();
        }


        // UPDATE

        public async Task Update(
            InternshipViewModel model)
        {
            var internship =
                await _context.Internships
                    .FindAsync(model.Id);


            if (internship == null)
                return;


            internship.Title =
                model.Title;

            internship.Location =
                model.Location;

            internship.CompanyId =
                model.CompanyId;


            await _context.SaveChangesAsync();
        }


        // DELETE

        public async Task<bool> Delete(int id)
        {
            var internship =
                await _context.Internships
                    .FindAsync(id);


            if (internship == null)
                return false;


            _context.Internships.Remove(
                internship);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}