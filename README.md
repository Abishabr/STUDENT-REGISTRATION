# Student Record Management System

A Windows desktop application built with **VB.NET (.NET 10)** and **Windows Forms** for managing student records. It features a professional UI, secure login, full CRUD operations, real-time search, CSV export, and print support — all backed by a **MySQL** database.

---

## Features

- **Splash Screen** — Animated loading screen on startup
- **Secure Login** — Email + password authentication with BCrypt hashing (max 5 attempts)
- **Dashboard** — Live statistics cards showing total, male, and female student counts
- **Add / Edit Students** — Form with full validation (required fields, email format, phone format, duplicate checks)
- **View & Manage Records** — Sortable DataGridView with real-time search across ID, name, and department
- **Delete Records** — Confirmation dialog before permanent deletion
- **Export to CSV** — Save all visible records to a `.csv` file
- **Print Report** — Print preview with paginated student report
- **Keyboard Shortcuts** — `Ctrl+N` (new), `Ctrl+S` (save), `Ctrl+F` (search), `Ctrl+E` (export), `Ctrl+P` (print)
- **Status Bar** — Live feedback showing current action and timestamp

---

## Tech Stack

| Layer | Technology |
|---|---|
| Language | Visual Basic .NET |
| Framework | .NET 10 (Windows) |
| UI | Windows Forms |
| Database | MySQL (via XAMPP) |
| ORM / DB Driver | MySqlConnector 2.5.0 |
| Password Hashing | BCrypt.Net-Next 4.0.2 |

---

## Project Structure

```
Student Record Management System/
├── Form1.vb                  # Main application form (dashboard, CRUD, export, print)
├── LoginForm.vb              # Login screen with email/password authentication
├── SplashForm.vb             # Animated splash screen
├── ApplicationEvents.vb      # VB.NET application startup events
├── Models/
│   └── Student.vb            # Student data model
└── Helpers/
    ├── DatabaseHelper.vb     # All MySQL database operations (CRUD + auth)
    └── ValidationHelper.vb   # Reusable input validation (email, phone, required)
```

---

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [XAMPP](https://www.apachefriends.org/) (or any MySQL server running on port 3306)
- Visual Studio 2022+ (recommended) or `dotnet` CLI

---

## Getting Started

### 1. Set up the database

Start MySQL via XAMPP (or your preferred MySQL server) and create the database:

```sql
CREATE DATABASE student_records;
```

The application will automatically create the required tables (`students`, `users`) on first launch.

### 2. Configure the connection string

Open `Helpers/DatabaseHelper.vb` and update the connection string if needed:

```vb
Private Shared ReadOnly ConnStr As String =
    "Server=127.0.0.1;Port=3306;Database=student_records;Uid=root;Pwd=;SslMode=Preferred;"
```

### 3. Build and run

```bash
dotnet build
dotnet run
```

Or open `Student Record Management System.slnx` in Visual Studio and press **F5**.

---

## Default Login Credentials

| Field | Value |
|---|---|
| Email | `admin@gmail.com` |
| Password | `12345678` |

> The default admin account is seeded automatically when the `users` table is empty.

---

## Validation Rules

- **First Name, Last Name, Gender, Department** — Required
- **Email** — Required, must match standard email format, must be unique
- **Phone** — Optional, 7–15 digits (allows `+`, `-`, spaces), must be unique
- **Date of Birth** — Must result in an age between 10 and 120 years

---

## License

This project is intended for educational purposes.
