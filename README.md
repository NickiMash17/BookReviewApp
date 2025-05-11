# 📚 Book Review Application

A modern ASP.NET Core MVC application for managing books and their reviews, built with clean architecture principles and best practices.

[![.NET Core](https://img.shields.io/badge/.NET%208.0-512BD4?style=flat&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![Entity Framework Core](https://img.shields.io/badge/EF%20Core%208.0-512BD4?style=flat&logo=.net&logoColor=white)](https://docs.microsoft.com/en-us/ef/core/)
[![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=flat&logo=microsoft-sql-server&logoColor=white)](https://www.microsoft.com/en-us/sql-server)
[![Bootstrap 5](https://img.shields.io/badge/Bootstrap%205-7952B3?style=flat&logo=bootstrap&logoColor=white)](https://getbootstrap.com/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

## 📋 Table of Contents
- [Features](#-features)
- [Architecture](#-architecture)
- [Technology Stack](#-technology-stack)
- [Prerequisites](#-prerequisites)
- [Getting Started](#-getting-started)
- [Configuration](#-configuration)
- [Troubleshooting](#-troubleshooting)
- [Project Structure in Detail](#-project-structure-in-detail)
- [API Documentation](#-api-documentation)
- [Running Tests](#-running-tests)
- [Deployment](#-deployment)
- [Contributing](#-contributing)
- [License](#-license)
- [Contact](#-contact)

## 🚀 Features

- **Book Management**: Create, read, update, and delete books
- **Author Management**: Track authors and their published works
- **Review System**: Add, edit, and moderate reviews with ratings
- **User Authentication**: Secure login and registration system
- **Role-based Authorization**: Admin and regular user roles with different permissions
- **Search Functionality**: Find books by title, author, genre, or keywords
- **Sorting and Filtering**: Organize book listings by various criteria
- **Responsive UI**: Mobile-friendly design using Bootstrap 5
- **Form Validation**: Client and server-side validation
- **Pagination**: Efficient handling of large book collections
- **Data Visualization**: Charts and statistics for book ratings
- **Image Upload**: Book cover image storage and management

## 🏗️ Architecture

The application follows Clean Architecture principles, separating concerns into distinct layers:

```
BookReviewApp/
├── BookReviewApp.Domain/        # Core entities, interfaces, business rules
├── BookReviewApp.Data/          # Data access, EF Core, Repositories
├── BookReviewApp.Services/      # Business logic, Service layer
├── BookReviewApp.Web/           # ASP.NET Core MVC Application
└── BookReviewApp.Tests/         # Unit and Integration tests
```

### Architecture Benefits
- **Maintainability**: Each layer has specific responsibilities
- **Testability**: Business logic can be tested independently
- **Flexibility**: Easy to replace data access or UI technologies
- **Scalability**: Clear boundaries between components

## 🛠️ Technology Stack

### Backend
- **ASP.NET Core 8.0**: Modern, high-performance web framework
- **Entity Framework Core 8.0**: ORM for data access
- **SQL Server Express**: Relational database
- **C# 12**: Latest language features
- **AutoMapper**: Object-to-object mapping
- **FluentValidation**: Advanced validation rules

### Frontend
- **Razor Views**: Server-side rendering
- **Bootstrap 5**: Responsive UI framework
- **JavaScript/jQuery**: Enhanced client-side interactions
- **Font Awesome**: Icons
- **Chart.js**: Data visualization

### Testing
- **xUnit**: Testing framework
- **Moq**: Mocking library
- **FluentAssertions**: Readable assertions
- **Entity Framework Core In-Memory Provider**: Database testing

## ⚙️ Prerequisites

- **IDE**: Visual Studio 2022 (v17.6+) or VS Code with C# extension
- **.NET SDK**: .NET 8.0 SDK
- **Database**: SQL Server Express 2019+ or LocalDB
- **Version Control**: Git 2.30+
- **Optional**: Node.js (for front-end tooling)

## 🚀 Getting Started

### 1. Clone the repository
```powershell
git clone https://github.com/NickiMash17/BookReviewApp.git
cd BookReviewApp
```

### 2. Install dependencies
```powershell
dotnet restore
```

### 3. Update database connection string
Edit `appsettings.json` in the `BookReviewApp.Web` project:

```json
"ConnectionStrings": {
    "DefaultConnection": "Server=.\\SQLEXPRESS;Database=BookReviewApp;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
}
```

### 4. Set up the database
```powershell
# Navigate to solution directory first
cd /path/to/BookReviewApp

# Remove existing migrations (if any)
dotnet ef migrations remove --project BookReviewApp.Data --startup-project BookReviewApp.Web

# Delete the database (if it exists)
dotnet ef database drop --project BookReviewApp.Data --startup-project BookReviewApp.Web --force

# Add new migration
dotnet ef migrations add InitialCreate --project BookReviewApp.Data --startup-project BookReviewApp.Web

# Update database
dotnet ef database update --project BookReviewApp.Data --startup-project BookReviewApp.Web
```

### 5. Run the application
```powershell
dotnet run --project BookReviewApp.Web
```

### 6. Access the application
- Open your browser to `https://localhost:7036` or `http://localhost:5036`
- Default admin credentials: `admin@bookreview.com` / `Admin123!`

## ⚙️ Configuration

### Application Settings
Additional application settings can be configured in `appsettings.json`:

```json
{
  "AppSettings": {
    "SiteTitle": "Book Review App",
    "PageSize": 10,
    "AllowRegistration": true,
    "RequireEmailConfirmation": false,
    "FileUploadSettings": {
      "AllowedExtensions": [".jpg", ".jpeg", ".png"],
      "MaxFileSizeInMB": 5
    }
  }
}
```

### Environment-Specific Configuration
- Development: `appsettings.Development.json`
- Production: `appsettings.Production.json`
- Testing: `appsettings.Testing.json`

## 🔧 Troubleshooting

### Database Issues

#### Connection Problems
- **Error**: Cannot connect to SQL Server
  - **Solution**: Verify SQL Server service is running (Services app > SQL Server)
  - **Solution**: Check if the connection string is correct in `appsettings.json`
  - **Solution**: Ensure firewall allows SQL Server connections

#### Migration Issues
- **Error**: "The database provider attempted to configure the database from scratch..."
  - **Solution**: Run the migration commands in step 4 of Getting Started
  - **Solution**: Ensure the migrations folder exists in BookReviewApp.Data

- **Error**: "Unable to create an object of type 'ApplicationDbContext'"
  - **Solution**: Verify the connection string points to a valid SQL Server instance
  - **Solution**: Check if EF Core tools are installed: `dotnet tool install --global dotnet-ef`

#### Entity Framework Errors
- **Error**: "No database provider has been configured for this DbContext"
  - **Solution**: Ensure `services.AddDbContext<ApplicationDbContext>` is in `Startup.cs` or `Program.cs`

- **Error**: "The entity type requires a primary key"
  - **Solution**: Check entity classes for proper key configuration

### Runtime Issues

#### API/Controller Errors
- **Error**: 404 Not Found
  - **Solution**: Verify route configuration in controllers
  - **Solution**: Check if controller and action names match the URL pattern

- **Error**: 500 Internal Server Error
  - **Solution**: Enable developer exception page in Development environment
  - **Solution**: Check application logs in `/logs` directory

#### Authorization Issues
- **Error**: Access denied or unauthorized
  - **Solution**: Verify user is assigned correct roles
  - **Solution**: Check policy configuration in `Program.cs`

#### File Upload Problems
- **Error**: File too large
  - **Solution**: Adjust `web.config` to allow larger file uploads:
  ```xml
  <system.webServer>
    <security>
      <requestFiltering>
        <requestLimits maxAllowedContentLength="52428800" />
      </requestFiltering>
    </security>
  </system.webServer>
  ```

### Deployment Issues

#### IIS Deployment
- **Error**: Application pool stops unexpectedly
  - **Solution**: Set App Pool to "No Managed Code" if using out-of-process hosting
  - **Solution**: Check Event Viewer for specific .NET runtime errors

- **Error**: Static files not loading
  - **Solution**: Verify `app.UseStaticFiles()` is in `Program.cs`
  - **Solution**: Check web.config for correct MIME type mappings

#### Docker Deployment
- **Error**: Container fails to start
  - **Solution**: Verify Dockerfile has correct base image
  - **Solution**: Check container logs: `docker logs [container_id]`

## 🏗️ Project Structure in Detail

### Domain Layer (`BookReviewApp.Domain`)
- **Entities**: Core business objects (Book, Author, Review, etc.)
- **Interfaces**: Repository and service contracts
- **Enums**: Common enumerations (BookGenre, RatingLevel, etc.)
- **Value Objects**: Immutable objects (Address, ISBN, etc.)
- **Domain Events**: Business events for domain-driven design
- **Custom Exceptions**: Domain-specific exceptions

### Data Layer (`BookReviewApp.Data`)
- **ApplicationDbContext**: EF Core database context
- **Repository Implementations**: Generic and specific repositories
- **Migrations**: Database schema changes
- **Configurations**: Entity type configurations (Fluent API)
- **Seeding**: Initial data setup
- **Query Specifications**: Reusable query patterns

### Service Layer (`BookReviewApp.Services`)
- **Service Implementations**: Business logic
- **DTOs**: Data transfer objects
- **Validators**: Input validation rules
- **Mapping Profiles**: AutoMapper configurations
- **Background Services**: Scheduled or long-running tasks
- **External API Integrations**: Third-party service clients

### Web Layer (`BookReviewApp.Web`)
- **Controllers**: Handle HTTP requests
- **Views**: Razor pages
- **View Models**: UI-specific models
- **Middleware**: Request/response processing
- **Filters**: Action and exception filters
- **Tag Helpers**: Custom HTML element generation
- **Areas**: Feature-specific sections (Admin, User, etc.)
- **wwwroot**: Static files (CSS, JS, images)

### Tests (`BookReviewApp.Tests`)
- **Unit Tests**: Testing individual components
- **Integration Tests**: Testing component interactions
- **Functional Tests**: Testing complete features
- **Fixtures**: Test data setup
- **Mocks & Stubs**: Test doubles

## 📘 API Documentation

The application provides a Swagger UI for API documentation when running in Development mode.

Access the Swagger UI at: `/swagger`

## 🧪 Running Tests

```powershell
# Run all tests
dotnet test

# Run specific test project
dotnet test BookReviewApp.Tests

# Run tests with coverage
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover

# Run specific test category
dotnet test --filter "Category=Integration"
```

## 🚢 Deployment

### IIS Deployment
1. Publish the application:
```powershell
dotnet publish -c Release -o ./publish
```

2. Set up an IIS website pointing to the publish directory
3. Configure application pool (.NET Core)
4. Set up the SQL Server database
5. Update connection strings in production `appsettings.json`

### Docker Deployment
```powershell
# Build the Docker image
docker build -t bookreviewapp .

# Run the container
docker run -d -p 8080:80 --name bookreview bookreviewapp
```

### Azure Deployment
1. Create an Azure App Service
2. Set up CI/CD pipeline or deploy directly from Visual Studio
3. Configure Azure SQL Database
4. Set connection string in App Service Configuration

## 🤝 Contributing

We welcome contributions to improve the Book Review Application!

### Contribution Process
1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

### Coding Guidelines
- Follow C# coding conventions
- Include XML comments for public APIs
- Write unit tests for new features
- Update documentation as needed

## 📝 License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.

## 📞 Contact

Nicolette Mashaba - [@m_neyi](https://twitter.com/m_neyi)

Project Link: [https://github.com/NickiMash17/BookReviewApp](https://github.com/NickiMash17/BookReviewApp)

---

*This project was built with ❤️ by Nicolette Mashaba*
