# MongoDB Setup Guide for BookReviewApp

This comprehensive guide will help you set up MongoDB for the BookReviewApp, supporting both local MongoDB and MongoDB Atlas cloud deployment.

## 📋 Prerequisites

- [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) or later
- [MongoDB Community Server](https://www.mongodb.com/try/download/community) (for local development)
- [MongoDB Compass](https://www.mongodb.com/try/download/compass) (GUI tool for database management)
- Internet connection (for MongoDB Atlas setup)

## 🏠 Local MongoDB Setup

### Step 1: Install MongoDB Community Server

**Windows Installation**

**Option A: Using winget (Recommended)**
```bash
winget install MongoDB.Server
```

**Option B: Manual Installation**
1. Visit [MongoDB Download Center](https://www.mongodb.com/try/download/community)
2. Download MongoDB Community Server for Windows
3. Run the installer and follow the setup wizard
4. Install MongoDB Compass when prompted during installation

**macOS Installation**
```bash
# Using Homebrew
brew tap mongodb/brew
brew install mongodb-community

# Start MongoDB service
brew services start mongodb/brew/mongodb-community
```

**Linux Installation (Ubuntu)**
```bash
# Import MongoDB public GPG key
wget -qO - https://www.mongodb.org/static/pgp/server-6.0.asc | sudo apt-key add -

# Create list file for MongoDB
echo "deb [ arch=amd64,arm64 ] https://repo.mongodb.org/apt/ubuntu focal/mongodb-org/6.0 multiverse" | sudo tee /etc/apt/sources.list.d/mongodb-org-6.0.list

# Update package database
sudo apt-get update

# Install MongoDB
sudo apt-get install -y mongodb-org

# Start MongoDB
sudo systemctl start mongod
sudo systemctl enable mongod
```

### Step 2: Verify MongoDB Installation

**Check MongoDB Service Status**
```bash
# Windows
sc query MongoDB

# macOS/Linux
sudo systemctl status mongod
```

**Test MongoDB Connection**
```bash
# Connect to MongoDB shell
mongosh

# Or legacy mongo shell
mongo
```

**Expected Output:**
```
Current Mongosh Log ID: 64f8a2b3c4d5e6f7g8h9i0j1
Connecting to:          mongodb://127.0.0.1:27017/?directConnection=true&serverSelectionTimeoutMS=2000&appName=mongosh+2.0.1
Using MongoDB:          7.0.4
Using Mongosh:          2.0.1
```

### Step 3: Install MongoDB Compass

1. Download [MongoDB Compass](https://www.mongodb.com/try/download/compass)
2. Install and launch Compass
3. Connect to local MongoDB: `mongodb://localhost:27017`
4. Create database: `BookReviewApp`

## ☁️ MongoDB Atlas Setup

### Step 1: Create Atlas Account

1. Visit [MongoDB Atlas](https://cloud.mongodb.com/)
2. Click "Try Free" or "Sign Up"
3. Create account (free tier available, no credit card required for basic setup)

### Step 2: Create Cluster

1. **Choose Cloud Provider**: AWS, Google Cloud, or Azure (AWS recommended)
2. **Choose Region**: Select closest to your location for better performance
3. **Cluster Tier**: M0 Free (shared, 512MB RAM) for development
4. **Cluster Name**: `BookReviewApp-Cluster`
5. Click "Create"

### Step 3: Configure Database Access

1. Navigate to **Database Access** in the left sidebar
2. Click **"Add New Database User"**
3. Configure user settings:
   - **Username**: `bookreviewapp`
   - **Password**: Generate a strong password (12+ characters, mixed case, numbers, symbols)
   - **Database User Privileges**: `Read and write to any database`
   - **Built-in Role**: `Atlas admin` (for development) or `Read and write to any database`
4. Click **"Add User"**

### Step 4: Configure Network Access

1. Navigate to **Network Access** in the left sidebar
2. Click **"Add IP Address"**
3. Choose access method:
   - **Development**: "Allow Access from Anywhere" (`0.0.0.0/0`)
   - **Production**: Add specific IP addresses or ranges
4. Click **"Confirm"**

### Step 5: Get Connection String

1. Navigate to **Clusters** in the left sidebar
2. Click **"Connect"** on your cluster
3. Choose **"Connect your application"**
4. Select **.NET** as your driver
5. Copy the connection string

**Connection String Format:**
```
mongodb+srv://bookreviewapp:<password>@cluster0.xxxxx.mongodb.net/?retryWrites=true&w=majority
```

## 🔧 Application Configuration

### Update appsettings.json

**For Local MongoDB:**
```json
{
  "MongoDbSettings": {
    "UseAtlas": false,
    "LocalConnectionString": "mongodb://localhost:27017/BookReviewApp",
    "DatabaseName": "BookReviewApp"
  }
}
```

**For MongoDB Atlas:**
```json
{
  "MongoDbSettings": {
    "UseAtlas": true,
    "ConnectionString": "mongodb+srv://bookreviewapp:YourPassword@cluster0.xxxxx.mongodb.net/BookReviewApp?retryWrites=true&w=majority",
    "DatabaseName": "BookReviewApp"
  }
}
```

### Environment-Specific Configuration

Create environment-specific configuration files:

**appsettings.Development.json (Local MongoDB)**
```json
{
  "MongoDbSettings": {
    "UseAtlas": false,
    "LocalConnectionString": "mongodb://localhost:27017/BookReviewApp",
    "DatabaseName": "BookReviewApp"
  }
}
```

**appsettings.Production.json (MongoDB Atlas)**
```json
{
  "MongoDbSettings": {
    "UseAtlas": true,
    "ConnectionString": "mongodb+srv://bookreviewapp:YourPassword@cluster0.xxxxx.mongodb.net/BookReviewApp?retryWrites=true&w=majority",
    "DatabaseName": "BookReviewApp"
  }
}
```

## 🧪 Testing Your Setup

### Test Database Connection

**Run the application:**
```bash
cd BookReviewApp.Web
dotnet run
```

**Test endpoints (if available):**
- Local MongoDB: `http://localhost:5000/debug/mongodb/test-local`
- Atlas MongoDB: `http://localhost:5000/debug/mongodb/test-atlas`

### Verify with MongoDB Compass

1. **Local MongoDB**: Connect to `mongodb://localhost:27017`
2. **MongoDB Atlas**: Use the connection string from Atlas dashboard
3. Verify database `BookReviewApp` exists
4. Check collections: Books, Authors, Reviews, Users, Categories

### Manual Connection Test

**Using MongoDB Shell:**
```bash
# Local MongoDB
mongosh "mongodb://localhost:27017/BookReviewApp"

# MongoDB Atlas
mongosh "mongodb+srv://bookreviewapp:YourPassword@cluster0.xxxxx.mongodb.net/BookReviewApp"
```

**Test Commands:**
```javascript
// Show databases
show dbs

// Use BookReviewApp database
use BookReviewApp

// Show collections
show collections

// Test query
db.books.find().limit(1)
```

## 🚨 Troubleshooting

### Local MongoDB Issues

**Service Not Starting:**
```bash
# Windows - Check service status
sc query MongoDB

# Windows - Start service manually
net start MongoDB

# macOS/Linux - Check service status
sudo systemctl status mongod

# macOS/Linux - Start service manually
sudo systemctl start mongod
```

**Port Already in Use:**
```bash
# Check what's using port 27017
netstat -ano | findstr :27017  # Windows
lsof -i :27017                 # macOS/Linux

# Kill process if needed
taskkill /PID <process_id> /F  # Windows
kill -9 <process_id>           # macOS/Linux
```

**Permission Issues:**
```bash
# Windows - Run as Administrator
# macOS/Linux - Check file permissions
sudo chown -R mongodb:mongodb /var/lib/mongodb
sudo chmod 755 /var/lib/mongodb
```

### Atlas Issues

**Connection Timeout:**
- Verify Network Access settings allow your IP
- Check firewall settings
- Ensure connection string is correct
- Try connecting from different network

**Authentication Failed:**
- Verify username and password
- Check database user permissions
- Ensure password is URL-encoded in connection string
- Reset database user password if needed

**TLS/SSL Issues:**
- Update MongoDB.Driver package to latest version
- Check .NET version compatibility
- Verify TLS 1.2 is enabled in application

### Application Issues

**Build Errors:**
```bash
# Clean and rebuild
dotnet clean
dotnet restore
dotnet build

# Clear NuGet cache
dotnet nuget locals all --clear
```

**Runtime Errors:**
- Check application logs
- Verify connection strings
- Ensure MongoDB service is running
- Check firewall settings

## 📊 Database Collections

The application uses the following MongoDB collections:

- **Books**: Book information, titles, descriptions, cover images
- **Authors**: Author details, biographies, photos
- **Reviews**: User reviews, ratings, comments
- **Users**: User accounts, profiles, authentication
- **Categories**: Book categories and genres
- **BookCategories**: Many-to-many relationship between books and categories

## 🔒 Security Best Practices

### Local Development
- Use strong passwords for database users
- Limit network access to localhost only
- Keep MongoDB updated to latest version
- Use MongoDB Compass for secure database management

### Production (Atlas)
- Use strong, unique passwords
- Enable IP whitelisting for production
- Use VPC peering for enhanced security
- Enable MongoDB Atlas security features
- Regular security audits and updates

## 📚 Additional Resources

- [MongoDB Documentation](https://docs.mongodb.com/)
- [MongoDB Atlas Documentation](https://docs.atlas.mongodb.com/)
- [MongoDB .NET Driver](https://docs.mongodb.com/drivers/csharp/)
- [ASP.NET Core with MongoDB](https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mongo-app)
- [MongoDB Compass Documentation](https://docs.mongodb.com/compass/)

## ✅ Setup Checklist

- [ ] MongoDB Community Server installed
- [ ] MongoDB service running
- [ ] MongoDB Compass installed and connected
- [ ] Atlas account created (optional)
- [ ] Atlas cluster configured (optional)
- [ ] Database user created
- [ ] Network access configured
- [ ] Connection strings updated in appsettings.json
- [ ] Application builds successfully
- [ ] Database connection test passes
- [ ] Collections created and accessible
- [ ] Sample data loaded (if applicable)

---

**Need help?** Check the troubleshooting section above or refer to the MongoDB documentation for detailed guidance. 