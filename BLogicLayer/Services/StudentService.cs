using BLogicLayer.Interfaces;
using BLogicLayer.ViewModels;
using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BLogicLayer.Services
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationDbContext _context;
        private readonly PasswordHasher<Student> _passwordHasher;

        public StudentService(ApplicationDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<Student>();
        }

        public async Task<List<StudentViewModel>> GetAll()
        {
            return await _context.Students
                .AsNoTracking()
                .Select(s => new StudentViewModel
                {
                    Id = s.Id,
                    FName = s.FName,
                    LName = s.LName,
                    Email = s.Email,
                    University = s.University,
                    Major = s.Major,
                    CV = s.CV,
                    CVType = s.CVType,
                    Education = s.Education,
                    AcademicYear = s.AcademicYear,
                    City = s.City,
                    Government = s.Government,
                    AboutMe = s.AboutMe,
                    Skills = s.Skills,
                    Courses = s.Courses,
                    Experience = s.Experience
                })
                .ToListAsync();
        }

        public async Task<StudentViewModel> GetById(int id)
        {
            var student = await _context.Students
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);

            if (student == null)
                return null!;

            return new StudentViewModel
            {
                Id = student.Id,
                FName = student.FName,
                LName = student.LName,
                Email = student.Email,
                University = student.University,
                Major = student.Major,
                CV = student.CV,
                CVType = student.CVType,
                Education = student.Education,
                AcademicYear = student.AcademicYear,
                City = student.City,
                Government = student.Government,
                AboutMe = student.AboutMe,
                Skills = student.Skills,
                Courses = student.Courses,
                Experience = student.Experience
            };
        }

        public async Task Add(StudentViewModel model)
        {
            var student = new Student
            {
                FName = model.FName.Trim(),
                LName = model.LName.Trim(),
                Email = model.Email.Trim().ToLower(),
                University = model.University,
                Major = model.Major,
                CV = model.CV,
                CVType = model.CVType,
                Education = model.Education,
                AcademicYear = model.AcademicYear,
                City = model.City,
                Government = model.Government,
                AboutMe = model.AboutMe,
                Skills = model.Skills,
                Courses = model.Courses,
                Experience = model.Experience
            };

            student.Password = _passwordHasher.HashPassword(
                student,
                string.IsNullOrWhiteSpace(model.Password)
                    ? "Temp@12345"
                    : model.Password);

            _context.Students.Add(student);

            await _context.SaveChangesAsync();
        }

        public async Task Update(StudentViewModel model)
        {
            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.Id == model.Id);

            if (student == null)
                return;

            student.FName = model.FName.Trim();
            student.LName = model.LName.Trim();
            student.Email = model.Email.Trim().ToLower();
            student.University = model.University;
            student.Major = model.Major;
            student.CV = model.CV;
            student.CVType = model.CVType;
            student.Education = model.Education;
            student.AcademicYear = model.AcademicYear;
            student.City = model.City;
            student.Government = model.Government;
            student.AboutMe = model.AboutMe;
            student.Skills = model.Skills;
            student.Courses = model.Courses;
            student.Experience = model.Experience;

            if (!string.IsNullOrWhiteSpace(model.Password))
            {
                student.Password = _passwordHasher.HashPassword(
                    student,
                    model.Password);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<bool> Delete(int id)
        {
            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.Id == id);

            if (student == null)
                return false;

            _context.Students.Remove(student);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<CVViewModel?> GetCV(int studentId)
        {
            var student = await _context.Students
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == studentId);

            if (student == null)
                return null;

            return new CVViewModel
            {
                StudentId = student.Id,

                FName = student.FName,
                LName = student.LName,
                Email = student.Email,

                University = student.University,
                Major = student.Major,
                AcademicYear = student.AcademicYear,
                Education = student.Education,

                City = student.City,
                Government = student.Government,

                AboutMe = student.AboutMe,
                Skills = student.Skills,
                Courses = student.Courses,
                Experience = student.Experience,

                ExistingCVLink =
                    student.CVType == "External"
                        ? student.CV
                        : null,

                HasExistingCV =
                    !string.IsNullOrWhiteSpace(student.CV)
            };
        }

        public async Task<bool> SaveExternalCV(
            int studentId,
            string cvLink)
        {
            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.Id == studentId);

            if (student == null)
                return false;

            student.CV = cvLink.Trim();
            student.CVType = "External";

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> SaveInternalCV(
            int studentId,
            CVViewModel model)
        {
            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.Id == studentId);

            if (student == null)
                return false;

            student.FName = model.FName.Trim();
            student.LName = model.LName.Trim();
            student.Email = model.Email.Trim().ToLower();

            student.University = model.University;
            student.Major = model.Major;
            student.AcademicYear = model.AcademicYear;
            student.Education = model.Education;

            student.City = model.City;
            student.Government = model.Government;

            student.AboutMe = model.AboutMe;
            student.Skills = model.Skills;
            student.Courses = model.Courses;
            student.Experience = model.Experience;

            student.CVType = "Internal";

            // CV itself is represented by the saved fields above.
            student.CV = "Internal";

            await _context.SaveChangesAsync();

            return true;
        }
    }
}