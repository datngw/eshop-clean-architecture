# 🛒 EShop Clean Architecture

A production-ready e-commerce API built with **.NET 8** following **Clean Architecture** principles. This solution demonstrates best practices for building scalable, maintainable, and testable enterprise applications.

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

### Environment Variables / Configuration Keys

| Key | Description | Default |
|-----|-------------|---------|
| `ConnectionStrings:DefaultConnection` | SQL Server connection string | See `database.json` |
| `SecuritySettings:JwtSettings:key` | JWT signing key | (See `security.json`) |
| `SecuritySettings:JwtSettings:tokenExpirationInMinutes` | Token expiry | `60` |
| `SecuritySettings:JwtSettings:refreshTokenExpirationInDays` | Refresh token expiry | `7` |
| `CacheSettings:UseDistributedCache` | Enable distributed cache | `false` |
| `CacheSettings:PreferRedis` | Use Redis for caching | `false` |
| `CacheSettings:RedisURL` | Redis connection URL | `localhost:6379` |
| `MailSettings:Host` | SMTP server host | `smtp.gmail.com` |
| `MailSettings:Port` | SMTP server port | `587` |
| `HangfireSettings:Route` | Hangfire dashboard route | `/jobs` |
| `SignalRSettings:UseBackplane` | Enable SignalR backplane | `false` |

### Logging Configuration

Serilog is configured to write logs to both console and rolling file:

```json
{
  "LoggerSettings": {
    "Serilog": {
      "MinimumLevel": { "Default": "Error" },
      "WriteTo": [
        { "Name": "Console" },
        { "Name": "File", "Args": { "path": "logs/log-.txt", "rollingInterval": "Day" } }
      ]
    }
  }
}
```

## 🔌 API Endpoints

**Base URL:** `https://localhost:7047/api`

### Authentication

| Method | Endpoint | Description | Auth |
|--------|----------|-------------|------|
| `POST` | `/Auth/login` | User login | ❌ |
| `POST` | `/Auth/refresh` | Refresh access token | ❌ |

### Users

| Method | Endpoint | Description | Auth |
|--------|----------|-------------|------|
| `GET` | `/Users` | Get all users | ✅ Permission |
| `GET` | `/Users/{id}` | Get user by ID | ✅ Permission |
| `POST` | `/Users` | Create user | ✅ Permission |
| `POST` | `/Users/self-register` | Self registration | ❌ |
| `GET` | `/Users/{id}/roles` | Get user roles | ✅ Permission |
| `POST` | `/Users/{id}/roles` | Assign roles | ✅ Permission |
| `POST` | `/Users/{id}/toggle-status` | Toggle user status | ✅ Permission |
| `GET` | `/Users/confirm-email` | Confirm email | ❌ |
| `POST` | `/Users/forgot-password` | Request password reset | ❌ |
| `POST` | `/Users/reset-password` | Reset password | ✅ |

### Roles

| Method | Endpoint | Description | Auth |
|--------|----------|-------------|------|
| `GET` | `/Roles` | Get all roles | ✅ Permission |
| `GET` | `/Roles/{id}` | Get role by ID | ✅ Permission |
| `GET` | `/Roles/{id}/permissions` | Get role permissions | ✅ Permission |
| `PUT` | `/Roles/{id}/permissions` | Update role permissions | ✅ Permission |
| `POST` | `/Roles` | Create role | ✅ Permission |
| `DELETE` | `/Roles/{id}` | Delete role | ✅ Permission |

### Products (V1)

| Method | Endpoint | Description | Auth |
|--------|----------|-------------|------|
| `POST` | `/api/v1/Product/paginated` | Get paginated products | ❌ |
| `GET` | `/api/v1/Product/{id}` | Get product by ID | ❌ |
| `POST` | `/api/v1/Product` | Create product | ✅ Permission |
| `PUT` | `/api/v1/Product` | Update product | ✅ Permission |
| `DELETE` | `/api/v1/Product/{id}` | Delete product | ✅ Permission |

### Categories (V1)

| Method | Endpoint | Description | Auth |
|--------|----------|-------------|------|
| `POST` | `/api/v1/Category/paginated` | Get paginated categories | ❌ |
| `GET` | `/api/v1/Category/{id}` | Get category by ID | ❌ |
| `POST` | `/api/v1/Category` | Create category | ✅ Permission |
| `PUT` | `/api/v1/Category/{id}` | Update category | ✅ Permission |
| `DELETE` | `/api/v1/Category/{id}` | Delete category | ✅ Permission |

### Baskets (V1)

| Method | Endpoint | Description | Auth |
|--------|----------|-------------|------|
| `GET` | `/api/v1/Basket` | Get user's basket | ✅ |
| `POST` | `/api/v1/Basket` | Add item to basket | ✅ |
| `DELETE` | `/api/v1/Basket/remove-product-item` | Remove item from basket | ✅ |
| `DELETE` | `/api/v1/Basket/clear-basket` | Clear basket | ✅ |
| `POST` | `/api/v1/Basket/check-out` | Checkout | ✅ |

### Orders (V1)

| Method | Endpoint | Description | Auth |
|--------|----------|-------------|------|
| `POST` | `/api/v1/Order` | Get orders by user ID | ✅ |
| `GET` | `/api/v1/Order` | Get order by ID | ✅ |

### Swagger / OpenAPI

Swagger UI is available at: `https://localhost:7047/swagger`

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
