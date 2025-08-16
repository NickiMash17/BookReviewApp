#!/bin/bash

echo "🚀 Deploying BookReviewApp to Azure (FREE Tier)..."

# Check if Azure CLI is installed
if ! command -v az &> /dev/null; then
    echo "❌ Azure CLI not found. Please install it first:"
    echo "   curl -sL https://aka.ms/InstallAzureCLIDeb | sudo bash"
    exit 1
fi

# Check if logged in to Azure
if ! az account show &> /dev/null; then
    echo "🔐 Please log in to Azure first:"
    echo "   az login"
    exit 1
fi

# Set variables
RESOURCE_GROUP="BookReviewApp-RG"
LOCATION="eastus"  # Free tier available
APP_NAME="bookreviewapp-$(date +%s)"  # Unique name
PLAN_NAME="${APP_NAME}-plan"

echo "📍 Location: $LOCATION"
echo "🏷️  App Name: $APP_NAME"
echo "📦 Resource Group: $RESOURCE_GROUP"

# Create resource group
echo "📁 Creating resource group..."
az group create --name $RESOURCE_GROUP --location $LOCATION

# Deploy using ARM template
echo "🚀 Deploying resources..."
az deployment group create \
    --resource-group $RESOURCE_GROUP \
    --template-file azure-template.json \
    --parameters appName=$APP_NAME location=$LOCATION

# Get the web app URL
WEBAPP_URL=$(az webapp show --name $APP_NAME --resource-group $RESOURCE_GROUP --query "defaultHostName" -o tsv)

echo "✅ Deployment complete!"
echo "🌐 Your app is available at: https://$WEBAPP_URL"
echo "📱 Azure Portal: https://portal.azure.com"
echo "💰 Cost: FREE (F1 tier)"

# Get publish profile for GitHub Actions
echo "📋 Getting publish profile for GitHub Actions..."
az webapp deployment list-publishing-profiles \
    --name $APP_NAME \
    --resource-group $RESOURCE_GROUP \
    --xml > publish-profile.xml

echo "📄 Publish profile saved to: publish-profile.xml"
echo "🔑 Add this to GitHub Secrets as: AZURE_WEBAPP_PUBLISH_PROFILE" 