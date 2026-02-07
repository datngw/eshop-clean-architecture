# 🛒 EShop Clean Architecture

A practice-focused e-commerce API built with **.NET 8** following **Clean Architecture** principles. This project is for learning and showcasing best practices for building scalable, maintainable, and testable applications.

## 📋 Overview

This is a complete e-commerce backend API featuring product catalog management, shopping basket functionality, order processing, and a comprehensive identity/authentication system. The project follows Domain-Driven Design (DDD) patterns with CQRS implemented via MediatR.

## ✨ Features

- **Product Management** - Full CRUD operations with pagination
- **Category Management** - Organize products by categories
- **Shopping Basket** - Add/remove items, clear basket, checkout
- **Order Processing** - Order creation and history tracking
- **User Authentication** - JWT-based authentication with refresh tokens
- **Role-Based Authorization** - Permission-based access control
- **User Management** - Registration, email confirmation, password reset
- **Email Notifications** - Email confirmation and password reset emails
- **Background Jobs** - Outbox pattern with Hangfire
- **API Versioning** - Versioned API endpoints (V1)
- **Real-time Communication** - SignalR support with optional Redis backplane
- **Distributed Caching** - Redis support for caching

## 🛠 Tech Stack

| Category | Technologies |
|----------|-------------|
| **Framework** | .NET 8, ASP.NET Core |
| **Architecture** | Clean Architecture, CQRS, DDD |
| **ORM** | Entity Framework Core 8, Dapper |
| **Database** | SQL Server |
| **Authentication** | ASP.NET Core Identity, JWT Bearer |
| **Validation** | FluentValidation |
| **Mapping** | Mapster |
| **Mediator** | MediatR 12 |
| **Background Jobs** | Hangfire |
| **Caching** | Redis (StackExchange.Redis) |
| **Email** | MailKit, MimeKit |
| **Logging** | Serilog (Console + File sinks) |
| **API Docs** | Swagger/OpenAPI (Swashbuckle) |
| **Testing** | xUnit, FluentAssertions, NetArchTest |
| **CI/CD** | GitHub Actions, Azure App Service |

## 📁 Solution Structure

```
CleanArchitecture.sln
├── src/
│   ├── Core/
│   │   ├── CleanArchitecture.Domain           # Entities, Value Objects, Domain Events
│   │   └── CleanArchitecture.Application      # Use Cases, Commands, Queries, Interfaces
│   │
│   ├── Infrastructure/
│   │   ├── CleanArchitecture.Persistence      # EF Core DbContext, Repositories
│   │   ├── CleanArchitecture.Identity         # ASP.NET Identity, JWT Auth
│   │   └── CleanArchitecture.Infrastructure   # Email, Caching, Background Jobs
│   │
│   └── API/
│       └── CleanArchitecture.Api              # Controllers, Middlewares, Configs
│
└── tests/
    ├── CleanArchitecture.Application.Tests.Unit   # Unit Tests
    └── CleanArchitecture.ArchitectureTests        # Architecture Tests
```

## 🏗 Architecture

```
┌─────────────────────────────────────────────────────────────────┐
│                         API Layer                                │
│              (Controllers, Middlewares, Swagger)                 │
├─────────────────────────────────────────────────────────────────┤
│                     Application Layer                            │
│        (Commands, Queries, Handlers, Validators, DTOs)          │
├─────────────────────────────────────────────────────────────────┤
│                       Domain Layer                               │
│     (Entities, Aggregates, Value Objects, Domain Events)        │
├─────────────────────────────────────────────────────────────────┤
│                   Infrastructure Layer                           │
│  ┌──────────────┐  ┌──────────────┐  ┌──────────────────────┐  │
│  │  Persistence │  │   Identity   │  │    Infrastructure    │  │
│  │  (EF Core)   │  │  (JWT Auth)  │  │ (Email, Cache, Jobs) │  │
│  └──────────────┘  └──────────────┘  └──────────────────────┘  │
└─────────────────────────────────────────────────────────────────┘
```

## 📋 Requirements

- **.NET 8 SDK** or later
- **SQL Server** (LocalDB, SQL Server, or Azure SQL)
- **Redis** (optional, for distributed caching and SignalR backplane)
- **Visual Studio 2022** / **VS Code** / **Rider**

## 🚀 Quick Start (Local)

### 1. Clone the repository

```bash
git clone <repository-url>
cd eshop-clean-architecture
```

