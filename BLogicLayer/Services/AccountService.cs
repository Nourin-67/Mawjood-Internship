//using BLogicLayer.Interfaces;
//using BLogicLayer.ViewModels;
//using DataAccessLayer.Data;
//using DataAccessLayer.Models;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//namespace BLogicLayer.Services
//{
//    public class AccountService : IAccountService
//    {
//        private readonly ApplicationDbContext _context;
//        private readonly PasswordHasher<Student> _studentHasher = new();

//        private readonly PasswordHasher<Admin> _adminHasher = new();


//        public AccountService(ApplicationDbContext context)
//        {
//            _context = context;
//        }


//        // =========================
//        // REGISTER STUDENT
//        // =========================

//        public async Task<(bool Succeeded, string[] Errors)>
//            RegisterStudent(RegisterViewModel model)
//        {
//            var email = model.Email
//                .Trim()
//                .ToLower();


//            // Check if email already exists
//            bool studentExists =
//                await _context.Students
//                    .AnyAsync(x =>
//                        x.Email.ToLower() == email);


//            bool adminExists =
//                await _context.Admins
//                    .AnyAsync(x =>
//                        x.Email.ToLower() == email);


//            if (studentExists || adminExists)
//            {
//                return (
//                    false,
//                    new[]
//                    {
//                    "This email is already registered."
//                    });
//            }


//            // Create Student
//            var student = new Student
//            {
//                FName = model.FName.Trim(),

//                LName = model.LName.Trim(),

//                Email = email,

//                University = model.University,

//                Major = model.Major,

//                Education = model.Education,

//                AcademicYear = model.AcademicYear,

//                City = model.City,

//                Government = model.Government
//            };


//            // Hash Password
//            student.Password =
//                _studentHasher.HashPassword(
//                    student,
//                    model.Password);


//            _context.Students.Add(student);

//            await _context.SaveChangesAsync();


//            return (
//                true,
//                Array.Empty<string>());
//        }


//        // =========================
//        // LOGIN
//        // =========================

//        public async Task<(
//            bool Succeeded,
//            int UserId,
//            string Name,
//            string Role,
//            string Error)>
//            Login(LoginViewModel model)
//        {
//            var email = model.Email
//                .Trim()
//                .ToLower();


//            // =========================
//            // CHECK ADMIN
//            // =========================

//            var admin =
//                await _context.Admins
//                    .FirstOrDefaultAsync(
//                        x => x.Email.ToLower() == email);


//            if (admin != null)
//            {
//                var result =
//                    _adminHasher.VerifyHashedPassword(
//                        admin,
//                        admin.Password,
//                        model.Password);


//                if (result !=
//                    PasswordVerificationResult.Failed)
//                {
//                    return (
//                        true,
//                        admin.Id,
//                        admin.Name,
//                        "Admin",
//                        "");
//                }
//            }


//            // =========================
//            // CHECK STUDENT
//            // =========================

//            var student =
//                await _context.Students
//                    .FirstOrDefaultAsync(
//                        x => x.Email.ToLower() == email);


//            if (student != null)
//            {
//                var result =
//                    _studentHasher.VerifyHashedPassword(
//                        student,
//                        student.Password,
//                        model.Password);


//                if (result !=
//                    PasswordVerificationResult.Failed)
//                {
//                    return (
//                        true,
//                        student.Id,
//                        student.FName +
//                        " " +
//                        student.LName,
//                        "Student",
//                        "");
//                }
//            }


//            // =========================// INVALID LOGIN
//            // =========================

//            return (
//                false,
//                0,
//                "",
//                "",
//                "Invalid Email or Password.");
//        }
//    }
//}
using BLogicLayer.Interfaces;
using BLogicLayer.ViewModels;
using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BLogicLayer.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountService(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // =========================
        // REGISTER STUDENT
        // =========================
        public async Task<(bool Succeeded, string[] Errors)>
            RegisterStudent(RegisterViewModel model)
        {
            var email = model.Email.Trim().ToLower();

            // Check if Student already exists
            bool studentExists =
                await _context.Students
                    .AnyAsync(x =>
                        x.Email.ToLower() == email);

            if (studentExists)
            {
                return (
                    false,
                    new[]
                    {
                        "This email is already registered."
                    }
                );
            }

            // Check if Identity User already exists
            var existingUser =
                await _userManager.FindByEmailAsync(email);

            if (existingUser != null)
            {
                return (
                    false,
                    new[]
                    {
                        "This email is already registered."
                    }
                );
            }

            // =========================
            // CREATE IDENTITY USER
            // =========================

            var identityUser = new ApplicationUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true,

                FullName =
                    model.FName.Trim()
                    + " "
                    + model.LName.Trim()
            };

            var createResult =
                await _userManager.CreateAsync(
                    identityUser,
                    model.Password
                );

            if (!createResult.Succeeded)
            {
                var errors =
                    createResult.Errors
                        .Select(e => e.Description)
                        .ToArray();

                return (false, errors);
            }

            // =========================
            // ADD USER ROLE
            // =========================

            var roleResult =
                await _userManager.AddToRoleAsync(
                    identityUser,
                    "User"
                );

            if (!roleResult.Succeeded)
            {
                var errors =
                    roleResult.Errors
                        .Select(e => e.Description)
                        .ToArray();

                // Remove Identity user
                // if role creation fails
                await _userManager.DeleteAsync(
                    identityUser
                );

                return (false, errors);
            }

            // =========================
            // CREATE STUDENT
            // =========================

            var student = new Student
            {
                FName = model.FName.Trim(),

                LName = model.LName.Trim(),

                Email = email,

                // Password is handled by ASP.NET Identity.
                Password = "",

                University = model.University,

                Major = model.Major,

                Education = model.Education,

                AcademicYear = model.AcademicYear,

                City = model.City,

                Government = model.Government
            };

            _context.Students.Add(student);

            await _context.SaveChangesAsync();

            return (
                true,
                Array.Empty<string>()
            );
        }


        // =========================
        // LOGIN
        // =========================
        public async Task<(
            bool Succeeded,
            int UserId,
            string Name,
            string Role,
            string Error)>
            Login(LoginViewModel model)
        {
            var email =
                model.Email.Trim().ToLower();

            // Find Identity User
            var user =
                await _userManager.FindByEmailAsync(
                    email
                );

            if (user == null)
            {
                return (
                    false,
                    0,
                    "",
                    "",
                    "Invalid Email or Password."
                );
            }

            // Check Identity Password
            var passwordValid =
                await _userManager.CheckPasswordAsync(
                    user,
                    model.Password
                );

            if (!passwordValid)
            {
                return (
                    false,
                    0,
                    "",
                    "",
                    "Invalid Email or Password."
                );
            }

            // Get User Roles
            var roles =
                await _userManager.GetRolesAsync(user);

            string role =
                roles.FirstOrDefault()
                ?? "User";

            int userId = 0;

            // Get Student Id
            if (role == "User")
            {
                var student =
                    await _context.Students
                        .FirstOrDefaultAsync(
                            x =>
                                x.Email.ToLower()
                                == email
                        );

                if (student != null)
                {
                    userId = student.Id;
                }
            }

            string name =
                user.FullName
                ?? user.Email
                ?? "";

            return (
                true,
                userId,
                name,
                role,
                ""
            );
        }
    }
}