# News API

.NET Core Web API for managing news articles, users, comments, favorites, and trending content.

## Features

### Authentication & Authorization
* JWT Authentication
* User Registration
* User Login
* Role-Based Authorization
* Password Hashing using IPasswordHasher

### Articles
* Create Article
* Update Article
* Delete Article
* Get All Articles
* Pagination Support for Get All Articles

### Comments

* Add Comment to Article
* Update Comment
* Delete Comment
* Get Comments by Article

### Favorite Articles

* Add Article to Favorite Articles
* Remove Article from Favorite Articles
* View User Favorite Articles

### Trending Articles

Trending articles are calculated using metrics such as:

* Views
* Comments

A background service periodically refreshes trending data and stores it in Redis.

Benefits:

* Reduced database load
* Faster API response times
* Scalable architecture

## Technology Stack

* ASP.NET Core 10
* Entity Framework Core
* SQL Server
* StackExchangeRedis
* JWT Authentication
* Serilog

### Database

* SQL Server (Local Server using Docker)

### Caching

* Redis (Local Server using Docker)

### Logging

* Serilog

### Documentation

* Scalar / OpenAPI

### Architecture

#### Controllers
Handle HTTP requests and responses.

#### Services
Contain business logic and validation.

#### Repositories
Responsible for data access and database operations.

#### DTOs
Used for request and response contracts.

#### Models
Data models for entities.

#### Data
Data access layer for repositories.

#### Validators
Validate incoming requests.

#### Middleware
Custom middleware for error handling and logging.

#### Exceptions
Custom exceptions for handling errors.

#### Extensions
Custom extension methods for common tasks.

#### Enums
Define custom enums for domain entities.

## Project Structure

```text
NewsApi
│
├── Controllers
├── Services
├── Repositories
├── DTOs
├── Models
├── Data
├── Validations
├── Middlewares
├── Exceptions
├── Extensions
├── Enums
├── Migrations
├── Logs
└── BackgroundServices
```

## Database Design

### Users

Stores user information.

### Articles

Stores article content and metadata.

### Comments

Stores comments associated with articles.

### Favorites

Stores user favorite articles.

## Trending Article Flow

```text
Background Service
        ↓
Calculate Trending Articles
        ↓
Store Results in Redis
        ↓
API Request
        ↓
Read from Redis
        ↓
Return Response
```

This approach avoids expensive database queries on every request.

## API Endpoints

### Authentication

```http
POST /api/auth/register
POST /api/auth/login
```

### Articles

```http
GET    /api/articles
GET    /api/articles/{id}
POST   /api/articles
PUT    /api/articles/{id}
DELETE /api/articles/{id}
```

### Comments

```http
GET    /api/articles/{id}/comments
POST   /api/comments
PUT    /api/comments/{id}
DELETE /api/comments/{id}
```

### Favorites

```http
POST   /api/favorite-articles/{articleId}
DELETE /api/favorite-aritcles/{articleId}
GET    /api/favorite-articles
```

### Trending

```http
GET /api/articles/trending
```

## Running Locally

### Prerequisites

* .NET 9 SDK
* SQL Server
* Redis

### Clone Repository

```bash
git clone <repository-url>
```

### Update Connection Strings in appsettings.Development.json or in Environment Variables

Configure:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=...",
    "Redis": "localhost:6379"
  },
  "Jwt": {
    "Key": "...",
    "Issuer": "...",
    "Audience": "..."
  }
}
```

### Run Migrations

```bash
dotnet ef database update
```

### Run Application

```bash
dotnet run
```

### Open Swagger

```text
https://localhost:5195/scalar
```

## Key Concepts Demonstrated

* Clean Architecture Principles
* Repository Pattern
* Dependency Injection
* JWT Authentication
* Entity Framework Core
* Redis Caching
* Background Services
* Exception Handling
* DTO Mapping
* Validation
* Logging
* RESTful API Design
* Extension Methods
