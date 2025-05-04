# NAME - Student Course Management System

## Table of Contents

1. [Overview](#Overview)  
2. [Features](#features)  
3. [Tech Stack](#tech-stack)  
4. [Folder Structure](#folder-structure)  
5. [Setup Instructions](#setup-instructions)  
6. [Database Design](#database-design)  
7. [API Documentation](#api-documentation)  
8. [UI](#ui)  
9. [Known Issues](#known-issues)  
10. [Contribution Guide](#contribution-guide)
11. [ Updatability & Scalability](#Updatability-&-Scalability)
12. [About the Author](#about-the-author)

## 1. Overview

NAME is a full-stack MVC-based Learning Management System (LMS) built to facilitate online course sharing and enrollment. Unlike traditional LMS platforms tied to specific institutions, this system is designed as an open platform where any instructor can upload and manage their courses—pending approval from an admin. Once approved, students can browse available courses, register for the ones that interest them, and start learning.

 ## 2. Features
### 🔒 Authentication & Authorization
Role-based access: Admin, Instructor, and Student

Secure registration and login for all users

### 👤 Admin Panel
View and manage all users

Approve or reject courses submitted by instructors

Manage platform content

### 👨‍🏫 Instructor Dashboard
Submit new courses (title, description, materials)

View status of submitted courses

Edit or remove their own courses (only before approval)

### 🎓 Student Interface
Browse available and approved courses

View course details before enrolling

Enroll in preferred courses

Access enrolled course content

### 📄 Course Management
Courses contain titles, descriptions, instructors, prices, materials and attached contents

contents may include text, video links, and downloadable files

### 🔎 Search & Filter
Filter courses by category or instructor

Search by keyword

## Tech Stack
- Frontend: HTML, CSS, JavaScript, Razor Views

- Backend: ASP.NET Core MVC (C#)

- Database: SQL Server (EF Core for ORM)

- Architecture: MVC (Model-View-Controller)

- Authentication: ASP.NET Identity

- Development Environment: Visual Studio, SQL Server Management Studio

## 4. Folder Structure
LMS-Project/
  - Controllers/
    - CourseController.cs
    - AccountController.cs
    - AdminController.cs
    - StudentController.cs
    - HomeController.cs
  - Models/
    - Course.cs
    - Material.cs
    - Student.cs
    - Instructor.cs
    - Admin.cs
    - ApplicationUser.cs
  - Views/
    - Account/
      - Login.cshtml
      - Register.cshtml
    - Home/
      - Index.cshtml
      - About.cshtml
      - Contact.cshtml
      - Courses.cshtml
      - Privacy.cshtml
    - Shared/
      - _ViewImports.cshtml
      - _ViewStart.cshtml
  - wwwroot/
    - css/
    - js/
  - appsettings.json
  - Program.cs

## 5. Setup Instructions

### Prerequisites
- .NET SDK 8.0
- Visual Studio 2022 or newer
- SQL Server 20 or later

### Steps
1. Clone the repo:
   ```bash
   git clone https://github.com/mohmadykhaled/project-LMS.git

2. Configure the Database
- Update the appsettings.json with your SQL Server connection string:
   ```bash
   "ConnectionStrings": {"DefaultConnection": "Server=.;Database=LMS_DB;Trusted_Connection=True;"}
- Apply the initial migration:
   ```bash
   Update-Database
3. Build and Run
- Open the solution in Visual Studio

- Set the main project as the startup project

- Press F5 or click Start to run the application

## 6. Database Design
## 7. API Documentation
## 8. UI
## 9. Known Issues
- Instructors currently cannot edit a course once it is submitted for review.

- Course approval doesn't trigger a real-time notification (requires page refresh).

- Role management is hardcoded in some areas—refactoring to a more dynamic claims-based system is planned.

- Some UI elements may need improvement on smaller mobile screens.

## 10. Contribution Guide
- Contributions are welcome! To get started:

    - Fork the repository
      
    - Create a new branch: feature/your-feature-name
    
    - Commit your changes with clear messages
  
    - Push to your fork and create a pull request

- Development Guidelines
  - Follow naming conventions (PascalCase for classes, camelCase for variables)
  
  - Ensure code is formatted and lint-free
  
  - Submit tests where possible

  - Clearly describe what your PR does and which issue it addresses

## 11.  Updatability & Scalability
## 12. About the Author
  
  
