# MongoDB Setup Guide for BookReviewApp

This guide will help you set up both local MongoDB and MongoDB Atlas for your BookReviewApp presentation.

## 🏠 Local MongoDB Setup

### Step 1: Install MongoDB Community Server

**Option A: Using winget (Windows)**
```bash
winget install MongoDB.Server
```

**Option B: Manual Installation**
1. Go to [MongoDB Download Center](https://www.mongodb.com/try/download/community)
2. Download MongoDB Community Server for Windows
3. Run the installer and follow the setup wizard
4. Install MongoDB Compass (GUI tool) when prompted

### Step 2: Start MongoDB Service

**Windows Service (Automatic)**
- MongoDB should start automatically as a Windows service
- Check Services app: `mongod` service should be running

**Manual Start (if needed)**
```bash
# Start MongoDB service
net start MongoDB

# Or start manually
"C:\Program Files\MongoDB\Server\8.0\bin\mongod.exe" --dbpath="C:\data\db"
```

### Step 3: Test Local Connection

1. **Start your application**
2. **Navigate to**: `http://localhost:5000/debug/mongodb/test-local`
3. **Expected result**: ✅ MongoDB Connected Successfully!

## ☁️ MongoDB Atlas Setup

### Step 1: Create Atlas Account

1. Go to [MongoDB Atlas](https://cloud.mongodb.com/)
2. Click "Try Free" or "Sign Up"
3. Create account (no credit card required)

### Step 2: Create Cluster

1. **Choose Cloud Provider**: AWS (recommended)
2. **Choose Region**: Select closest to your location
3. **Cluster Tier**: M0 Free (shared, 512MB RAM)
4. **Cluster Name**: `BookReviewApp-Cluster`
5. Click "Create"

### Step 3: Set Up Database Access

1. **Go to Database Access** (left sidebar)
2. **Click "Add New Database User"**
3. **Configure User**:
   - Username: `bookreviewapp`
   - Password: `YourSecurePassword123!`
   - Role: `Read and write to any database`
4. **Click "Add User"**

### Step 4: Set Up Network Access

1. **Go to Network Access** (left sidebar)
2. **Click "Add IP Address"**
3. **Choose**: "Allow Access from Anywhere" (for demo)
4. **Click "Confirm"**

### Step 5: Get Connection String

1. **Go to Clusters** (left sidebar)
2. **Click "Connect"** on your cluster
3. **Choose "Connect your application"**
4. **Copy the connection string**

### Step 6: Update Configuration

1. **Open** `appsettings.Atlas.json`
2. **Replace** the connection string with your actual Atlas connection string
3. **Update** username and password in the connection string

### Step 7: Test Atlas Connection

1. **Add Atlas connection string to appsettings.json**:
```json
{
  "AtlasConnectionString": "mongodb+srv://bookreviewapp:YourPassword@cluster0.xxxxx.mongodb.net/?retryWrites=true&w=majority"
}
```

2. **Navigate to**: `http://localhost:5000/debug/mongodb/test-atlas`
3. **Expected result**: ✅ MongoDB Connected Successfully!

## 🔧 Application Configuration

### Switch Between Local and Atlas

**For Local MongoDB:**
```json
{
  "MongoDbSettings": {
    "ConnectionString": "mongodb://localhost:27017",
    "DatabaseName": "BookReviewApp"
  }
}
```

**For MongoDB Atlas:**
```json
{
  "MongoDbSettings": {
    "ConnectionString": "mongodb+srv://bookreviewapp:YourPassword@cluster0.xxxxx.mongodb.net/?retryWrites=true&w=majority",
    "DatabaseName": "BookReviewApp"
  }
}
```

## 🧪 Testing Your Setup

### Test Endpoints

1. **MongoDB Status**: `http://localhost:5000/debug/mongodb`
2. **Local Connection**: `http://localhost:5000/debug/mongodb/test-local`
3. **Atlas Connection**: `http://localhost:5000/debug/mongodb/test-atlas`

### Expected Results

**Successful Connection:**
```
✅ MongoDB Connected Successfully!
Database: BookReviewApp
Connection: mongodb://localhost:27017
Collections: 0
```

**Failed Connection:**
```
❌ MongoDB Connection Failed!
Error: Connection timeout
Connection: mongodb://localhost:27017
```

## 🎯 Presentation Demo Points

### 1. Show Local MongoDB
- Open MongoDB Compass
- Show local database and collections
- Demonstrate data operations

### 2. Show MongoDB Atlas
- Open Atlas dashboard
- Show cluster status
- Show database collections
- Demonstrate cloud benefits

### 3. Show Application Integration
- Run the application
- Show debug endpoints
- Demonstrate CRUD operations
- Show data persistence

### 4. Show Code Changes
- Domain models with BSON attributes
- MongoDB repositories
- Configuration files
- Service layer updates

## 🚨 Troubleshooting

### Local MongoDB Issues

**Service not starting:**
```bash
# Check if MongoDB service exists
sc query MongoDB

# Start service manually
net start MongoDB
```

**Port already in use:**
```bash
# Check what's using port 27017
netstat -ano | findstr :27017

# Kill process if needed
taskkill /PID <process_id> /F
```

### Atlas Issues

**Connection timeout:**
- Check Network Access settings
- Verify IP address is allowed
- Check firewall settings

**Authentication failed:**
- Verify username and password
- Check database user permissions
- Ensure connection string is correct

## 📚 Additional Resources

- [MongoDB Documentation](https://docs.mongodb.com/)
- [MongoDB Atlas Documentation](https://docs.atlas.mongodb.com/)
- [MongoDB .NET Driver](https://docs.mongodb.com/drivers/csharp/)
- [ASP.NET Core with MongoDB](https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mongo-app)

## 🎉 Success Checklist

- [ ] Local MongoDB installed and running
- [ ] MongoDB Atlas account created
- [ ] Atlas cluster configured
- [ ] Database user created
- [ ] Network access configured
- [ ] Connection strings updated
- [ ] Application builds successfully
- [ ] Local connection test passes
- [ ] Atlas connection test passes
- [ ] Data operations working
- [ ] Presentation demo ready

---

**Good luck with your presentation! 🚀** 