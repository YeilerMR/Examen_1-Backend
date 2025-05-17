# Examen_1-Backend  
API developed in .NET for the first exam.

------------

## Description

This project is a server developed in .NET that implements a REST API to manage **courses** and **students**. It provides full CRUD operations and uses SQL Server as its database.

---

## 🧱 Features

- **REST API** built with ASP.NET Core.
- CRUD operations for:
  - **Courses**
  - **Students**
- Integration with **SQL Server**.
- Automatic API documentation using **Swagger**.

---

## 💻 Technologies Used

- [.NET 9](https://dotnet.microsoft.com/en-us/download) or higher.
- **REST API** architecture.
- **SQL Server** as the database.

---

## 🚀 Getting Started

Follow these steps to set up and run the project:

1. **Check your .NET version**:
   ```bash
   dotnet --version  
   ```
2. **Clone the repository:**
   ```bash
   git clone https://github.com/YeilerMR/Examen_1-Backend.git
   ```
3. **Open the project** in your preferred IDE (VS Code, Cursor, IntelliJ IDEA, etc.).
4. **Configure the connection string** in the **`appsettings.json`** file to point to your SQL Server instance.
5. **Apply migrations** to set up the database:
   ```bash
   dotnet ef database update
   ```
6. **Build and run the project:**
   ```bash
   dotnet build
   dotnet watch run
   ```

---

## 📘 API Documentation

You can test the endpoints using **Swagger**:
```bash
http://localhost:8080/swagger/v1/swagger.json
```
Or access the following link directly after running the project: [API Link](http://localhost:8080/swagger/v1/swagger.json)

---

## 📁 Project Structure

```none
EXAM1_API/
│
├── Controllers/            # API Endpoints
├── Data/                   # Database context and configuration
├── Dtos/                   # Data Transfer Objects
├── Helpers/                # Utility classes and methods (FirebaseHelper)
├── Mappers/                # AutoMapper profiles or manual mappings
├── Migrations/             # Entity Framework migrations
├── Models/                 # Entities (e.g., Course, Student)
└── README.md               # Project documentation
```

---

## 🤝 Authors

- **Yeiler Montes Rojas**  
  **GitHub:** [YeilerMR](https://github.com/YeilerMR)

- **Aaron Matarrita Portuguez**  
  **GitHub:** [AaronMatarrita](https://github.com/AaronMatarrita)
