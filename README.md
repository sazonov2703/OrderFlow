# 🚀 OrderFlow  

---  

A service organizer for small businesses that helps track sales, customers, products, and other important data in one place—eliminating the need for Excel or notebooks.  

The current version includes an API, with plans to add a frontend for a user-friendly interface in the future.  

---  

## 🧩 Key Features  

- Customer management (CRUD operations)  
- Product and inventory management  
- Sales tracking  
- Clear separation of responsibilities thanks to Clean Architecture and CQRS  
- RESTful API for integration with external clients and future frontend  

---  

## 🧱 Architecture & Approaches  

- **Clean Architecture** – Code is divided into layers: presentation (future), application (business logic), domain, and infrastructure. This improves maintainability, testability, and flexibility.  
- **CQRS (Command Query Responsibility Segregation)** – Commands (state changes) and queries (data reads) are separated, enhancing scalability and simplifying management.  
- **Entity Framework Core** – Database operations with migration support and ORM.  
- **REST API** – Client interaction via HTTP using JSON.  
- **Dependency Injection** – For dependency inversion and easier testing.  
- **Automatic logging & error handling** – Simplifies maintenance and monitoring.  

---  

## 🛠 Technologies  

- C# / .NET 8  
- Entity Framework Core  
- ASP.NET Core Web API  
- MediatR (or equivalent) for CQRS implementation  
- Swagger / OpenAPI for API documentation  
- SQL Server / PostgreSQL (configurable)  

---  

## ⚙ Setup & Launch  

1. Clone the repository:  
   ```bash
   git clone https://github.com/sazonov2703/OrderFlow.git  
   cd OrderFlow
   
2. Configure the database connection string in appsettings.json.
   
3. Run database migrations:
   ```bash
   dotnet ef database update

4. Launch the API:
   ```bash
   dotnet run --project src/OrderFlow.Api

5. Access Swagger UI in your browser at:
   ```bash
   https://localhost:5001/swagger

---

## 📋 Future Plans
- Frontend development (React) for a seamless user experience
- Advanced analytics and reporting
- Integration with payment systems and third-party services

---

## 🌐 Contributing

Want to contribute? Fork the repository, make your changes, and submit a Pull Request!
