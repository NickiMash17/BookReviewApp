# 📚 Book Review App

<div align="center">

![Original Work](https://img.shields.io/badge/Handcrafted%20by-Nicolette%20Mashaba-blueviolet?style=for-the-badge)
![.NET](https://img.shields.io/badge/.NET-6.0-blue?style=for-the-badge&logo=.net)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-6.0-purple?style=for-the-badge&logo=aspnet)
![MongoDB](https://img.shields.io/badge/MongoDB-5.0-green?style=for-the-badge&logo=mongodb)
![Bootstrap](https://img.shields.io/badge/Bootstrap-5.0-blue?style=for-the-badge&logo=bootstrap)
![License](https://img.shields.io/badge/License-MIT-yellow?style=for-the-badge)

</div>

> **🎨 Original work by Nicolette Mashaba (nickimash)**  
> *All UI/UX, code, and design are handcrafted and proudly owned.*

## 🌟 About

A sophisticated, full-stack web application for managing books, authors, and reviews. Built with modern .NET technologies and featuring MongoDB integration for flexible data storage. The application supports both local MongoDB and MongoDB Atlas cloud deployment, showcasing professional development practices and clean architecture principles.

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
- **🗄️ Dual Database Support**: MongoDB Atlas (cloud) and local MongoDB (Compass)
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
├── 🗄️ BookReviewApp.Data/           # Data access, repositories, and MongoDB context
├── ⚙️ BookReviewApp.Services/        # Business logic and services
├── 🌐 BookReviewApp.Web/            # Web application and controllers
└── 🧪 BookReviewApp.Tests/          # Unit and integration tests
```

### 🔧 Technology Stack

**Backend**
- ASP.NET Core 6.0
- MongoDB.Driver (latest)
- Repository Pattern with MongoDB support
- Dependency Injection
- Clean Architecture principles

**Database**
- MongoDB (local and Atlas support)
- MongoDB Compass (GUI tool)
- Flexible schema design
- BSON document storage

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
- MongoDB best practices

---

## 🚀 Quick Start

### Prerequisites
- [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) or later
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)
- [MongoDB Community Server](https://www.mongodb.com/try/download/community) (for local development)
- [MongoDB Compass](https://www.mongodb.com/try/download/compass) (GUI tool)

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

3. **Setup MongoDB**
   - **Local Setup**: Install MongoDB Community Server and start the service
   - **Atlas Setup**: Follow the [MongoDB Setup Guide](MONGODB_SETUP.md) for cloud configuration

4. **Configure database connection**
   - Edit `BookReviewApp.Web/appsettings.json`
   - Set `UseAtlas: false` for local MongoDB
   - Set `UseAtlas: true` for MongoDB Atlas

5. **Run the application**
   ```bash
   cd BookReviewApp.Web
   dotnet run
   ```

6. **Open in browser**
   ```
   🌐 https://localhost:7036
   ```

### 🗄️ Database Configuration

**Local MongoDB (Default)**
```json
{
  "MongoDbSettings": {
    "UseAtlas": false,
    "LocalConnectionString": "mongodb://localhost:27017/BookReviewApp",
    "DatabaseName": "BookReviewApp"
  }
}
```

**MongoDB Atlas (Cloud)**
```json
{
  "MongoDbSettings": {
    "UseAtlas": true,
    "ConnectionString": "mongodb+srv://username:password@cluster.mongodb.net/BookReviewApp",
    "DatabaseName": "BookReviewApp"
  }
}
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
- **MongoDB Integration**: Flexible NoSQL database with dual deployment options
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
- **Flexible Schema**: MongoDB document-based storage for easy schema evolution
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

**Development (Local MongoDB)**
```bash
dotnet run --environment Development
```

**Production (MongoDB Atlas)**
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

### MongoDB Configuration
- **Development**: Local MongoDB (Compass)
- **Production**: MongoDB Atlas (cloud)
- **Flexible Schema**: Document-based storage
- **Collections**: Books, Authors, Reviews, Users, Categories

### Setup Documentation
- **Primary**: [MongoDB Setup Guide](MONGODB_SETUP.md) - Complete setup instructions
- **Configuration**: `appsettings.json` - Database connection settings

### Migration Commands
```bash
# Seed data (if needed)
dotnet run --project BookReviewApp.Web --seed

# Test database connection
dotnet run --project BookReviewApp.Web --test-db
```

---

## 📚 Additional Resources

- [MongoDB Documentation](https://docs.mongodb.com/)
- [MongoDB Atlas Documentation](https://docs.atlas.mongodb.com/)
- [MongoDB .NET Driver](https://docs.mongodb.com/drivers/csharp/)
- [ASP.NET Core with MongoDB](https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mongo-app)

---

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

---

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

## 👤 Author

**Nicolette Mashaba (nickimash)**
- GitHub: [@nickimash](https://github.com/nickimash)
- LinkedIn: [Nicolette Mashaba](https://www.linkedin.com/in/nicolette-mashaba/)

---

<div align="center">

**Made with ❤️ by Nicolette Mashaba**

</div>