# Student Record Management System
## Project Documentation

---

**Course:** Information Systems / Software Development  
**Version:** 1.0  
**Date:** May 2026  
**Platform:** Windows Desktop Application  
**Language:** Visual Basic .NET (.NET 10)

---

## Table of Contents

1. [Background Information](#1-background-information)
2. [Statement of the Problem](#2-statement-of-the-problem)
3. [Justification for the Proposed Solution](#3-justification-for-the-proposed-solution)
4. [List of Technologies Used](#4-list-of-technologies-used)
5. [System Architecture](#5-system-architecture)
6. [Interface Screenshots](#6-interface-screenshots)
7. [Database Operation Screenshots](#7-database-operation-screenshots)
8. [Validation and Error Handling](#8-validation-and-error-handling)
9. [Keyboard Shortcuts Reference](#9-keyboard-shortcuts-reference)
10. [Conclusion](#10-conclusion)

---

## 1. Background Information

Educational institutions manage large volumes of student data on a daily basis. This data includes personal information such as names, dates of birth, and contact details, as well as academic information such as department enrollment and registration dates. Traditionally, many schools and colleges have relied on paper-based filing systems or basic spreadsheet tools like Microsoft Excel to store and manage this information.

While these approaches may work at a small scale, they become increasingly difficult to manage as student populations grow. Paper records are prone to physical damage, loss, and unauthorized access. Spreadsheets lack proper access control, do not enforce data integrity, and make it difficult to search, update, or report on records efficiently.

The **Student Record Management System (SRMS)** was developed to address these challenges by providing a dedicated, secure, and user-friendly desktop application for managing student records. The system is designed for use by administrative staff in schools, colleges, or training institutions who need a reliable tool to perform day-to-day record-keeping tasks.

The application was built using **Visual Basic .NET** on the **.NET 10** framework with a **MySQL** relational database backend, providing a robust and maintainable solution that can be deployed on any Windows machine with minimal setup.

---

## 2. Statement of the Problem

Many educational institutions face the following challenges in managing student records:

1. **Lack of centralized storage** — Student data is scattered across multiple files, folders, or spreadsheets, making it difficult to find and update records quickly.

2. **No access control** — Spreadsheet-based systems do not restrict who can view or modify records, creating a risk of unauthorized changes or data breaches.

3. **Data integrity issues** — Without validation rules, duplicate records (same email or phone number) and incomplete entries are common, leading to unreliable data.

4. **Inefficient search and retrieval** — Finding a specific student record in a large spreadsheet or paper file requires manual scanning, which is time-consuming and error-prone.

5. **No audit trail or reporting** — There is no easy way to generate reports, export data for analysis, or print formatted student lists.

6. **Scalability limitations** — As the number of students grows, spreadsheet-based systems become slow and unmanageable.

These problems result in wasted administrative time, data inconsistencies, and potential privacy risks for students.

---

## 3. Justification for the Proposed Solution

The Student Record Management System was proposed as a solution for the following reasons:

### 3.1 Dedicated Database Backend
By using **MySQL** as the database engine, the system enforces data integrity through constraints (e.g., unique email and phone fields), supports efficient querying, and scales well as the number of records grows. Unlike spreadsheets, the database ensures that duplicate or malformed records cannot be saved.

### 3.2 Secure Authentication
The system requires users to log in with an email address and password before accessing any records. Passwords are stored using **BCrypt hashing**, a one-way cryptographic algorithm, meaning that even if the database is compromised, raw passwords cannot be recovered. Login attempts are also limited to five before the account is locked, preventing brute-force attacks.

### 3.3 User-Friendly Windows Forms Interface
A graphical desktop interface was chosen over a web or command-line interface because the target users are administrative staff who are familiar with Windows applications. The interface uses a professional color scheme, clear navigation, and real-time feedback to minimize the learning curve.

### 3.4 Input Validation
All form fields are validated before data is saved. Required fields are enforced, email addresses are checked against a standard format pattern, phone numbers are restricted to valid formats, and duplicate entries are detected and rejected. This ensures the database always contains clean, reliable data.

### 3.5 Export and Reporting
Administrative staff often need to share student data with other departments or generate printed reports. The system supports **CSV export** for use in spreadsheet tools and a **print preview** feature for generating formatted paper reports.

### 3.6 Low Cost and Easy Deployment
The application uses free, open-source technologies (MySQL via XAMPP, .NET 10) and runs on standard Windows hardware. No expensive software licenses are required, making it accessible to institutions with limited budgets.

---

## 4. List of Technologies Used

| # | Technology | Version | Purpose |
|---|---|---|---|
| 1 | Visual Basic .NET | .NET 10 | Primary programming language and application framework |
| 2 | Windows Forms (WinForms) | .NET 10 | Graphical user interface framework |
| 3 | MySQL | 8.x (via XAMPP) | Relational database for persistent data storage |
| 4 | MySqlConnector | 2.5.0 | ADO.NET driver for connecting VB.NET to MySQL |
| 5 | BCrypt.Net-Next | 4.0.2 | Password hashing library for secure credential storage |
| 6 | Microsoft.Data.Sqlite | 9.0.4 | Referenced in project (SQLite support) |
| 7 | XAMPP | Latest | Local MySQL server environment for development |
| 8 | Visual Studio 2022 | Latest | Integrated development environment (IDE) |
| 9 | Git & GitHub | — | Version control and source code hosting |

### Key Libraries Detail

**MySqlConnector** — A high-performance, fully async ADO.NET driver for MySQL. It is used throughout `DatabaseHelper.vb` to open connections, execute parameterized SQL commands, and read results into `DataTable` objects.

**BCrypt.Net-Next** — Implements the bcrypt password hashing algorithm. When a user account is created, the password is hashed using `BCrypt.HashPassword()`. During login, `BCrypt.Verify()` compares the entered password against the stored hash without ever decrypting it.

**Windows Forms** — Provides all UI components including `Form`, `Panel`, `Button`, `TextBox`, `ComboBox`, `DateTimePicker`, `DataGridView`, `StatusStrip`, and `ErrorProvider`. All controls in this project are created **programmatically** (no Designer drag-and-drop) for cleaner, more maintainable code.

---

## 5. System Architecture

The application follows a **layered architecture** with clear separation of concerns:

```
┌─────────────────────────────────────────────┐
│              Presentation Layer              │
│  Form1.vb · LoginForm.vb · SplashForm.vb    │
│  (Windows Forms UI — all controls coded)    │
└────────────────────┬────────────────────────┘
                     │
┌────────────────────▼────────────────────────┐
│               Helper / Logic Layer           │
│  DatabaseHelper.vb  ·  ValidationHelper.vb  │
│  (Database operations · Input validation)   │
└────────────────────┬────────────────────────┘
                     │
┌────────────────────▼────────────────────────┐
│                  Data Layer                  │
│         MySQL Database (student_records)     │
│         Tables: students · users             │
└─────────────────────────────────────────────┘
```

### Database Schema

**Table: `students`**

| Column | Type | Constraints |
|---|---|---|
| student_id | INT | PRIMARY KEY, AUTO_INCREMENT |
| first_name | VARCHAR(100) | NOT NULL |
| last_name | VARCHAR(100) | NOT NULL |
| gender | VARCHAR(20) | NOT NULL |
| date_of_birth | DATE | NOT NULL |
| department | VARCHAR(150) | NOT NULL |
| phone_number | VARCHAR(30) | DEFAULT NULL |
| email | VARCHAR(255) | DEFAULT NULL |
| address | TEXT | — |
| registration_date | DATETIME | NOT NULL |

**Table: `users`**

| Column | Type | Constraints |
|---|---|---|
| user_id | INT | PRIMARY KEY, AUTO_INCREMENT |
| username | VARCHAR(255) | NOT NULL, UNIQUE |
| password_hash | VARCHAR(255) | NOT NULL |
| full_name | VARCHAR(255) | — |
| role | VARCHAR(50) | DEFAULT 'Admin' |
| created_at | DATETIME | NOT NULL |

---

## 6. Interface Screenshots

> **Note:** Replace each placeholder below with the actual screenshot from the running application.

---

### 6.1 Splash Screen

The splash screen is displayed automatically when the application launches. It shows the application name, version number, and an animated progress bar while the main form initializes in the background.

```
[ SCREENSHOT: Splash screen with animated progress bar ]
```

---

### 6.2 Login Screen

After the splash screen, the login form is presented. Users must enter a valid email address and password. The form includes a "Show Password" toggle and displays an error message for invalid credentials. After 5 failed attempts, the login button is disabled.

```
[ SCREENSHOT: Login form — empty state ]
```

```
[ SCREENSHOT: Login form — showing an error message for wrong credentials ]
```

---

### 6.3 Dashboard

After a successful login, the main dashboard is displayed. It shows a personalized welcome message with the logged-in user's name and three statistics cards showing the total number of students, the number of male students, and the number of female students. The left sidebar contains the main navigation.

```
[ SCREENSHOT: Dashboard with statistics cards and navigation sidebar ]
```

---

### 6.4 Add New Student Form

Clicking "Add Student" in the navigation opens the student entry form. All required fields are clearly labeled. The department field supports both selecting from existing departments and typing a new one. The form header shows "Add New Student" and the save button is green.

```
[ SCREENSHOT: Add New Student form — empty ]
```

```
[ SCREENSHOT: Add New Student form — filled in with sample data ]
```

---

### 6.5 View / Manage Students

The "View Students" page displays all student records in a sortable DataGridView. A real-time search bar at the top filters records as the user types. Each row can be selected to enable the Edit and Delete buttons.

```
[ SCREENSHOT: View Students page with records in the DataGridView ]
```

```
[ SCREENSHOT: View Students page — search results filtered by a keyword ]
```

---

### 6.6 Edit Student Form

Double-clicking a row or clicking the Edit button opens the same form pre-populated with the selected student's data. The form header changes to "Edit Student Record", the ID being edited is shown, and the save button changes to blue labeled "Update Student".

```
[ SCREENSHOT: Edit Student form pre-populated with existing student data ]
```

---

### 6.7 Export to CSV

Clicking "Export CSV" in the navigation opens a Save File dialog. The exported file contains all visible columns with proper CSV formatting and UTF-8 encoding.

```
[ SCREENSHOT: Save File dialog for CSV export ]
```

---

### 6.8 Print Report

Clicking "Print Report" opens a Print Preview dialog showing a formatted, paginated student report with the system title, generation date, total count, and a table of student records.

```
[ SCREENSHOT: Print Preview dialog showing the formatted report ]
```

---

## 7. Database Operation Screenshots

> **Note:** Replace each placeholder below with a screenshot showing the operation in the application alongside the result in the database (e.g., via phpMyAdmin or MySQL Workbench).

---

### 7.1 Add (INSERT) Operation

**Steps:**
1. Navigate to "Add Student" from the sidebar.
2. Fill in all required fields: First Name, Last Name, Gender, Date of Birth, Department, and Email.
3. Optionally fill in Phone and Address.
4. Click "Save Student".
5. A success message confirms the record was added.

```
[ SCREENSHOT: Filled Add Student form before clicking Save ]
```

```
[ SCREENSHOT: Success message dialog — "Student added successfully!" ]
```

```
[ SCREENSHOT: phpMyAdmin / MySQL Workbench showing the new row in the students table ]
```

**SQL executed internally:**
```sql
INSERT INTO students 
  (first_name, last_name, gender, date_of_birth, department, phone_number, email, address, registration_date)
VALUES 
  (@fn, @ln, @g, @dob, @dept, @phone, @email, @addr, @regdate);
```

---

### 7.2 Read (SELECT) Operation

**Steps:**
1. Navigate to "View Students" from the sidebar.
2. All records are loaded automatically, ordered by most recently added.
3. Type in the search box to filter by ID, first name, last name, or department.

```
[ SCREENSHOT: View Students page showing all records loaded in the DataGridView ]
```

```
[ SCREENSHOT: Search box with a keyword entered and filtered results displayed ]
```

**SQL executed internally:**
```sql
-- Load all records
SELECT student_id AS StudentID, first_name AS FirstName, last_name AS LastName,
       gender AS Gender, date_of_birth AS DateOfBirth, department AS Department,
       phone_number AS PhoneNumber, email AS Email, address AS Address,
       registration_date AS RegistrationDate
FROM students
ORDER BY student_id DESC;

-- Search
SELECT ... FROM students
WHERE CAST(student_id AS CHAR) LIKE @q
   OR first_name LIKE @q
   OR last_name LIKE @q
   OR department LIKE @q
ORDER BY student_id DESC;
```

---

### 7.3 Update (UPDATE) Operation

**Steps:**
1. On the "View Students" page, select a student row.
2. Click "Edit Selected" or double-click the row.
3. The Edit form opens pre-populated with the student's current data.
4. Modify the desired fields.
5. Click "Update Student".
6. A confirmation dialog asks "Are you sure you want to update this student record?"
7. Click Yes. A success message confirms the update.

```
[ SCREENSHOT: Edit Student form with modified data before clicking Update ]
```

```
[ SCREENSHOT: Update confirmation dialog ]
```

```
[ SCREENSHOT: Success message — "Student record updated successfully!" ]
```

```
[ SCREENSHOT: phpMyAdmin / MySQL Workbench showing the updated row ]
```

**SQL executed internally:**
```sql
UPDATE students
SET first_name = @fn, last_name = @ln, gender = @g,
    date_of_birth = @dob, department = @dept,
    phone_number = @phone, email = @email, address = @addr
WHERE student_id = @id;
```

---

### 7.4 Delete (DELETE) Operation

**Steps:**
1. On the "View Students" page, select a student row.
2. Click "Delete Selected".
3. A warning dialog asks: "Are you sure you want to permanently delete this student record? This action cannot be undone."
4. Click Yes. A success message confirms the deletion.
5. The DataGridView refreshes and the record is no longer visible.

```
[ SCREENSHOT: Delete confirmation warning dialog ]
```

```
[ SCREENSHOT: Success message — "Student record deleted successfully!" ]
```

```
[ SCREENSHOT: DataGridView after deletion — record is gone ]
```

```
[ SCREENSHOT: phpMyAdmin / MySQL Workbench confirming the row no longer exists ]
```

**SQL executed internally:**
```sql
DELETE FROM students WHERE student_id = @id;
```

---

### 7.5 Duplicate Validation (Integrity Check)

The system prevents duplicate email addresses and phone numbers from being saved. If a user attempts to add or update a student with an email or phone that already exists in the database, an inline error is shown on the field and the record is not saved.

```
[ SCREENSHOT: Add Student form showing "This email is already registered" error ]
```

```
[ SCREENSHOT: Add Student form showing "This phone number is already registered" error ]
```

**SQL executed internally:**
```sql
-- Email check
SELECT COUNT(*) FROM students WHERE email = @email AND student_id <> @id;

-- Phone check
SELECT COUNT(*) FROM students WHERE phone_number = @phone AND student_id <> @id;
```

---

## 8. Validation and Error Handling

The application performs the following validations before any record is saved:

| Field | Rule |
|---|---|
| First Name | Required — cannot be empty or whitespace |
| Last Name | Required — cannot be empty or whitespace |
| Gender | Required — must select from dropdown (Male / Female / Other) |
| Department | Required — must select or type a department |
| Date of Birth | Must result in an age between 10 and 120 years |
| Email | Required, must match standard email format, must be unique in the database |
| Phone | Optional, but if provided must be 7–15 characters (digits, `+`, `-`, spaces), must be unique |

Errors are displayed using the built-in `ErrorProvider` component, which shows a red icon next to the invalid field with a tooltip describing the problem. A summary message is also shown in the status bar at the bottom of the window.

---

## 9. Keyboard Shortcuts Reference

| Shortcut | Action |
|---|---|
| `Ctrl + N` | Open Add New Student form |
| `Ctrl + S` | Save / Update student (when form is open) |
| `Ctrl + U` | Navigate to View Students |
| `Ctrl + F` | Navigate to View Students and focus the search box |
| `Ctrl + E` | Export records to CSV |
| `Ctrl + P` | Open Print Preview |

---

## 10. Conclusion

The Student Record Management System successfully addresses the core problems of manual and spreadsheet-based student record keeping. By combining a structured relational database with a secure, validated, and user-friendly Windows Forms interface, the system provides:

- **Reliability** — Data integrity enforced at both the application and database levels
- **Security** — BCrypt-hashed passwords and login attempt limiting
- **Efficiency** — Real-time search, keyboard shortcuts, and instant feedback
- **Usability** — Clean, professional interface designed for non-technical administrative staff
- **Portability** — Runs on any Windows machine with .NET 10 and a MySQL server

The system is built on free, open-source technologies, making it cost-effective for educational institutions of any size. Future enhancements could include role-based access control (e.g., read-only staff vs. admin), student photo uploads, academic grade tracking, and a web-based interface for remote access.

---

*End of Documentation*
