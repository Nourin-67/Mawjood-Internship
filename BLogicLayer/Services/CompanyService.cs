using BLogicLayer.Interfaces;
using BLogicLayer.ViewModels;
using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
namespace BLogicLayer.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ApplicationDbContext _context;
        public CompanyService(
            ApplicationDbContext context)
        {
            _context = context;
        }


        // GET ALL

        public async Task<List<CompanyViewModel>>
            GetAll()
        {
            return await _context.Companies
                .AsNoTracking()
                .Select(x => new CompanyViewModel
                {
                    Id = x.Id,

                    Name = x.Name,

                    Email = x.Email,

                    Description =
                        x.Description,

                    City = x.City,

                    Government =
                        x.Government
                })
                .ToListAsync();
        }


        // GET BY ID

        public async Task<CompanyViewModel>
            GetById(int id)
        {
            return await _context.Companies
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new CompanyViewModel
                {
                    Id = x.Id,

                    Name = x.Name,

                    Email = x.Email,

                    Description =
                        x.Description,

                    City = x.City,

                    Government =
                        x.Government
                })
                .FirstOrDefaultAsync();
        }


        // ADD

        public async Task Add(
            CompanyViewModel model)
        {
            var company = new Company
            {
                Name = model.Name,

                Email = model.Email,

                Description =
                    model.Description,

                City = model.City,

                Government =
                    model.Government
            };


            _context.Companies.Add(company);

            await _context.SaveChangesAsync();
        }


        // UPDATE

        public async Task Update(
            CompanyViewModel model)
        {
            var company =
                await _context.Companies
                    .FindAsync(model.Id);


            if (company == null)
                return;


            company.Name =
                model.Name;

            company.Email =
                model.Email;

            company.Description =
                model.Description;

            company.City =
                model.City;

            company.Government =
                model.Government;


            await _context.SaveChangesAsync();
        }


        // DELETE

        public async Task<bool> Delete(int id)
        {
            var company =
                await _context.Companies
                    .FindAsync(id);


            if (company == null)
                return false;


            _context.Companies.Remove(company);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}