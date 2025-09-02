# ECommerce API

A RESTful API for an e-commerce platform built with ASP.NET Core 9.0, implementing clean architecture principles.

## 🚀 Features

- **Product Management**: CRUD operations for products with stock tracking
- **Order Management**: Shopping cart functionality with order processing
- **User Management**: User registration and management
- **Clean Architecture**: Separated concerns with Domain, Application, Infrastructure, and Host layers
- **Entity Framework Core**: Code-first approach with SQL Server
- **Swagger/OpenAPI**: Interactive API documentation

## 🛠️ Tech Stack

- **Framework**: ASP.NET Core 9.0
- **Database**: SQL Server (via Entity Framework Core 9.0)
- **ORM**: Entity Framework Core with Code-First migrations
- **Documentation**: Swagger/OpenAPI
- **Security**: BCrypt.Net for password hashing
- **Architecture**: Clean Architecture pattern
- **ID Generation**: FastGuid for optimized GUID generation

## 📋 Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or SQL Server Express
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/) (optional)

## ⚙️ Setup Instructions

### 1. Clone the Repository
```bash
git clone <repository-url>
cd ECommerceApi
```

### 2. Database Configuration
Update the connection string in `appsettings.Development.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=ECommerceApiDb;User Id=YOUR_USER;Password=YOUR_PASSWORD;TrustServerCertificate=True;"
  }
}
```

### 3. Database Migration
Run the following commands to create and seed the database:
```bash
dotnet ef database update
```

### 4. Run the Application
```bash
dotnet run
```

The API will be available at:
- **HTTP**: `http://localhost:5190`
- **Swagger UI**: `https://localhost:5190/swagger`

## 📁 Project Structure

```
ECommerceApi/
├── Domain/                 # Core business entities
│   └── Entities/          # Domain models (Product, Order, User, etc.)
├── Application/           # Business logic layer
│   ├── DTOs/             # Data Transfer Objects
│   ├── Interfaces/       # Service contracts
│   └── Services/         # Business logic implementation
├── Infrastructure/       # Data access layer
│   └── Data/            # DbContext and configurations
├── Host/                 # API controllers and presentation layer
│   └── Controllers/     # REST API endpoints
└── Migrations/          # EF Core database migrations
```

## 🗄️ Database Schema

The application includes the following main entities:

- **Products**: Product catalog with stock management
- **Users**: User accounts and profiles
- **Orders**: Shopping cart and order processing
- **OrderItems**: Individual items within orders

## 🔧 Key Features & Implementation Notes

### Shopping Cart Functionality
- Users can add products to their cart (creates an unchecked-out order)
- Automatic stock quantity management
- Support for multiple quantities of the same product
- Real-time total amount calculation

### Entity Framework Optimization
- **Important**: When working with related entities, use explicit context operations (e.g., `_context.OrderItems.AddAsync()`) rather than navigation property modifications to avoid entity tracking conflicts.

## 🧪 API Endpoints

### Products
- `GET /api/product/GetAll` - Get all products
- `GET /api/product/GetById/{id}` - Get product by ID
- `POST /api/product/Create` - Create new product
- `PUT /api/product/Update/{id}` - Update product
- `PUT /api/product/Delete/{id}` - Delete product (soft delete)

### Orders
- `GET /api/order/GetAll` - Get all orders
- `GET /api/order/GetById/{orderId}` - Get order by ID
- `GET /api/order/GetUserActiveCart/{userId}` - Get user's active cart
- `POST /api/order/Create/{productId}` - Add product to cart
- `PUT /api/order/Delete/{orderId}` - Delete order

### Users
- `POST /api/user/Register` - Register new user
- `POST /api/user/Login` - Login user

## 🔒 Security Considerations

- Passwords are hashed using BCrypt
- SQL injection protection via Entity Framework parameterized queries
- Input validation through DTOs
- HTTPS enforcement in production

## 📝 Assumptions

1. **Single Currency**: All prices are in a single currency (decimal type)
2. **Simple Authentication**: Basic user management without JWT/OAuth implementation
3. **Stock Management**: Simple stock decrement on order creation (no complex inventory tracking)
4. **Order States**: Orders are either "in cart" (!IsCheckedOut) or "completed" (IsCheckedOut)
5. **Single Tenant**: Application designed for single-tenant use

## 🚨 Known Limitations

- No authentication/authorization middleware implemented
- No payment processing integration
- Basic error handling (can be enhanced with global exception handling)

## 🔄 Future Enhancements

- [ ] JWT Authentication & Authorization
- [ ] Payment gateway integration
- [ ] Advanced inventory management
- [ ] Order status tracking
- [ ] Email notifications
- [ ] Admin dashboard
- [ ] Product categories and search
- [ ] User reviews and ratings