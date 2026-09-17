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

        public InternshipService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<InternshipViewModel>> GetAll()
        {
            return await _context.Internships
                .AsNoTracking()
                .Include(x => x.Company)
                .Include(x => x.InternshipSkills)
                .Select(x => new InternshipViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Location = x.Location,
                    CompanyId = x.CompanyId,
                    CompanyName = x.Company.Name,

                    SkillIds = x.InternshipSkills
                        .Select(s => s.SkillId)
                        .ToList(),

                    RequiredSkillIds = x.InternshipSkills
                        .Where(s => s.IsRequired)
                        .Select(s => s.SkillId)
                        .ToList()
                })
                .ToListAsync();
        }

        public async Task<InternshipViewModel> GetById(int id)
        {
            return await _context.Internships
                .AsNoTracking()
                .Include(x => x.Company)
                .Include(x => x.InternshipSkills)
                .Where(x => x.Id == id)
                .Select(x => new InternshipViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Location = x.Location,
                    CompanyId = x.CompanyId,
                    CompanyName = x.Company.Name,

                    SkillIds = x.InternshipSkills
                        .Select(s => s.SkillId)
                        .ToList(),

                    RequiredSkillIds = x.InternshipSkills
                        .Where(s => s.IsRequired)
                        .Select(s => s.SkillId)
                        .ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task Add(InternshipViewModel model)
        {
            var internship = new Internship
            {
                Title = model.Title.Trim(),
                Location = model.Location.Trim(),
                CompanyId = model.CompanyId
            };

            _context.Internships.Add(internship);

            await _context.SaveChangesAsync();

            var selectedSkills =
                model.SkillIds
                    .Distinct()
                    .ToList();

            foreach (var skillId in selectedSkills)
            {
                _context.InternshipSkills.Add(
                    new InternshipSkill
                    {
                        InternshipId = internship.Id,
                        SkillId = skillId,
                        IsRequired =
                            model.RequiredSkillIds
                                .Contains(skillId)
                    });
            }

            await _context.SaveChangesAsync();
        }

        public async Task Update(InternshipViewModel model)
        {
            var internship =
                await _context.Internships
                    .FirstOrDefaultAsync(
                        x => x.Id == model.Id);

            if (internship == null)
                return;

            internship.Title = model.Title.Trim();
            internship.Location = model.Location.Trim();
            internship.CompanyId = model.CompanyId;

            var oldSkills =
                await _context.InternshipSkills
                    .Where(x =>
                        x.InternshipId == model.Id)
                    .ToListAsync();

            _context.InternshipSkills.RemoveRange(oldSkills);

            var selectedSkills =
                model.SkillIds
                    .Distinct()
                    .ToList();

            foreach (var skillId in selectedSkills)
            {
                _context.InternshipSkills.Add(
                    new InternshipSkill
                    {
                        InternshipId = model.Id,
                        SkillId = skillId,
                        IsRequired =
                            model.RequiredSkillIds
                                .Contains(skillId)
                    });
            }

            await _context.SaveChangesAsync();
        }

        public async Task<bool> Delete(int id)
        {
            var internship =
                await _context.Internships
                    .FirstOrDefaultAsync(
                        x => x.Id == id);

            if (internship == null)
                return false;

            _context.Internships.Remove(internship);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}