# 🚀 Mawjood Internship Management System

A robust, enterprise-grade 3-Tier/N-Layered ASP.NET Core Web Application designed to connect students with internship opportunities, streamline corporate recruiter postings, and simplify application processing.

---

## 🏗️ Architecture & Project Structure

The project strictly follows the **N-Tier Architecture** pattern for clean separation of concerns, maintainability, and scalability:

* **`DataAccessLayer`**: Contains Entity Framework Core DB contexts, migrations, database seeders, and domain entity models.
* **`BLogicLayer`**: Encapsulates business logic, service abstractions (Interfaces), service implementations, custom validations, and ViewModels.
* **`Mawjood Internship`**: ASP.NET Core MVC presentation layer housing Controllers, Razor Views, and Static Web Assets (`wwwroot`).

---
## 👥 Team & Task Allocation

| Team Member | Role | Key Responsibilities |
| :--- | :--- | :--- |
| **Fatma Farh** | Frontend Lead | • Complete UI/UX design using HTML, CSS, JavaScript & Bootstrap.<br>• Developing responsive views for Students, Providers & Admin portals.<br>• Integrating Frontend interfaces with ASP.NET Core MVC Views. |Developing & binding ASP.NET Core MVC Razor Views and ViewModels.<br>• Handling Frontend Form Validations & AJAX HTTP Requests.<br>• Creating responsive layouts for Student, Provider & Admin portals. |
| **Noureen El Borady** | Backend Lead | • Designing & implementing SQL Server DB Schema & Entity Framework Core.<br>• Configuring ASP.NET Core Identity (Authentication, Authorization & Roles).<br>• Developing core Business Logic Controllers for Students & Providers. |
| **Hadia Ebrahim** | Backend Developer | • Implementing CRUD operations for Internship Opportunities.<br>• Developing Search & Filter features for students.<br>• Managing Application submission & Status tracking workflows.|• Preparing & updating project documentation (SRS & README).
| **Farah Sameh** | Backend Developer | • Developing System Administrator features (User & Content Management).<br>• Implementing Data Validation across forms and request payloads.<br>• Handling system reporting and issue management functionalities. |
| **Somia Ssarhan** | QA & Documentation | <br>• Creating Seed Data for database initialization and testing.<br>• Conducting Manual System Testing & QA verification. 


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
* [x] Frontend UI Views & Responsive Styling (In Progress)
