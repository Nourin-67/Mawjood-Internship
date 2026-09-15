using BLogicLayer.Interfaces;
using BLogicLayer.ViewModels;
using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
namespace BLogicLayer.Services
{
    public class SkillService : ISkillService
    {
        private readonly ApplicationDbContext _context;
        public SkillService(
            ApplicationDbContext context)
        {
            _context = context;
        }


        // GET ALL

        public async Task<List<SkillViewModel>>
            GetAll()
        {
            return await _context.Skills
                .AsNoTracking()
                .Select(x => new SkillViewModel
                {
                    Id = x.Id,

                    Name = x.Name
                })
                .ToListAsync();
        }


        // GET BY ID

        public async Task<SkillViewModel>
            GetById(int id)
        {
            return await _context.Skills
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new SkillViewModel
                {
                    Id = x.Id,

                    Name = x.Name
                })
                .FirstOrDefaultAsync();
        }


        // ADD

        public async Task Add(
            SkillViewModel model)
        {
            var skill = new Skill
            {
                Name = model.Name
            };


            _context.Skills.Add(skill);

            await _context.SaveChangesAsync();
        }


        // UPDATE

        public async Task Update(
            SkillViewModel model)
        {
            var skill =
                await _context.Skills
                    .FindAsync(model.Id);


            if (skill == null)
                return;


            skill.Name =
                model.Name;


            await _context.SaveChangesAsync();
        }


        // DELETE

        public async Task<bool> Delete(int id)
        {
            var skill =
                await _context.Skills
                    .FindAsync(id);


            if (skill == null)
                return false;


            _context.Skills.Remove(skill);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}