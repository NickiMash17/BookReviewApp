   # Use the official .NET SDK image to build and publish the app
   FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
   WORKDIR /src
   COPY . .
   RUN dotnet publish BookReviewApp.Web/BookReviewApp.Web.csproj -c Release -o /app/publish

   # Use the official ASP.NET Core runtime image
   FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
   WORKDIR /app
   COPY --from=build /app/publish .
   ENV ASPNETCORE_URLS=http://+:10000
   EXPOSE 10000
   ENTRYPOINT [\"dotnet\", \"BookReviewApp.Web.dll\"]