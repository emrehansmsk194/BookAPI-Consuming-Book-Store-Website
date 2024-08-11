# BookAPI & BookWEB Project

This repository contains two main projects:

- **BookAPI**: A RESTful API developed using ASP.NET Core for managing books, publishers, and categories. It includes endpoints for CRUD operations, as well as endpoints for retrieving books by category or publisher. The API also features authentication for secure access.
- **BookWEB**: A web application developed using ASP.NET Core MVC that consumes the BookAPI to create a book store website. It allows users to register, log in, manage their book collections, and purchase books.

Technologies used include AutoMapper, DTOs, Repository Pattern, Entity Framework Core, Dependency Injection, and ASP.NET Identity for authentication.

## Getting Started

### Prerequisites
- .NET SDK 6.0 or later
- SQL Server (or any compatible database)
- Visual Studio 2022 (or any compatible IDE)
- Git

### Setup Instructions

1. **Clone the repository**:
   ```bash
   git clone https://github.com/emrehansmsk194/BookAPI-Consuming-Book-Store-Website.git
   cd BookAPI-BookWEB
   ```

2. **Set up the database**:
   - Navigate to the `BookAPI` project and configure the `appsettings.json` file to point to your SQL Server instance.
   - Run the migrations to set up the database:
     ```bash
     cd BookAPI
     dotnet ef database update
     ```

3. **Run the API**:
   - Build and run the `BookAPI` project:
     ```bash
     dotnet run
     ```

4. **Set up the web application**:
   - Navigate to the `BookWEB` project and configure the `appsettings.json` to point to the API and database.
   - Run the web application:
     ```bash
     cd BookWEB
     dotnet run
     ```

5. **Access the application**:
   - API: `https://localhost:5182/api/`
   - Web: `https://localhost:5251/`
  



  ## Project Structure

- **BookAPI**: 
  - `Controllers/`: Contains the API controllers for managing books, categories, and publishers.
  - `Models/`: Contains the data models and DTOs used by the API.
  - `Repository/`: Implements the repository pattern for data access.
  - `Services/`: Contains services for authentication and other business logic.
  - `Migrations/`: Contains the EF Core migrations for database management.

- **BookWEB**: 
  - `Controllers/`: Contains the MVC controllers for handling user interactions and views.
  - `Views/`: Contains Razor views for rendering the user interface.
  - `Models/`: Contains the view models and DTOs for the web application.
  - `Services/`: Contains services for API consumption and business logic.
  - `wwwroot/`: Contains static files such as CSS, JavaScript, and images.


## Technologies Used

- **ASP.NET Core**: Framework for building the API and web application.
- **Entity Framework Core**: ORM for database operations and migrations.
- **AutoMapper**: Used for mapping between models and DTOs.
- **ASP.NET Identity**: Handles authentication and authorization.
- **Repository Pattern**: Decouples the business logic from the data access layer.
- **DTOs (Data Transfer Objects)**: Used to encapsulate data for transfer between layers.
- **Dependency Injection**: Provides a way to manage dependencies and promote loose coupling.


## API Documentation

### Endpoints

- **Books**
  - `GET /api/books`: Get all books with optional search and pagination.
  - `GET /api/books/{id}`: Get a specific book by ID.
  - `POST /api/books`: Create a new book (admin only).
  - `PUT /api/books/{id}`: Update an existing book (admin only).
  - `DELETE /api/books/{id}`: Delete a book (admin only).
  - `GET /api/books/category={categoryId}`: Get books by category.
  - `GET /api/books/publisher={publisherId}`: Get books by publisher.

- **Authentication**
  - `POST /api/users/login`: User login.
  - `POST /api/users/register`: User registration.

## Contributing

If you'd like to contribute to this project, please follow these steps:

1. Fork the repository.
2. Create a new branch (`git checkout -b feature-branch`).
3. Commit your changes (`git commit -m 'Add some feature'`).
4. Push to the branch (`git push origin feature-branch`).
5. Open a pull request.

