# 🚀 Mawjood Internship Management System

A robust, enterprise-grade 3-Tier/N-Layered ASP.NET Core Web Application designed to connect students with internship opportunities, streamline corporate recruiter postings, and simplify application processing.

---

## 🏗️ Architecture & Project Structure

The project strictly follows the **N-Tier Architecture** pattern for clean separation of concerns, maintainability, and scalability:

* **`DataAccessLayer`**: Contains Entity Framework Core DB contexts, migrations, database seeders, and domain entity models.
* **`BLogicLayer`**: Encapsulates business logic, service abstractions (Interfaces), service implementations, custom validations, and ViewModels.
* **`Mawjood Internship`**: ASP.NET Core MVC presentation layer housing Controllers, Razor Views, and Static Web Assets (`wwwroot`).

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
* [x] Frontend UI Views & Responsive Styling (In Progress)
