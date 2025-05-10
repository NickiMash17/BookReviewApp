# 📚 Book Review Application

A modern ASP.NET Core MVC application for managing books and their reviews, built with clean architecture principles.

## 🚀 Features

- Book management (Add, Edit, Delete, List)
- Author management
- Clean Architecture implementation
- Generic Repository Pattern
- Entity Framework Core with SQL Server
- Dependency Injection
- Asynchronous operations

## 🏗️ Architecture

```
BookReviewApp/
├── BookReviewApp.Domain/        # Core entities, interfaces
├── BookReviewApp.Data/          # Data access, EF Core, Repositories
├── BookReviewApp.Services/      # Business logic, Service layer
├── BookReviewApp.Web/           # ASP.NET Core MVC Application
└── BookReviewApp.Tests/         # Unit and Integration tests
```

## 🛠️ Technology Stack

- ASP.NET Core 8.0
- Entity Framework Core 8.0
- SQL Server Express
- C# 12
- Bootstrap 5
- JavaScript/jQuery

## ⚙️ Prerequisites

- Visual Studio 2022 or VS Code
- .NET 8.0 SDK
- SQL Server Express
- Git

## 🚀 Getting Started

1. **Clone the repository**
```powershell
git clone https://github.com/NickiMash17/BookReviewApp.git
cd BookReviewApp
```

2. **Update database connection string** (if needed) in `appsettings.json`:
```json
"ConnectionStrings": {
    "DefaultConnection": "Server=.\\SQLEXPRESS;Database=BookReviewApp;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
}
```

3. **Apply database migrations**
```powershell
dotnet ef database update --project BookReviewApp.Data --startup-project BookReviewApp.Web
```

4. **Run the application**
```powershell
dotnet run --project BookReviewApp.Web
```

## 🏗️ Project Structure

### Domain Layer
- Contains entities and interfaces
- No dependencies on other projects
- Defines core business logic

### Data Layer
- Implements repositories
- Manages database context
- Handles data access concerns

### Service Layer
- Implements business logic
- Coordinates between repositories
- Handles data transformations

### Web Layer
- MVC controllers and views
- Dependency injection configuration
- User interface implementation

## 🧪 Running Tests

```powershell
dotnet test
```

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Open a Pull Request

## 📝 License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.

## 📞 Contact

Nicolette Mashaba - [@m_neyi](https://twitter.com/m_neyi)
Project Link: [https://github.com/NickiMash17/BookReviewApp](https://github.com/NickiMash17/BookReviewApp)
