# 🔐 Authentication Service

> A complete Authentication & Authorization platform built with ASP.NET Core Web API, SQL Server, Angular, and Windows Forms following professional Software Engineering practices.

![Version](https://img.shields.io/badge/version-1.0.0-blue)
![Platform](https://img.shields.io/badge/.NET-ASP.NET_Core_Web_API-purple)
![Database](https://img.shields.io/badge/Database-SQL_Server-red)
![Frontend](https://img.shields.io/badge/Frontend-Angular-DD0031)
![Desktop](https://img.shields.io/badge/Desktop-Windows_Forms-512BD4)
![License](https://img.shields.io/badge/license-MIT-green)

---

# 📖 Overview

Authentication Service is a full-stack authentication and authorization system designed to demonstrate real-world software engineering principles.

Unlike many tutorial projects, this application was developed incrementally, allowing the architecture to evolve naturally while maintaining simplicity in Version 1.

The project includes:

- ASP.NET Core Web API
- SQL Server Database
- Angular Dashboard
- Windows Forms Desktop Client

---

# ✨ Features

## User Management

- Create Users
- Update Users
- Delete Users
- Activate / Deactivate Users
- Search Users
- User Details

---

## Authentication

- Login
- Logout
- Password Verification
- Password Change

---

## Profile Management

- View Profile
- Update Profile

---

## Role Management

- Create Roles
- Update Roles
- Delete Roles
- Permissions Mask

---

## Permission Management

- Create Permissions
- Update Permissions
- Delete Permissions
- Assign Permissions to Roles

---

## Login History

- Login Time
- Success / Failure
- Failure Reason
- IP Address
- Browser
- Device

---

## Audit Logs

Tracks important operations such as:

- User Creation
- User Update
- Password Change
- Login
- Logout
- Permission Changes

---

# 🏛 Architecture

Version 1 follows a traditional N-Tier Architecture.

Presentation

↓

ASP.NET Core Web API

↓

Business Layer

↓

Repository Layer (ADO.NET)

↓

SQL Server

---

# 🛠 Technologies

### Backend

- C#
- ASP.NET Core Web API
- ADO.NET

### Database

- Microsoft SQL Server
- Stored Procedures

### Frontend

- Angular

### Desktop

- Windows Forms

### Tools

- Visual Studio 2022
- SQL Server Management Studio
- Git
- GitHub
- Postman

---

# 📦 Project Structure

```text
AuthenticationService

├── Source
│   ├── AuthenticationService.API
│   ├── AuthenticationService.Business
│   ├── AuthenticationService.Repository
│   ├── AuthenticationService.WinForms
│   └── AuthenticationService.Angular
│
├── Database
│   ├── Tables
│   ├── Stored Procedures
│   ├── Seed Data
│   └── Functions
│
└── README.md
```

---

# 🗄 Database

Main Tables

- Users
- Roles
- Permissions
- Status
- LoginHistory
- AuditLog
- Entities
- OperationTypes

---

# 🔌 API

RESTful API built with ASP.NET Core.

Examples:

```
POST /api/auth/login

POST /api/auth/change-password

GET /api/users

POST /api/users

PUT /api/users/{id}

DELETE /api/users/{id}
```

---

# 🚀 Getting Started

## Clone Repository

```bash
git clone https://github.com/YOUR_USERNAME/AuthenticationService.git
```

---

## Database

Execute SQL scripts in the following order:

1. Create Database
2. Tables
3. Seed Data
4. Functions
5. Stored Procedures

---

## Backend

Update

```
appsettings.json
```

with your SQL Server connection string.

Run:

```bash
dotnet run
```

---

## Angular

```bash
npm install

ng serve
```

---

# 📸 Screenshots

Coming Soon

---

# 🛣 Roadmap

## Version 2

- Dependency Injection
- Repository Interfaces
- DTOs
- Async/Await
- JWT Authentication
- Refresh Tokens
- Password Hashing
- Email Verification
- Password Recovery

---

## Version 3

- Clean Architecture
- Docker
- Unit Testing
- Integration Testing
- CI/CD

---

# 📄 License

This project is licensed under the MIT License.

---

# 👨‍💻 Author

Ahmed Mahmoud Kelany

Software Engineering Student

AI & Backend Developer

GitHub:

https://github.com/YOUR_USERNAME
