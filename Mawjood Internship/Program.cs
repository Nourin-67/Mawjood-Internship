

//using BLogicLayer.Interfaces;
//using BLogicLayer.Services;
//using DataAccessLayer.Data;
//using DataAccessLayer.Models;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;

//var builder = WebApplication.CreateBuilder(args);


//// ========================================
//// MVC
//// ========================================

//builder.Services.AddControllersWithViews();


//// ========================================
//// Database
//// ========================================

//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(
//        builder.Configuration.GetConnectionString(
//            "DefaultConnection"
//        )
//    ));


//// ========================================
//// Identity
//// ========================================

//builder.Services
//    .AddIdentity<ApplicationUser, IdentityRole>(options =>
//    {
//        // Password settings
//        options.Password.RequireDigit = true;
//        options.Password.RequireLowercase = true;
//        options.Password.RequireUppercase = true;
//        options.Password.RequireNonAlphanumeric = true;
//        options.Password.RequiredLength = 8;

//        // User settings
//        options.User.RequireUniqueEmail = true;
//    })
//    .AddEntityFrameworkStores<ApplicationDbContext>()
//    .AddDefaultTokenProviders();


//// ========================================
//// Services
//// ========================================

//builder.Services.AddScoped<IAccountService, AccountService>();

//builder.Services.AddScoped<IStudentService, StudentService>();

//builder.Services.AddScoped<ICompanyService, CompanyService>();

//builder.Services.AddScoped<IInternshipService, InternshipService>();

//builder.Services.AddScoped<ISkillService, SkillService>();

//builder.Services.AddScoped<IApplicationService, ApplicationService>();


//// ========================================
//// Authorization
//// ========================================

//builder.Services.AddAuthorization();


//// ========================================
//// Build
//// ========================================

//var app = builder.Build();


//// ========================================
//// Middleware
//// ========================================

//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");

//    app.UseHsts();
//}


//app.UseHttpsRedirection();

//app.UseStaticFiles();

//app.UseRouting();


//// IMPORTANT
//// Authentication BEFORE Authorization

//app.UseAuthentication();

//app.UseAuthorization();


//// ========================================
//// Routes
//// ========================================

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");


//// ========================================
//// Seed Identity
//// ========================================

//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;

//    var context =
//        services.GetRequiredService<ApplicationDbContext>();

//    var roleManager =
//        services.GetRequiredService<RoleManager<IdentityRole>>();

//    var userManager =
//        services.GetRequiredService<UserManager<ApplicationUser>>();


//    await context.Database.MigrateAsync();


//    await SeedIdentityData.InitializeAsync(
//        roleManager,
//        userManager
//    );


//    // Existing project seed
//    await SeedData.InitializeAsync(context);
//}


//app.Run();




using BLogicLayer.Interfaces;
using BLogicLayer.Services;
using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ========================================
// MVC
// ========================================

builder.Services.AddControllersWithViews();


// ========================================
// Database
// ========================================

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));


// ========================================
// Identity + Roles
// ========================================

builder.Services
    .AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        // Password settings
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequiredLength = 8;

        // User settings
        options.User.RequireUniqueEmail = true;

        // Sign-in settings
        options.SignIn.RequireConfirmedAccount = false;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();


// ========================================
// Services
// ========================================

builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddScoped<IStudentService, StudentService>();

builder.Services.AddScoped<ICompanyService, CompanyService>();

builder.Services.AddScoped<IInternshipService, InternshipService>();

builder.Services.AddScoped<ISkillService, SkillService>();

builder.Services.AddScoped<IApplicationService, ApplicationService>();


// ========================================
// Authorization
// ========================================

builder.Services.AddAuthorization();


// ========================================
// Build
// ========================================

var app = builder.Build();


// ========================================
// Middleware
// ========================================

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();


// IMPORTANT:
// Authentication must come before Authorization

app.UseAuthentication();

app.UseAuthorization();


// ========================================
// Routes
// ========================================

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);


// ========================================
// Database Migration + Seed
// ========================================

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context =
        services.GetRequiredService<ApplicationDbContext>();

    var roleManager =
        services.GetRequiredService<RoleManager<IdentityRole>>();

    var userManager =
        services.GetRequiredService<UserManager<ApplicationUser>>();


    // Apply migrations

    await context.Database.MigrateAsync();


    // Create Identity Roles + Default Admin

    await SeedIdentityData.InitializeAsync(
        roleManager,
        userManager
    );


    // Create initial application data

    await SeedData.InitializeAsync(context);
}


app.Run();