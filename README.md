# 📚 Book Review App

<div align="center">

![Original Work](https://img.shields.io/badge/Handcrafted%20by-Nicolette%20Mashaba-blueviolet?style=for-the-badge)
![.NET](https://img.shields.io/badge/.NET-6.0-blue?style=for-the-badge&logo=.net)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-6.0-purple?style=for-the-badge&logo=aspnet)
![Entity Framework](https://img.shields.io/badge/Entity%20Framework-6.0-green?style=for-the-badge&logo=entity-framework)
![Bootstrap](https://img.shields.io/badge/Bootstrap-5.0-blue?style=for-the-badge&logo=bootstrap)
![License](https://img.shields.io/badge/License-MIT-yellow?style=for-the-badge)

</div>

> **🎨 Original work by Nicolette Mashaba (nickimash)**  
> *All UI/UX, code, and design are handcrafted and proudly owned.*

## 🌟 About

A sophisticated, full-stack web application for managing books, authors, and reviews. Built with modern .NET technologies and featuring a beautiful, responsive interface that showcases professional development practices and clean architecture principles.

**Perfect for demonstrating full-stack development skills to potential employers.**

---

## 🎯 Features

### ✨ Core Functionality
- **📖 Book Management**: Complete CRUD operations with cover image support
- **👨‍💼 Author Management**: Detailed author profiles with biographies and photos
- **⭐ Review System**: Interactive 5-star rating system with detailed comments
- **🔍 Smart Search**: Real-time search and filtering across all content
- **📱 Responsive Design**: Mobile-first design that works seamlessly on all devices

### 🚀 Advanced Features
- **📊 Analytics Dashboard**: Interactive charts and comprehensive statistics
- **⚡ Real-time Search**: Instant search results with advanced filtering
- **🎨 Visual Ratings**: Beautiful star rating displays with average calculations
- **🖼️ Image Management**: Robust image handling with automatic fallbacks
- **🔗 Data Relationships**: Sophisticated database relationships and optimized queries

### 🎨 UI/UX Excellence
- **Modern Aesthetics**: Clean, professional interface with smooth gradients and animations
- **Interactive Elements**: Engaging hover effects, transitions, and overlays
- **Consistent Iconography**: Bootstrap Icons throughout for visual consistency
- **Accessibility First**: WCAG compliant with semantic HTML and ARIA labels
- **Performance Optimized**: Fast loading with proper error handling and user feedback

---

## 🏗️ Architecture

### 📁 Project Structure
```
BookReviewApp/
├── 📦 BookReviewApp.Domain/          # Domain models and interfaces
├── 🗄️ BookReviewApp.Infrastructure/  # Data access and repositories
├── ⚙️ BookReviewApp.Services/        # Business logic and services
└── 🌐 BookReviewApp.Web/            # Web application and controllers
```

### 🔧 Technology Stack

**Backend**
- ASP.NET Core 6.0
- Entity Framework Core
- SQLite/SQL Server support
- Repository Pattern
- Dependency Injection

**Frontend**
- Bootstrap 5
- JavaScript ES6+
- Chart.js for analytics
- Bootstrap Icons
- CSS3 with custom properties

**Development**
- Clean Architecture
- SOLID Principles
- Async/Await patterns
- Comprehensive error handling

---

## 🚀 Quick Start

### Prerequisites
- [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) or later
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)
- Optional: [Docker Desktop](https://www.docker.com/products/docker-desktop) for SQL Server

### 🔧 Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/nickimash/BookReviewApp.git
   cd BookReviewApp
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Setup database**
   ```bash
   cd BookReviewApp.Web
   dotnet ef database update
   ```

4. **Run the application**
   ```bash
   dotnet run
   ```

5. **Open in browser**
   ```
   🌐 https://localhost:7036
   ```

### 🐳 Docker Setup (Optional - SQL Server)

For production-like database setup:

```bash
# Start SQL Server container
docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=YourStrong!Passw0rd' \
  -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest

# Update connection string in appsettings.json
# Run migrations
dotnet ef database update
```

---

## 📸 Application Preview

### 🏠 Dashboard Overview
*Modern analytics dashboard with interactive charts and key metrics*

### 📚 Books Library
*Elegant book grid with advanced search and filtering capabilities*

### 👥 Author Profiles
*Professional author management with rich profiles and photo support*

### ⭐ Review System
*Intuitive rating system with detailed feedback collection*

---

## 🎨 Key Highlights

### 💻 Technical Excellence
- **Clean Architecture**: Proper separation of concerns with layered approach
- **Async Operations**: Non-blocking operations throughout the application
- **Robust Error Handling**: Comprehensive exception handling and logging
- **Input Validation**: Both client-side and server-side validation
- **Security Best Practices**: Anti-forgery tokens and input sanitization

### 🎯 User Experience
- **Intuitive Navigation**: Logical user flow and clear information architecture
- **Visual Feedback**: Loading states, success messages, and error notifications
- **Mobile-First Design**: Optimized for all screen sizes and touch interactions
- **Fast Performance**: Optimized database queries and efficient rendering
- **Accessibility**: WCAG guidelines compliance with semantic markup

### 📊 Data Management
- **Complete CRUD**: Full Create, Read, Update, Delete operations
- **Smart Relationships**: Optimized foreign key relationships and includes
- **Advanced Search**: Multi-criteria search with real-time results
- **Flexible Sorting**: Multiple sorting options across all data
- **Scalable Design**: Ready for pagination and advanced filtering

---

## 🛠️ Development

### 🧪 Running Tests
```bash
# Unit tests
dotnet test BookReviewApp.Tests/

# Integration tests
dotnet test BookReviewApp.IntegrationTests/
```

### 🎨 Customization

**Adding New Features**
1. Create domain model in `Domain` layer
2. Add repository interface and implementation
3. Implement service layer logic
4. Create controller and views
5. Update navigation and UI

**Styling Customization**
```css
:root {
  --primary-color: #667eea;
  --secondary-color: #764ba2;
  --accent-color: #f093fb;
}
```

### 🚀 Deployment Options

**Development**
```bash
dotnet run --environment Development
```

**Production**
```bash
dotnet publish -c Release -o ./publish
dotnet ./publish/BookReviewApp.Web.dll
```

**Docker Deployment**
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:6.0
COPY publish/ App/
WORKDIR /App
EXPOSE 80
ENTRYPOINT ["dotnet", "BookReviewApp.Web.dll"]
```

---

## 🗄️ Database

### Default Configuration
- **Development**: SQLite (local file database)
- **Production**: SQL Server (configurable)

### Setup Scripts
- **Primary**: `setup_bookreviewdb.sql` - Official setup script
- **Archive**: `BookReviewApp.sql` - Legacy reference

### Migration Commands
```bash
# Add new migration
dotnet ef migrations add MigrationName

# Update database
dotnet ef database update

# Generate SQL script
dotnet ef migrations script
```

---

## 🤝 Contributing

We welcome contributions! Please follow these steps:

1. **Fork** the repository
2. **Create** a feature branch (`git checkout -b feature/AmazingFeature`)
3. **Commit** your changes (`git commit -m 'Add AmazingFeature'`)
4. **Push** to the branch (`git push origin feature/AmazingFeature`)
5. **Open** a Pull Request

### Code Standards
- Follow C# coding conventions
- Write comprehensive unit tests
- Update documentation for new features
- Ensure responsive design compliance

---

## 📄 License

This project is licensed under the **MIT License** - see the [LICENSE](LICENSE) file for details.

---

## 👩‍💻 Author

<div align="center">

### **Nicolette Mashaba**
*Full Stack Developer | Polokwane, South Africa*

[![Portfolio](https://img.shields.io/badge/Portfolio-Visit-blueviolet?style=for-the-badge&logo=vercel)](https://nickimash.vercel.app/)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-Connect-blue?style=for-the-badge&logo=linkedin)](https://www.linkedin.com/in/nicolette-mashaba-b094a5221)
[![Email](https://img.shields.io/badge/Email-Contact-red?style=for-the-badge&logo=gmail)](mailto:nene171408@gmail.com)
[![GitHub](https://img.shields.io/badge/GitHub-Follow-black?style=for-the-badge&logo=github)](https://github.com/nickimash)

> *All code, UI/UX, and design are original works by Nicolette Mashaba (nickimash)*  
> *Please respect intellectual property - do not copy or redistribute without permission*

</div>

---

## 🙏 Acknowledgments

- **Bootstrap Team** - For the exceptional UI framework
- **Microsoft** - For ASP.NET Core and Entity Framework
- **Bootstrap Icons** - For the beautiful icon library
- **Chart.js** - For powerful data visualization
- **Community** - For inspiration and best practices

---

<div align="center">

### ⭐ Star this repository if you found it helpful!

*This project demonstrates modern web development practices, clean architecture, and professional UI/UX design.*

**Perfect showcase for full-stack development skills**

---

**Built with ❤️ by Nicolette Mashaba**

</div>