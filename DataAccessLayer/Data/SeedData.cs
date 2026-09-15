

using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(
            ApplicationDbContext context)
        {
            // ========================================
            // Skills
            // ========================================

            if (!await context.Skills.AnyAsync())
            {
                context.Skills.AddRange(

                    new Skill
                    {
                        Name = "C#"
                    },

                    new Skill
                    {
                        Name = "SQL"
                    },

                    new Skill
                    {
                        Name = "Python"
                    },

                    new Skill
                    {
                        Name = "Machine Learning"
                    },

                    new Skill
                    {
                        Name = "Cyber Security"
                    }
                );

                await context.SaveChangesAsync();
            }


            // ========================================
            // Company
            // ========================================

            if (!await context.Companies.AnyAsync())
            {
                var company = new Company
                {
                    Name = "Mawjood Tech",

                    Email = "company@mawjoood.com",

                    Description = "Technology Company",

                    City = "Desouk",

                    Government = "Kafr El Sheikh"
                };

                context.Companies.Add(company);

                await context.SaveChangesAsync();
            }


            // ========================================
            // Internships
            // ========================================

            if (!await context.Internships.AnyAsync())
            {
                var company =
                    await context.Companies.FirstAsync();

                context.Internships.AddRange(

                    new Internship
                    {
                        Title = "AI Internship",

                        Location = "Cairo",

                        CompanyId = company.Id
                    },

                    new Internship
                    {
                        Title = "Cyber Security Internship",

                        Location = "Alexandria",

                        CompanyId = company.Id
                    },

                    new Internship
                    {
                        Title = "Software Development Internship",

                        Location = "Desouk",

                        CompanyId = company.Id
                    }
                );

                await context.SaveChangesAsync();
            }
        }
    }
}