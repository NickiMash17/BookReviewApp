# 🚀 Azure Deployment Guide for BookReviewApp (FREE Student Tier)

## 🎓 **What You Get FREE with Student Account:**
- **$100 Azure credits** (12 months)
- **App Service F1 tier** (FREE hosting)
- **Azure SQL Database Basic** (FREE tier)
- **GitHub Actions** (FREE CI/CD)

## 📋 **Prerequisites:**

### 1. **Azure Student Account Setup**
```bash
# Go to: https://azure.microsoft.com/en-us/free/students/
# Sign in with your student email
# Verify $100 credits are available
```

### 2. **Install Azure CLI**
```bash
# Install Azure CLI
curl -sL https://aka.ms/InstallAzureCLIDeb | sudo bash

# Verify installation
az --version
```

### 3. **Login to Azure**
```bash
# Login to your Azure account
az login

# Set your subscription (if you have multiple)
az account set --subscription "Your Subscription Name"
```

## 🚀 **Deployment Steps:**

### **Option 1: Automated Deployment (Recommended)**
```bash
# Just run this one command:
./deploy-to-azure.sh
```

### **Option 2: Manual Deployment**

#### **Step 1: Create Resource Group**
```bash
az group create --name BookReviewApp-RG --location eastus
```

#### **Step 2: Deploy Resources**
```bash
az deployment group create \
    --resource-group BookReviewApp-RG \
    --template-file azure-template.json \
    --parameters appName=bookreviewapp-$(date +%s)
```

#### **Step 3: Get Your App URL**
```bash
az webapp show --name YOUR_APP_NAME --resource-group BookReviewApp-RG --query "defaultHostName" -o tsv
```

## 🔧 **Post-Deployment Setup:**

### **1. Configure GitHub Actions (Optional but Recommended)**

#### **Get Publish Profile:**
```bash
az webapp deployment list-publishing-profiles \
    --name YOUR_APP_NAME \
    --resource-group BookReviewApp-RG \
    --xml > publish-profile.xml
```

#### **Add to GitHub Secrets:**
1. Go to your GitHub repository
2. Settings → Secrets and variables → Actions
3. Add new secret: `AZURE_WEBAPP_PUBLISH_PROFILE`
4. Copy content from `publish-profile.xml`

**✅ PUBLISH PROFILE ADDED TO GITHUB SECRETS - READY FOR AUTOMATIC DEPLOYMENT!**

### **2. Update App Settings (Optional)**
```bash
# Set environment variables
az webapp config appsettings set \
    --name YOUR_APP_NAME \
    --resource-group BookReviewApp-RG \
    --settings DOTNET_ENVIRONMENT=Production
```

## 🌐 **Your Live URL:**
Your app will be available at:
```
https://YOUR_APP_NAME.azurewebsites.net
```

## 📱 **Azure Portal Access:**
- **Portal**: https://portal.azure.com
- **Resource Group**: BookReviewApp-RG
- **App Service**: Your app name
- **Monitor**: View logs, performance, and costs

## 💰 **Cost Management (FREE Tier):**
- **F1 App Service Plan**: FREE
- **Resource Group**: FREE
- **Total Cost**: $0/month
- **Limitations**: 
  - 1 GB RAM
  - 60 minutes/day CPU time
  - Shared infrastructure

## 🔍 **Troubleshooting:**

### **Common Issues:**

#### **1. App Won't Start**
```bash
# Check logs
az webapp log tail --name YOUR_APP_NAME --resource-group BookReviewApp-RG

# Check app settings
az webapp config appsettings list --name YOUR_APP_NAME --resource-group BookReviewApp-RG
```

#### **2. Database Connection Issues**
- Ensure `UseMongoDB: false` in production
- SQLite database will be created automatically

#### **3. Build Failures**
- Check GitHub Actions logs
- Verify .NET 8.0 compatibility

## 🎯 **Next Steps After Deployment:**

### **1. Test Your Live App**
- Visit your Azure URL
- Test all functionality
- Check mobile responsiveness

### **2. Set Up Custom Domain (Optional)**
```bash
# Add custom domain
az webapp config hostname add \
    --webapp-name YOUR_APP_NAME \
    --resource-group BookReviewApp-RG \
    --hostname yourdomain.com
```

### **3. Monitor Performance**
- Set up Azure Application Insights
- Monitor response times
- Track user analytics

## 🆘 **Need Help?**

### **Azure Support:**
- **Student Support**: Available with your account
- **Documentation**: https://docs.microsoft.com/azure
- **Community**: https://stackoverflow.com/questions/tagged/azure

### **Local Testing:**
```bash
# Test locally before deploying
dotnet run --environment Production
```

## 🎉 **Congratulations!**
Your BookReviewApp repository is now ready for automatic Azure deployment!

**Remember**: Your student account gives you $100 in credits, so you can experiment with other Azure services too! 