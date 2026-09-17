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

        public StudentService(
            ApplicationDbContext context)
        {
            _context = context;

            _passwordHasher =
                new PasswordHasher<Student>();
        }

        // =====================================================
        // Get All Students
        // =====================================================

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

                    Education = s.Education,

                    AcademicYear =
                        s.AcademicYear,

                    City = s.City,

                    Government =
                        s.Government
                })
                .ToListAsync();
        }

        // =====================================================
        // Get Student By Id
        // =====================================================

        public async Task<StudentViewModel> GetById(int id)
        {
            var student =
                await _context.Students
                    .AsNoTracking()
                    .FirstOrDefaultAsync(
                        s => s.Id == id
                    );

            if (student == null)
                return null!;

            return new StudentViewModel
            {
                Id = student.Id,

                FName = student.FName,
                LName = student.LName,

                Email = student.Email,

                University =
                    student.University,

                Major =
                    student.Major,

                CV =
                    student.CV,

                Education =
                    student.Education,

                AcademicYear =
                    student.AcademicYear,

                City =
                    student.City,

                Government =
                    student.Government
            };
        }

        // =====================================================
        // Add Student
        // =====================================================

        public async Task Add(
            StudentViewModel model)
        {
            var student = new Student
            {
                FName =
                    model.FName,

                LName =
                    model.LName,

                Email =
                    model.Email,

                University =
                    model.University,

                Major =
                    model.Major,

                CV =
                    model.CV,

                Education =
                    model.Education,

                AcademicYear =
                    model.AcademicYear,

                City =
                    model.City,

                Government =
                    model.Government
            };

            student.Password =
                _passwordHasher.HashPassword(
                    student,
                    string.IsNullOrWhiteSpace(
                        model.Password)
                        ? "Temp@12345"
                        : model.Password
                );

            _context.Students.Add(student);

            await _context.SaveChangesAsync();
        }

        // =====================================================
        // Update Student
        // =====================================================

        public async Task Update(
            StudentViewModel model)
        {
            var student =
                await _context.Students
                    .FirstOrDefaultAsync(
                        s => s.Id == model.Id
                    );

            if (student == null)
                return;

            student.FName =
                model.FName;

            student.LName =
                model.LName;

            student.Email =
                model.Email;

            student.University =
                model.University;

            student.Major =
                model.Major;

            student.CV =
                model.CV;

            student.Education =
                model.Education;

            student.AcademicYear =
                model.AcademicYear;

            student.City =
                model.City;

            student.Government =
                model.Government;

            if (!string.IsNullOrWhiteSpace(
                model.Password))
            {
                student.Password =
                    _passwordHasher.HashPassword(
                        student,
                        model.Password
                    );
            }

            await _context.SaveChangesAsync();
        }

        // =====================================================
        // Delete Student
        // =====================================================

        public async Task<bool> Delete(int id)
        {
            var student =
                await _context.Students
                    .FirstOrDefaultAsync(
                        s => s.Id == id
                    );

            if (student == null)
                return false;

            _context.Students.Remove(student);

            await _context.SaveChangesAsync();

            return true;
        }

        // =====================================================
        // Get CV
        // =====================================================

        public async Task<CVViewModel?> GetCV(
            int studentId)
        {
            var student =
                await _context.Students
                    .AsNoTracking()
                    .FirstOrDefaultAsync(
                        s => s.Id == studentId
                    );

            if (student == null)
                return null;

            var model = new CVViewModel
            {
                StudentId =
                    student.Id,

                FName =
                    student.FName,

                LName =
                    student.LName,

                Email =
                    student.Email,

                University =
                    student.University,

                Major =
                    student.Major,

                AcademicYear =
                    student.AcademicYear,

                Education =
                    student.Education,

                City =
                    student.City,

                Government =
                    student.Government,

                // =====================================
                // NEW
                // =====================================

                CVFilePath =
                    student.CVFilePath,

                ExternalCVLink =
                    student.ExternalCVLink
            };

            // =========================================
            // Old External CV
            // =========================================

            if (student.CVType == "External" &&
                !string.IsNullOrWhiteSpace(student.CV))
            {
                model.HasExistingCV = true;

                model.ExistingCVLink =
                    student.CV;
            }

            // =========================================
            // Old Internal CV
            // =========================================

            if (student.CVType == "Internal" &&
                !string.IsNullOrWhiteSpace(student.CV))
            {
                model.HasExistingCV = true;

                ParseInternalCV(
                    student.CV,
                    model
                );
            }

            // =========================================
            // New CV File / External Link
            // =========================================

            if (!string.IsNullOrWhiteSpace(
                student.CVFilePath))
            {
                model.HasExistingCV = true;
            }

            if (!string.IsNullOrWhiteSpace(
                student.ExternalCVLink))
            {
                model.HasExistingCV = true;
            }

            return model;
        }

        // =====================================================
        // Save External CV
        // =====================================================

        public async Task<bool> SaveExternalCV(
            int studentId,
            string cvLink)
        {
            var student =
                await _context.Students
                    .FirstOrDefaultAsync(
                        s => s.Id == studentId
                    );

            if (student == null)
                return false;

            student.ExternalCVLink =
                cvLink.Trim();

            await _context.SaveChangesAsync();

            return true;
        }

        // =====================================================
        // Save Uploaded CV File
        // =====================================================

        public async Task<bool> SaveCVFile(
            int studentId,
            string filePath)
        {
            var student =
                await _context.Students
                    .FirstOrDefaultAsync(
                        s => s.Id == studentId
                    );

            if (student == null)
                return false;

            student.CVFilePath =
                filePath;

            await _context.SaveChangesAsync();

            return true;
        }

        // =====================================================
        // Save Internal CV
        // =====================================================

        public async Task<bool> SaveInternalCV(
            int studentId,
            CVViewModel model)
        {
            var student =
                await _context.Students
                    .FirstOrDefaultAsync(
                        s => s.Id == studentId
                    );

            if (student == null)
                return false;

            // Personal Information

            student.FName =
                model.FName;

            student.LName =
                model.LName;

            student.Email =
                model.Email;

            // Education

            student.University =
                model.University;

            student.Major =
                model.Major;

            student.AcademicYear =
                model.AcademicYear;

            student.Education =
                model.Education;

            // Location

            student.City =
                model.City;

            student.Government =
                model.Government;

            // Store Internal CV

            student.CV =
                "ABOUT_ME_START\n" +
                (model.AboutMe ?? "") +
                "\nABOUT_ME_END\n" +

                "SKILLS_START\n" +
                (model.Skills ?? "") +
                "\nSKILLS_END\n" +

                "COURSES_START\n" +
                (model.Courses ?? "") +
                "\nCOURSES_END\n" +

                "EXPERIENCE_START\n" +
                (model.Experience ?? "") +
                "\nEXPERIENCE_END";

            student.CVType =
                "Internal";

            await _context.SaveChangesAsync();

            return true;
        }

        // =====================================================
        // Parse Internal CV
        // =====================================================

        private void ParseInternalCV(
            string cv,
            CVViewModel model)
        {
            model.AboutMe =
                ExtractSection(
                    cv,
                    "ABOUT_ME_START",
                    "ABOUT_ME_END"
                );

            model.Skills =
                ExtractSection(
                    cv,
                    "SKILLS_START",
                    "SKILLS_END"
                );

            model.Courses =
                ExtractSection(
                    cv,
                    "COURSES_START",
                    "COURSES_END"
                );

            model.Experience =
                ExtractSection(
                    cv,
                    "EXPERIENCE_START",
                    "EXPERIENCE_END"
                );
        }

        // =====================================================
        // Extract Section
        // =====================================================

        private string? ExtractSection(
            string text,
            string startMarker,
            string endMarker)
        {
            int start =
                text.IndexOf(startMarker);

            if (start == -1)
                return null;

            start +=
                startMarker.Length;

            int end =
                text.IndexOf(
                    endMarker,
                    start
                );

            if (end == -1)
                return null;

            return text
                .Substring(
                    start,
                    end - start
                )
                .Trim();
        }
    }
}