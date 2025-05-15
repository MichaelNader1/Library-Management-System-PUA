# 📚 Library Management System API - Pharos University (PUA)

## 📌 About the Project

This project was developed for **Pharos University** to serve as a complete website solution.
It is currently **active** and **in use** by the university staff and students, providing real, functional services within the university environment.

A powerful and flexible RESTful API built with ASP.NET Core for managing libraries, users, and books across multiple branches of **Pharos University in Alexandria (PUA)**. This project serves as the backend infrastructure for a multi-library management platform that handles book circulation, copying, reading, borrowing, penalties, banning, transferring, discarding, and more.

---

## 🚀 Technologies Used

- ASP.NET Core Web API (.NET 6)
- Entity Framework Core (Code-First)
- SQL Server
- ASP.NET Identity with Cookie-based Authentication
- LINQ & EF Includes
- Repository Pattern + Unit of Work Pattern
- Swagger (API documentation)
- CORS Policy (for security)

---

## ✨ Features

- 🏛 Multi-Library Structure (Each admin manages one library)
- 📖 Book Borrowing, Reading, Copying & Transferring between libraries
- ❌ Discarding books with reason tracking
- ⏱ Penalty System for late returns (automatic ban if exceeded)
- 🔐 Cookie-based Authentication with Role-based Authorization
- 📊 Logging of all Add/Update/Delete operations
- 👨‍🏫 Admin-exclusive user management (add, edit, delete, assign to library)
- 📎 Soft validations (ex: prevent borrowing locked or unavailable books)
- 📑 RESTful API with clean structure and navigation properties
- 🧪 Fully testable with Swagger UI

---

## 🔐 Authentication

The system uses **ASP.NET Core Identity** with **Cookie-based Authentication**.

- Admins authenticate via login using cookies (no public registration).
- Admins can:
  - Register new users
  - Assign users to libraries
  - Edit user roles or libraries
  - Delete users
- Role checking is handled using `UserManager.GetRolesAsync(user)`.

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

---

🔒 **Copyright © 2025  
Michael Nader**  
Software Engineering 
Department of Information Technology
PUA - Alexandria, Egypt 🇪🇬


