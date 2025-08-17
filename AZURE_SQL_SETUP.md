# Azure SQL Database Setup Guide for BookReviewApp

## 🎯 **Why Azure SQL Database?**

Our SQLite approach failed because Azure App Service has a read-only file system that prevents database creation. Azure SQL Database provides:

- ✅ **Persistent Storage** - Data survives deployments
- ✅ **Professional Grade** - Production-ready database
- ✅ **Student Free Tier** - Azure offers free credits
- ✅ **Reliable** - No file system restrictions
- ✅ **Scalable** - Grows with your application

## 🚀 **Step 1: Create Azure SQL Database**

### Using Azure Portal:

1. **Go to Azure Portal**: https://portal.azure.com
2. **Create Resource** → Search for "SQL Database"
3. **Click "Create"**

### Database Configuration:

- **Resource Group**: `BookReviewApp-rg` (use existing)
- **Database name**: `BookReviewAppDB`
- **Server**: Create new server
  - **Server name**: `bookreviewapp-sql-1755367448` (unique name)
  - **Location**: `East US` (same as your web app)
  - **Authentication method**: `SQL authentication`
  - **Server admin login**: `bookreviewadmin`
  - **Password**: `BookReviewApp2025!` (strong password)
- **Compute + storage**: 
  - **Service tier**: `Basic` (cheapest, good for development)
  - **Compute tier**: `Serverless`
  - **Min vCores**: `0.5`
  - **Max vCores**: `1`
  - **Auto-pause delay**: `1 hour` (saves money)

### Networking:

- **Connectivity method**: `Public endpoint`
- **Firewall rules**: 
  - ✅ **Allow Azure services and resources to access this server**
  - ✅ **Add your current IP address**

## 🔧 **Step 2: Get Connection String**

1. **Go to your SQL Database**
2. **Settings** → **Connection strings**
3. **Copy the ADO.NET connection string**
4. **Replace placeholders**:
   ```
   Server=tcp:bookreviewapp-sql-1755367448.database.windows.net,1433;
   Database=BookReviewAppDB;
   User ID=bookreviewadmin;
   Password=BookReviewApp2025!;
   Encrypt=true;
   TrustServerCertificate=false;
   Connection Timeout=30;
   ```

## ⚙️ **Step 3: Configure Azure App Service**

### Set Environment Variables:

```bash
az webapp config appsettings set \
  --resource-group BookReviewApp-rg \
  --name bookreviewapp-1755367448 \
  --settings \
    "ConnectionStrings__DefaultConnection"="Server=tcp:bookreviewapp-sql-1755367448.database.windows.net,1433;Database=BookReviewAppDB;User ID=bookreviewadmin;Password=BookReviewApp2025!;Encrypt=true;TrustServerCertificate=false;Connection Timeout=30;" \
    "AZURE_SQL_SERVER"="bookreviewapp-sql-1755367448.database.windows.net" \
    "AZURE_SQL_DATABASE"="BookReviewAppDB" \
    "AZURE_SQL_USERID"="bookreviewadmin" \
    "AZURE_SQL_PASSWORD"="BookReviewApp2025!"
```

### Or use Azure Portal:

1. **Go to your Web App**
2. **Settings** → **Configuration**
3. **Application settings** → **New application setting**
4. **Add these settings**:
   - **Name**: `ConnectionStrings__DefaultConnection`
   - **Value**: Your connection string from Step 2

## 🗄️ **Step 4: Create Database Schema**

The application will automatically create the database schema on first run using Entity Framework migrations.

## 💰 **Cost Estimation (Student Account)**

- **Basic Tier SQL Database**: ~$5-10/month
- **Serverless**: Pauses when not in use (saves money)
- **Student Benefits**: 
  - Free Azure credits ($100-200)
  - Discounted rates
  - Perfect for learning and projects

## 🧪 **Step 5: Test the Setup**

1. **Deploy the updated code**
2. **Check Azure App Service logs**
3. **Test user registration**
4. **Verify database creation**

## 🔍 **Troubleshooting**

### Common Issues:

1. **Firewall Rules**: Ensure your IP is allowed
2. **Connection String**: Check format and credentials
3. **Server Name**: Must be globally unique
4. **Authentication**: Use SQL authentication, not Azure AD

### Check Logs:

```bash
az webapp log tail --resource-group BookReviewApp-rg --name bookreviewapp-1755367448
```

## 📚 **Next Steps**

1. **Create the SQL Database** using the steps above
2. **Update the connection string** in Azure App Service
3. **Deploy the updated code** with SQL Server support
4. **Test user registration** - it should work now!

## 🎉 **Benefits After Migration**

- ✅ **User registration works**
- ✅ **User login works**
- ✅ **Data persists between deployments**
- ✅ **Professional database solution**
- ✅ **Scalable for future growth** 