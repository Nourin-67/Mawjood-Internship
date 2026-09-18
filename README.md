# 🚀 Mawjood Internship Management System

A robust, enterprise-grade 3-Tier/N-Layered ASP.NET Core Web Application designed to connect students with internship opportunities, streamline corporate recruiter postings, and simplify application processing.

---

## 🏗️ Architecture & Project Structure

The project strictly follows the **N-Tier Architecture** pattern for clean separation of concerns, maintainability, and scalability:

* **`DataAccessLayer`**: Contains Entity Framework Core DB contexts, migrations, database seeders, and domain entity models.
* **`BLogicLayer`**: Encapsulates business logic, service abstractions (Interfaces), service implementations, custom validations, and ViewModels.
* **`Mawjood Internship`**: ASP.NET Core MVC presentation layer housing Controllers, Razor Views, and Static Web Assets (`wwwroot`).

---

## 👥 Team Members & Contribution Breakdown

### 1. Fatma Farh- Authentication & Student Profile Lead
* **Primary Layer:** `BLogicLayer` & `DataAccessLayer`
* **Key Contributions:**
  * Implemented identity core extensions using `ApplicationUser`, `Admin`, and `Student` domain models.
  * Developed `AccountService` and `StudentService` along with their respective interface abstractions (`IAccountService`, `IStudentService`).
  * Built registration and authentication ViewModels (`LoginViewModel`, `RegisterViewModel`, `StudentViewModel`).
  * Configured user seed data handling via `SeedIdentityData.cs`.

### 2. Noureen Elborady- Company Management & Recruiter Module
* **Primary Layer:** `BLogicLayer` & `DataAccessLayer`
* **Key Contributions:**
  * Created the `Company` entity and mapping configurations.
  * Implemented `ICompanyService` and `CompanyService` for handling corporate profile operations and approvals.
  * Designed `CompanyViewModel` for data transfer between presentation and service layers.

### 3. Hadia Ebrahim- Internship Catalog & Skills Engine
* **Primary Layer:** `BLogicLayer` & `DataAccessLayer`
* **Key Contributions:**
  * Modeled domain entities for `Internship`, `Skill`, `InternshipSkill`, and `StudentSkill` relational logic.
  * Implemented core business services: `IInternshipService`, `ISkillService`, `InternshipService`, and `SkillService`.
  * Created `InternshipViewModel` and `SkillViewModel` to handle complex posting structures and skill-tagging logic.

### 4. Farah Sameh- Application Workflow & Candidate CV Tracking
* **Primary Layer:** `BLogicLayer` & Presentation Setup
* **Key Contributions:**
  * Built candidate application processing logic via `Application` entity and `IApplicationService`.
  * Designed `ApplicationViewModel` and `CvViewModel` for student submissions and status tracking.
  * Managed application status transitions (Pending, Accepted, Rejected) and validation logic.

### 5. Somia Soliman-Infrastructure, Database Operations & MVC Integration
* **Primary Layer:** `DataAccessLayer` & `Mawjood Internship` (Presentation)
* **Key Contributions:**
  * Configured `ApplicationDbContext`, EF Core migrations, and initial data seeding (`SeedData.cs`).
  * Registered and managed Dependency Injection (DI) service lifetimes within `Program.cs`.
  * Configured presentation base pipeline, routing, controllers structure, and static file middleware.

---

## 🛠️ Tech Stack & Dependencies

* **Framework:** .NET / ASP.NET Core MVC
* **ORM:** Entity Framework Core
* **Database:** Microsoft SQL Server
* **Architecture:** N-Tier Architecture (Presentation, Business Logic, Data Access)
* **Security & Auth:** ASP.NET Core Identity

---

## ⚙️ How to Run the Project Local Environment

1. **Clone the Repository:**
   ```bash
   git clone https://github.com/Nourin-67/Mawgood-internship.git
   ```

2. **Navigate to the Root Directory:**
   ```bash
   cd Mawgood-internship
   ```

3. **Apply Database Migrations:**
   ```bash
   dotnet ef database update --project DataAccessLayer --startup-project "Mawjood Internship"
   ```

4. **Run the Application:**
   ```bash
   dotnet run --project "Mawjood Internship"
   ```

---

## 📌 Status
* [x] Database Schema & Domain Modeling
* [x] Business Services & ViewModel Architecture
* [x] Application Logic & Identity Setup
* [ ] Frontend UI Views & Responsive Styling (In Progress)
