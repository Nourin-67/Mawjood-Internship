

//using DataAccessLayer.Models;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore;

//namespace DataAccessLayer.Data
//{
//    public class ApplicationDbContext
//        : IdentityDbContext<ApplicationUser>
//    {
//        public ApplicationDbContext(
//            DbContextOptions<ApplicationDbContext> options)
//            : base(options)
//        {
//        }

//        public DbSet<Admin> Admins { get; set; }

//        public DbSet<Student> Students { get; set; }

//        public DbSet<Company> Companies { get; set; }

//        public DbSet<Internship> Internships { get; set; }

//        public DbSet<Application> Applications { get; set; }

//        public DbSet<Skill> Skills { get; set; }

//        public DbSet<StudentSkill> StudentSkills { get; set; }

//        public DbSet<InternshipSkill> InternshipSkills { get; set; }


//        protected override void OnModelCreating(
//            ModelBuilder modelBuilder)
//        {
//            base.OnModelCreating(modelBuilder);


//            // StudentSkill
//            modelBuilder.Entity<StudentSkill>()
//                .HasKey(x => new
//                {
//                    x.StudentId,
//                    x.SkillId
//                });


//            // InternshipSkill
//            modelBuilder.Entity<InternshipSkill>()
//                .HasKey(x => new
//                {
//                    x.InternshipId,
//                    x.SkillId
//                });


//            // Company -> Internship
//            modelBuilder.Entity<Internship>()
//                .HasOne(i => i.Company)
//                .WithMany(c => c.Internships)
//                .HasForeignKey(i => i.CompanyId)
//                .OnDelete(DeleteBehavior.Cascade);


//            // Student -> Application
//            modelBuilder.Entity<Application>()
//                .HasOne(a => a.Student)
//                .WithMany(s => s.Applications)
//                .HasForeignKey(a => a.StudentId)
//                .OnDelete(DeleteBehavior.Cascade);


//            // Internship -> Application
//            modelBuilder.Entity<Application>()
//                .HasOne(a => a.Internship)
//                .WithMany(i => i.Applications)
//                .HasForeignKey(a => a.InternshipId)
//                .OnDelete(DeleteBehavior.Cascade);


//            // Student -> StudentSkill
//            modelBuilder.Entity<StudentSkill>()
//                .HasOne(ss => ss.Student)
//                .WithMany(s => s.StudentSkills)
//                .HasForeignKey(ss => ss.StudentId);


//            // Skill -> StudentSkill
//            modelBuilder.Entity<StudentSkill>()
//                .HasOne(ss => ss.Skill)
//                .WithMany(s => s.StudentSkills)
//                .HasForeignKey(ss => ss.SkillId);


//            // Internship -> InternshipSkill
//            modelBuilder.Entity<InternshipSkill>()
//                .HasOne(ins => ins.Internship)
//                .WithMany(i => i.InternshipSkills)
//                .HasForeignKey(ins => ins.InternshipId);


//            // Skill -> InternshipSkill
//            modelBuilder.Entity<InternshipSkill>()
//                .HasOne(ins => ins.Skill)
//                .WithMany(s => s.InternshipSkills)
//                .HasForeignKey(ins => ins.SkillId);
//        }
//    }
//}





using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Data
{
    public class ApplicationDbContext
        : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        // ========================================
        // DbSets
        // ========================================

        public DbSet<Admin> Admins { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Internship> Internships { get; set; }

        public DbSet<Application> Applications { get; set; }

        public DbSet<Skill> Skills { get; set; }

        public DbSet<StudentSkill> StudentSkills { get; set; }

        public DbSet<InternshipSkill> InternshipSkills { get; set; }


        // ========================================
        // Relationships
        // ========================================

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            // IMPORTANT:
            // Identity tables must be configured first.

            base.OnModelCreating(modelBuilder);


            // ========================================
            // StudentSkill
            // Composite Primary Key
            // ========================================

            modelBuilder.Entity<StudentSkill>()
                .HasKey(x => new
                {
                    x.StudentId,
                    x.SkillId
                });


            // ========================================
            // InternshipSkill
            // Composite Primary Key
            // ========================================

            modelBuilder.Entity<InternshipSkill>()
                .HasKey(x => new
                {
                    x.InternshipId,
                    x.SkillId
                });


            // ========================================
            // Company 1 : Many Internship
            // ========================================

            modelBuilder.Entity<Internship>()
                .HasOne(i => i.Company)
                .WithMany(c => c.Internships)
                .HasForeignKey(i => i.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);


            // ========================================
            // Student 1 : Many Application
            // ========================================

            modelBuilder.Entity<Application>()
                .HasOne(a => a.Student)
                .WithMany(s => s.Applications)
                .HasForeignKey(a => a.StudentId)
                .OnDelete(DeleteBehavior.Cascade);


            // ========================================
            // Internship 1 : Many Application
            // ========================================

            modelBuilder.Entity<Application>()
                .HasOne(a => a.Internship)
                .WithMany(i => i.Applications)
                .HasForeignKey(a => a.InternshipId)
                .OnDelete(DeleteBehavior.Cascade);


            // ========================================
            // Student 1 : Many StudentSkill
            // ========================================

            modelBuilder.Entity<StudentSkill>()
                .HasOne(ss => ss.Student)
                .WithMany(s => s.StudentSkills)
                .HasForeignKey(ss => ss.StudentId)
                .OnDelete(DeleteBehavior.Cascade);


            // ========================================
            // Skill 1 : Many StudentSkill
            // ========================================

            modelBuilder.Entity<StudentSkill>()
                .HasOne(ss => ss.Skill)
                .WithMany(s => s.StudentSkills)
                .HasForeignKey(ss => ss.SkillId)
                .OnDelete(DeleteBehavior.Cascade);


            // ========================================
            // Internship 1 : Many InternshipSkill
            // ========================================

            modelBuilder.Entity<InternshipSkill>()
                .HasOne(ins => ins.Internship)
                .WithMany(i => i.InternshipSkills)
                .HasForeignKey(ins => ins.InternshipId)
                .OnDelete(DeleteBehavior.Cascade);


            // ========================================
            // Skill 1 : Many InternshipSkill
            // ========================================

            modelBuilder.Entity<InternshipSkill>()
                .HasOne(ins => ins.Skill)
                .WithMany(s => s.InternshipSkills)
                .HasForeignKey(ins => ins.SkillId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}