# 📚 Library Management System API - Pharos University (PUA)

A powerful and flexible RESTful API built with ASP.NET Core for managing libraries, users, and books across multiple branches of **Pharos University in Alexandria (PUA)**. This project serves as the backend infrastructure for a multi-library management platform that handles book circulation, copying, reading, borrowing, penalties, banning, transferring, discarding, and more.

---

## 🚀 Features

- 🔐 **JWT Authentication** and Role-based Authorization (Admin only access to user management)
- 📦 **Book Inventory Management** across multiple libraries
- 📖 **Borrowing, Reading, and Copying** workflows
- 🛑 **Penalty System** with automated banning logic
- ♻️ **Book Discarding and Transfer** between libraries
- 📊 **Logging System** for tracking all critical data changes
- 🔁 **Unit of Work** and **Generic Repository Pattern** for clean, testable architecture
- 📡 **Linked Server** structure compatible with SQL Server and multiple campuses

---

## 🛠️ Tech Stack

- **Backend Framework:** ASP.NET Core Web API (.NET 6+)
- **Database:** SQL Server
- **ORM:** Entity Framework Core
- **Authentication:** ASP.NET Identity + JWT Bearer Tokens
- **Architecture:** Clean Architecture with Repository & Unit of Work
- **Dependency Injection:** Built-in ASP.NET Core DI
- **Documentation:** Swagger/OpenAPI

---

## 🔐 Roles and Access

- `Admin`: Full control over users, libraries, and book management.
- `User`: Limited to borrowing, reading, and copying books from their assigned library.

---

## 📚 Main Modules

### 🔄 Borrowing
- Link books to users
- Auto-calculate return date
- Auto-check penalties and ban if necessary

### 📥 Copying
- Allows users to request copies of available books
- Reduces the available copies on usage

### 👁️ Reading
- Track book reading activity within the library
- Time-stamped start and end times

### ❌ Discarding
- Soft-remove damaged or outdated books
- Track reason and date of discarding

### 🔁 Transferring
- Move book copies between different libraries
- Auto-create destination `LibraryBook` if it doesn't exist

### 🚫 Penalties & Banning
- Late returns are penalized
- Users are automatically banned if they exceed two penalties

### 📄 Logging
- All critical operations (`Add`, `Update`, `Delete`) are logged with timestamps and usernames

---

## ✅ Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (v6 or higher)
- [SQL Server](https://www.microsoft.com/en-us/sql-server)
- Visual Studio / VS Code

