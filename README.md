# ProductStockManager

A modular inventory management system built with ASP.NET Core Web API and MVC, designed to manage product data and stock levels. 
This project demonstrates RESTful API design, MVC Razor frontend integration, layered architecture, and testable code.

---

##Features

- Full CRUD operations on products
- Auto-generated unique 6-digit Product IDs
- RESTful API built with ASP.NET Core
- MVC frontend using Razor views and HttpClient
- Stock increment/decrement endpoints
- EF Core with Code-First migrations
- Modular architecture with Core, Infrastructure, API, MVC, and Tests
- CORS and API-MVC client communication setup

---

##Tech Stack

- .NET 9
- ASP.NET Core Web API
- ASP.NET Core MVC
- Entity Framework Core (SQL Server)
- In-Memory Unit Testing (EF Core)
- NUnit
- Swagger (OpenAPI)

---

##Project Structure


ProductStockManager/
├── ProductStockManager.sln
├── ProductStockManager.API/          # REST API project
│   ├── Controllers/
│   ├── Program.cs
│   ├── appsettings.json
├── ProductStockManager.MVC/          # MVC UI project
│   ├── Controllers/
│   ├── Views/
│   ├── Program.cs
├── ProductStockManager.Core/         # Models, DTOs, Interfaces
├── ProductStockManager.Infrastructure/ # EF Core DbContext and Repositories
├── ProductStockManager.Tests/        # Unit tests using NUnit and EF In-Memory

##Prerequisites
SQL Server LocalDB (or any SQL Server)
VS Code

##Clone the repository
git clone https://github.com/DishaPrateechee/ProductStockManager.git
cd ProductStockManager

##Restore packages and build the solution
dotnet restore
dotnet build

##Apply EF Core migrations
cd ProductStockManager.API
dotnet ef migrations add InitialCreate
dotnet ef database update

##Run both projects
one terminal
cd ProductStockManager.API
dotnet run

Other terminal
cd ProductStockManager.MVC
dotnet run

##Running Tests
From root or test project folder:
dotnet test ProductStockManager.Tests

##Notes
-The frontend assumes the API runs on https://localhost:5001, adjust in Program.cs or launchSettings.json if needed.
-CORS is configured in the API project to allow calls from the MVC app at https://localhost:5002.



