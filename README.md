# Pharmacy Management System

A comprehensive, enterprise-level Pharmacy Management System designed to streamline and automate daily pharmacy operations. The core idea behind this project is to provide a unified, secure, and intuitive platform for managing medicine inventory, processing sales, and analyzing pharmacy performance through specialized portals for both **Administrators** and **Cashiers**.

## 💡 Project Idea

Managing a pharmacy involves tracking thousands of items, managing expiration dates, processing prescriptions, and handling financial transactions. This system digitalizes those processes:
- Ensuring high accuracy in stock keeping by organizing medicines into "Batches" to handle varying expiration dates and variable costs.
- Speeding up the checkout process via a streamlined Point of Sale (POS) system.
- Empowering management with real-time analytics and detailed reporting to make data-driven business decisions.
- Offering a premium, accessible user experience with a modern "Glassmorphism" UI design philosophy.

## 🌟 Key Features

### **Admin Operations**
Administrators have full oversight of the pharmacy's operations and data:
- **Dashboard & Analytics:** View real-time statistics including total sales revenue, low stock alerts, and cashier performance metrics at a glance.
- **Inventory & Batch Management:** Full CRUD (Create, Read, Update, Delete) operations for medicines and their respective inventory batches. Manage pricing algorithms, track expiration dates, and monitor granular stock levels.
- **User & Access Management:** Create, update, and manage cashier and administrator employee accounts with strict role-based access control.
- **Financial Reporting:** Generate comprehensive historical sales ledgers and inventory health reports to analyze business trends.

### **Cashier Operations (POS)**
Cashiers are provided with tools optimized for speed and accuracy during customer transactions:
- **Point of Sale (POS):** Fast, intuitive checkout process with interactive, JavaScript-driven cart management.
- **Dynamic Search & Filter:** Quickly find medicines by brand name or generic scientific name, verify real-time stock availability, and seamlessly add items to a sale.
- **Sales Tracking:** Every transaction is cryptographically associated with the cashier who processed it, ensuring total accountability and traceable transaction histories.

## 🏗️ Architecture

The solution adheres strictly to modern software engineering principles, utilizing **Clean Architecture** to ensure robust separation of concerns, high maintainability, and scalability. This modular design makes it easy to swap external dependencies (like databases or UI frameworks) without affecting the core business rules.

The architecture is divided into the following layers:

- **PharmacyManagementSystem.Domain:** The core of the system. Defines the essential entities (Medicines, Batches, Users, Sales), domain models, and core repository interfaces. Entirely independent of external frameworks.
- **PharmacyManagementSystem.Application:** Contains all application-specific business logic. This layer implements the use cases, orchestrates the domain entities, defines Data Transfer Objects (DTOs), and handles request/response flow.
- **PharmacyManagementSystem.Infrastructure:** The implementation details. Handles data persistence using Entity Framework Core, interacts with the SQL Server database, and implements the data access repositories defined in the Application layer.
- **PharmacyManagementSystem.API:** A robust, headless RESTful API that securely exposes system functionalities. It acts as the gateway for any frontend application (Web, Mobile, Desktop) to interact with the pharmacy system.
- **PharmacyManagementSystem.WebAppMVC:** The client-facing ASP.NET Core MVC frontend. It consumes the API layer and serves the rich, interactive graphical interface to end users, completely decoupled from direct database access.

## 🛣️ Project Paths & Structure

Navigating the web application follows an intuitive, role-based routing structure:

### **Cashier Paths**
- `/Cashier/Index` - The main Point of Sale (POS) terminal.
- `/Cashier/SearchMedicines` - Dedicated view for querying medicine stock and reading drug details.
- `/Cashier/Profile` - Cashier account and security settings.

### **Admin Paths**
- `/Admin/Dashboard` - The central command center with high-level analytics.
- `/Admin/ManageMedicines` / `/Admin/CreateMedicine` / `/Admin/EditMedicine/...` - Routes for managing the master drug catalogue.
- `/Admin/ManageBatchs` - Routes for handling specific inventory shipments and expiration dates.
- `/Admin/ManageSales` - Interactive ledger for auditing pharmacy transactions.
- `/Admin/ManageUsers` - Portal for employee onboarding and role assignment.
- `/Admin/Reports` - Generation center for business intelligence and financial exports.

## 🛠️ Technology Stack
- **Backend framework:** C#, .NET 10, ASP.NET Core Web API
- **Frontend framework:** ASP.NET Core MVC, Razor Pages
- **Database architecture:** Microsoft SQL Server, Entity Framework Core (Code-First Approach)
- **UI / UX Design:** HTML5, Vanilla CSS structured around a Glassmorphism aesthetic, JavaScript for DOM manipulation, Bootstrap 5 for responsiveness.
- **Authentication pipeline:** Secure Token/Cookie-based authentication flow passing through the API to the MVC client.