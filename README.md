# 📚 Book Review App

![Original Work](https://img.shields.io/badge/Handcrafted%20by-Nicolette%20Mashaba-blueviolet?style=for-the-badge)

> **Original work by Nicolette Mashaba (nickimash). All UI/UX, code, and design are handcrafted and proudly owned.**

A modern, full-stack web application for managing books, authors, and reviews. Built with ASP.NET Core, Entity Framework, and Bootstrap 5.

![Book Review App](https://img.shields.io/badge/.NET-6.0-blue?style=for-the-badge&logo=.net)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-6.0-purple?style=for-the-badge&logo=aspnet)
![Entity Framework](https://img.shields.io/badge/Entity%20Framework-6.0-green?style=for-the-badge&logo=entity-framework)
![Bootstrap](https://img.shields.io/badge/Bootstrap-5.0-blue?style=for-the-badge&logo=bootstrap)

## 🎯 Features

### ✨ Core Functionality
- **Book Management**: Add, edit, delete, and view books with cover images
- **Author Management**: Complete author profiles with biographies and photos
- **Review System**: 5-star rating system with optional comments
- **Search & Filter**: Real-time search and sorting capabilities
- **Responsive Design**: Beautiful UI that works on all devices

### 📊 Advanced Features
- **Analytics Dashboard**: Interactive charts and statistics
- **Real-time Search**: Instant search across books and authors
- **Rating System**: Visual star ratings with average calculations
- **Image Handling**: Cover images and author photos with fallbacks
- **Data Relationships**: Proper foreign key relationships and includes

### 🎨 UI/UX Highlights
- **Modern Design**: Clean, professional interface with gradients and animations
- **Interactive Elements**: Hover effects, smooth transitions, and overlays
- **Bootstrap Icons**: Consistent iconography throughout the application
- **Mobile-First**: Responsive design optimized for all screen sizes
- **Loading States**: Proper error handling and user feedback

## 🏗️ Architecture

### 📁 Project Structure
```
BookReviewApp/
├── BookReviewApp.Domain/          # Domain models and interfaces
├── BookReviewApp.Infrastructure/  # Data access and repositories
├── BookReviewApp.Services/        # Business logic and services
└── BookReviewApp.Web/            # Web application and controllers
```

### 🔧 Technology Stack
- **Backend**: ASP.NET Core 6.0, Entity Framework Core
- **Database**: SQLite (with migration support)
- **Frontend**: Bootstrap 5, JavaScript, Chart.js
- **Icons**: Bootstrap Icons
- **Architecture**: Repository Pattern, Service Layer, Dependency Injection

### 🎯 Design Patterns
- **Repository Pattern**: Clean data access abstraction
- **Service Layer**: Business logic separation
- **Dependency Injection**: Loose coupling and testability
- **MVC Pattern**: Clear separation of concerns

## 🚀 Getting Started

### Prerequisites
- .NET 6.0 SDK or later
- Visual Studio 2022 or VS Code

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/yourusername/BookReviewApp.git
   cd BookReviewApp
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Run database migrations**
   ```bash
   cd BookReviewApp.Web
   dotnet ef database update
   ```

4. **Start the application**
   ```bash
   dotnet run
   ```

5. **Access the application**
   - Open your browser and navigate to `https://localhost:7036`
   - The application will be ready to use!

## 📱 Screenshots

### Homepage
![Homepage](https://via.placeholder.com/800x400/667eea/ffffff?text=Beautiful+Homepage+with+Statistics)

### Books Library
![Books Library](https://via.placeholder.com/800x400/f5f7fa/333333?text=Modern+Books+Grid+with+Search)

### Dashboard
![Dashboard](https://via.placeholder.com/800x400/764ba2/ffffff?text=Analytics+Dashboard+with+Charts)

### Author Management
![Authors](https://via.placeholder.com/800x400/d4fc79/333333?text=Author+Profiles+with+Photos)

## 🎨 Key Features Demonstrated

### 💻 Technical Excellence
- **Clean Architecture**: Proper separation of concerns
- **Async/Await**: Non-blocking operations throughout
- **Error Handling**: Comprehensive exception handling
- **Validation**: Client and server-side validation
- **Security**: Anti-forgery tokens and input sanitization

### 🎯 User Experience
- **Intuitive Navigation**: Clear, logical user flow
- **Visual Feedback**: Loading states and success messages
- **Responsive Design**: Works perfectly on all devices
- **Accessibility**: Semantic HTML and ARIA labels
- **Performance**: Optimized queries and efficient rendering

### 📊 Data Management
- **CRUD Operations**: Complete Create, Read, Update, Delete functionality
- **Relationships**: Proper foreign key relationships
- **Search & Filter**: Real-time search with multiple criteria
- **Sorting**: Multiple sorting options for all entities
- **Pagination**: Efficient data loading (ready for implementation)

## 🔧 Customization

### Adding New Features
The application is designed for easy extension:

1. **New Entity**: Add domain model, repository, service, and controller
2. **New Views**: Create Razor views with Bootstrap styling
3. **New Features**: Extend existing functionality with minimal changes

### Styling
- **CSS Variables**: Easy color scheme customization
- **Bootstrap Classes**: Consistent design system
- **Custom Components**: Reusable UI components

## 📈 Performance Optimizations

- **Lazy Loading**: Efficient data loading with includes
- **Caching**: Ready for Redis implementation
- **Optimized Queries**: Proper use of Entity Framework
- **Minified Assets**: Production-ready asset optimization
- **CDN Integration**: External libraries via CDN

## 🧪 Testing

The application is designed with testability in mind:

- **Unit Testing**: Service layer is easily testable
- **Integration Testing**: Repository pattern enables testing
- **UI Testing**: Selenium-ready structure
- **Mock Data**: Seeded data for development and testing

## 🚀 Deployment

### Local Development
```bash
dotnet run --environment Development
```

### Production
```bash
dotnet publish -c Release
dotnet run --environment Production
```

### Docker Support
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:6.0
COPY bin/Release/net6.0/publish/ App/
WORKDIR /App
ENTRYPOINT ["dotnet", "BookReviewApp.Web.dll"]
```

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 👩‍💻 Author & Branding

**Nicolette Mashaba**  
Full Stack Developer | Polokwane, South Africa

- Portfolio: [nickimash.vercel.app](https://nickimash.vercel.app/)
- LinkedIn: [in/nicolette-mashaba-b094a5221](https://www.linkedin.com/in/nicolette-mashaba-b094a5221)
- Email: nene171408@gmail.com
- GitHub: [@nickimash](https://github.com/nickimash)

> All code, UI/UX, and design are original and proudly authored by Nicolette Mashaba (nickimash). Do not copy or redistribute without permission.

## 🙏 Acknowledgments

- Bootstrap team for the amazing UI framework
- Microsoft for ASP.NET Core
- Entity Framework team for the ORM
- Bootstrap Icons for the beautiful icon set

---

⭐ **Star this repository if you found it helpful!**

This project demonstrates modern web development practices, clean architecture, and professional UI/UX design. Perfect for showcasing full-stack development skills to potential employers.

## Database Setup

The official database initialization script for this project is [`setup_bookreviewdb.sql`](./setup_bookreviewdb.sql). This script:
- Drops and recreates all tables in the correct order
- Uses modern SQL Server syntax (`DROP TABLE IF EXISTS`, explicit foreign key constraints)
- Supports Unicode data for international author/book names
- Populates the database with sample data and provides useful queries

The previous script, `BookReviewApp.sql`, has been archived for reference. **All new development and deployments should use `setup_bookreviewdb.sql`.**

**Best Practices:**
- Keep schema and data scripts clear, well-commented, and versioned
- Use Unicode (`N'...'`) for all string data
- Prefer explicit constraint names for maintainability
- Archive old scripts for reference, but use a single canonical setup script for production

## Switching Between SQLite and SQL Server

By default, the app uses SQLite for easy local development. To use SQL Server (e.g., for production or advanced features):

1. **Start SQL Server in Docker:**
   ```bash
   docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=YourStrong!Passw0rd' -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest
   ```
2. **Update the connection string in `BookReviewApp.Web/appsettings.json`:**
   Replace the existing line with:
   ```json
   "DefaultConnection": "Server=localhost,1433;Database=BookReviewDB;User Id=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=True;"
   ```
3. **Run EF Core migrations:**
   ```bash
   dotnet ef database update
   ```

**Tip:**
- Use EF Core migrations to keep your schema in sync with your models.
- Use the seeding logic in `SeedData.cs` to populate your database with sample data for development and testing.
- For production, review and update your connection string and security settings as needed.

---

## 🌟 What's Unique?

- **Creative Trademark Badge:** Fixed, stylish badge with personal links (LinkedIn, portfolio, email) on every page.
- **Beautiful Privacy Page:** Modern, animated, and creative privacy policy with a quote, shield icon, and developer info.
- **Image Placeholders:** All book/author images use placeholders to prevent flickering or broken images (notably for 'American Gods' and 'Harry Potter and the Prisoner of Azkaban').
- **Authorship Assertion:** All major files include clear comments asserting original work by Nicolette Mashaba (nickimash).
- **UI/UX Polish:** Modern gradients, smooth animations, and a mobile-first, accessible design.

Developed with ❤️ by Nicolette Mashaba