### 2. Restore dependencies

```bash
dotnet restore
```

### 3. Update configuration

Update the connection string in `src/API/CleanArchitecture.Api/Configurations/database.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=localhost,1433;Initial Catalog=eshop;User id=sa;Password=YourPassword@;TrustServerCertificate=True"
  }
}
```

### 4. Build the solution

```bash
dotnet build
```

### 5. Run the API

```bash
dotnet run --project src/API/CleanArchitecture.Api
```

The API will be available at:
- **HTTPS**: `https://localhost:7047`
- **HTTP**: `http://localhost:5016`
- **Swagger UI**: `https://localhost:7047/swagger`

> **Note:** The application automatically applies migrations and seeds the database on startup.

## ⚙️ Configuration

Configuration files are located in `src/API/CleanArchitecture.Api/Configurations/`:

| File | Description |
|------|-------------|
| `database.json` | SQL Server connection string |
| `security.json` | JWT settings & admin credentials |
| `cache.json` | Redis caching configuration |
| `mail.json` | SMTP email settings |
| `hangfire.json` | Background job dashboard settings |
| `logger.json` | Serilog logging configuration |
| `signalr.json` | SignalR backplane settings |
| `outbox.json` | Outbox pattern settings |

## 🗄 Database

### Entity Framework Core Migrations

The application uses **EF Core** with **SQL Server**. Migrations are automatically applied on startup.

To manually create a new migration:

```bash
dotnet ef migrations add <MigrationName> --project src/Infrastructure/CleanArchitecture.Persistence --startup-project src/API/CleanArchitecture.Api
```

To update the database manually:

```bash
dotnet ef database update --project src/Infrastructure/CleanArchitecture.Persistence --startup-project src/API/CleanArchitecture.Api
```

### Database Seeding

The application seeds an admin user on startup with the following credentials (configured in `security.json`):

- **Username:** `admin`
- **Email:** (See `security.json`)
- **Password:** (See `security.json`)

> ⚠️ **Important:** Change these credentials for production environments!

## 🧪 Testing

The solution includes two test projects:

### Unit Tests

```bash
dotnet test tests/CleanArchitecture.Application.UnitTests
```

### Architecture Tests

Validates Clean Architecture dependency rules:

```bash
dotnet test tests/CleanArchitecture.ArchitectureTests
```

### Run All Tests

```bash
dotnet test
```

## 🐳 Docker

A Dockerfile is provided for containerization.

### Build Docker Image

```bash
docker build -t cleanarchitecture-api -f src/Presentations/BackEnds/CleanArchitecture.Api/Dockerfile .
```

### Run Container

```bash
docker run -d -p 8080:8080 -p 8081:8081 cleanarchitecture-api
```

**Exposed Ports:**
- `8080` - HTTP
- `8081` - HTTPS

## 🚀 Deployment

### CI/CD Pipeline

The project includes a GitHub Actions workflow (`.github/workflows/main.yml`) that:

1. Triggers on push to `main` branch or manual dispatch
2. Restores, builds, and publishes the application
3. Deploys to **Azure App Service** (`eshop-clean-architecture`)

**Required Secrets:**
- `API_PUBLISH_SECRET` - Azure Web App publish profile

### Manual Deployment

```bash
dotnet publish src/API/CleanArchitecture.Api -c Release -o ./publish
```

## 📊 Background Jobs

Hangfire is used for background job processing with the **Outbox Pattern**.

- **Dashboard URL:** `https://localhost:7047/jobs`
- **Dashboard Credentials:** (See `hangfire.json`)
  - User: `Admin`
  - Password: `Admin@Jobs`

## 🔧 Troubleshooting

### Database Connection Issues

1. Verify SQL Server is running
2. Check connection string in `database.json`
3. Ensure `TrustServerCertificate=True` for local development

### Redis Connection Issues

1. If not using Redis, set `UseDistributedCache` and `PreferRedis` to `false` in `cache.json`
2. If using Redis, ensure Redis is running on the configured port

### Email Sending Issues

1. Verify SMTP settings in `mail.json`
2. For Gmail, use an App Password instead of your account password
3. Ensure less secure app access or use OAuth2

### JWT Authentication Issues

1. Ensure the JWT key in `security.json` is at least 32 characters
2. Verify token expiration settings
3. Check the Authorization header format: `Bearer <token>`

---

Built with ❤️ using .NET 8 and Clean Architecture
