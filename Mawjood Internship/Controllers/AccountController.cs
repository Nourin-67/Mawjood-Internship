




//using BLogicLayer.Interfaces;
//using BLogicLayer.ViewModels;
//using DataAccessLayer.Models;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;

//namespace Mawjood_Internship.Controllers
//{
//    public class AccountController : Controller
//    {
//        private readonly UserManager<ApplicationUser> _userManager;

//        private readonly SignInManager<ApplicationUser> _signInManager;

//        private readonly IAccountService _accountService;


//        public AccountController(
//            UserManager<ApplicationUser> userManager,
//            SignInManager<ApplicationUser> signInManager,
//            IAccountService accountService)
//        {
//            _userManager = userManager;

//            _signInManager = signInManager;

//            _accountService = accountService;
//        }


//        // ========================================
//        // LOGIN - GET
//        // ========================================

//        [HttpGet]
//        public IActionResult Login()
//        {
//            return View();
//        }


//        // ========================================
//        // LOGIN - POST
//        // ========================================

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Login(
//            LoginViewModel model)
//        {
//            if (!ModelState.IsValid)
//            {
//                return View(model);
//            }


//            var user =
//                await _userManager.FindByEmailAsync(
//                    model.Email.Trim()
//                );


//            if (user == null)
//            {
//                ModelState.AddModelError(
//                    "",
//                    "Invalid email or password."
//                );

//                return View(model);
//            }


//            var result =
//                await _signInManager.PasswordSignInAsync(
//                    user.UserName!,
//                    model.Password,
//                    false,
//                    false
//                );


//            if (!result.Succeeded)
//            {
//                ModelState.AddModelError(
//                    "",
//                    "Invalid email or password."
//                );

//                return View(model);
//            }


//            // ========================================
//            // Redirect
//            // ========================================

//            if (await _userManager.IsInRoleAsync(
//                    user,
//                    "Admin"))
//            {
//                return RedirectToAction(
//                    "Index",
//                    "Home"
//                );
//            }


//            return RedirectToAction(
//                "Index",
//                "Home"
//            );
//        }


//        // ========================================
//        // REGISTER - GET
//        // ========================================

//        [HttpGet]
//        public IActionResult Register()
//        {
//            return View();
//        }


//        // ========================================
//        // REGISTER - POST
//        // ========================================

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Register(
//            RegisterViewModel model)
//        {
//            if (!ModelState.IsValid)
//            {
//                return View(model);
//            }


//            var result =
//                await _accountService.RegisterStudent(
//                    model
//                );


//            if (!result.Succeeded)
//            {
//                foreach (var error in result.Errors)
//                {
//                    ModelState.AddModelError(
//                        "",
//                        error
//                    );
//                }

//                return View(model);
//            }


//            return RedirectToAction(
//                nameof(Login)
//            );
//        }


//        // ========================================
//        // LOGOUT
//        // ========================================

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Logout()
//        {
//            await _signInManager.SignOutAsync();

//            return RedirectToAction(
//                "Index",
//                "Home"
//            );
//        }


//        // ========================================
//        // ACCESS DENIED
//        // ========================================

//        [HttpGet]
//        public IActionResult AccessDenied()
//        {
//            return View();
//        }
//    }
//}


using BLogicLayer.Interfaces;
using BLogicLayer.ViewModels;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Mawjood_Internship.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly IAccountService _accountService;


        // =========================
        // CONSTRUCTOR
        // =========================

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IAccountService accountService)
        {
            _userManager = userManager;

            _signInManager = signInManager;

            _accountService = accountService;
        }


        // =========================
        // LOGIN - GET
        // =========================

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        // =========================
        // LOGIN - POST
        // =========================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(
            LoginViewModel model)
        {
            // Check Validation
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Normalize Email
            var email =
                model.Email.Trim().ToLower();

            // Find Identity User
            var user =
                await _userManager.FindByEmailAsync(
                    email
                );

            // User does not exist
            if (user == null)
            {
                ModelState.AddModelError(
                    "",
                    "Invalid email or password."
                );

                return View(model);
            }

            // =========================
            // SIGN IN USING IDENTITY
            // =========================

            var result =
                await _signInManager.PasswordSignInAsync(
                    user.UserName!,
                    model.Password,
                    model.RememberMe,
                    false
                );

            // Login failed
            if (!result.Succeeded)
            {
                ModelState.AddModelError(
                    "",
                    "Invalid email or password."
                );

                return View(model);
            }

            // =========================
            // LOGIN SUCCESS
            // =========================

            return RedirectToAction(
                "Index",
                "Home"
            );
        }


        // =========================
        // REGISTER - GET
        // =========================

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        // =========================
        // REGISTER - POST
        // =========================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(
            RegisterViewModel model)
        {
            // Check Validation
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Register Student
            var result =
                await _accountService.RegisterStudent(
                    model
                );

            // Registration failed
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(
                        "",
                        error
                    );
                }

                return View(model);
            }

            // Registration successful
            return RedirectToAction(
                nameof(Login)
            );
        }


        // =========================
        // LOGOUT
        // =========================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(
                "Index",
                "Home"
            );
        }


        // =========================
        // ACCESS DENIED
        // =========================

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}