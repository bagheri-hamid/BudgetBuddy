# BudgetBuddy API

[![.NET Build](https://img.shields.io/badge/.NET-8.0-blueviolet)](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
[![Architecture](https://img.shields.io/badge/Architecture-Clean%20Architecture-blue)](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
[![Pattern](https://img.shields.io/badge/Pattern-CQRS%20%26%20Repository-orange)](https://martinfowler.com/bliki/CQRS.html)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

BudgetBuddy is a robust and scalable personal finance management API designed to help users track expenses, manage budgets, and gain insights into their financial habits. Developed as a portfolio piece, this project demonstrates a strong understanding of modern backend development principles, including Clean Architecture, Domain-Driven Design (DDD), and CQRS.

## 🏛️ Architectural Overview

This project is built using **Clean Architecture** to create a system that is independent of frameworks, UI, and databases. This separation of concerns results in a more maintainable, scalable, and testable application.

The core architectural principles are:

* **Domain Layer**: Contains the enterprise logic and business entities (e.g., `User`, `Account`, `Transaction`). It has no dependencies on any other layer.
* **Application Layer**: Orchestrates the data flow and contains the application-specific business rules. It implements the **CQRS (Command and Query Responsibility Segregation)** pattern using the `MediatR` library to separate read and write operations.
* **Infrastructure Layer**: Handles external concerns like database access, authentication, and logging. It implements the **Repository and Unit of Work patterns** for data persistence with Entity Framework Core.
* **API Layer (Presentation)**: The entry point to the application, exposing the functionalities through a versioned RESTful API.

This structure ensures that the core business logic is decoupled from external frameworks and technologies, making it highly adaptable and resilient to change.

## ✨ Features

* **Secure Authentication**: JWT-based authentication for secure user access.
* **Account Management**: Create and manage multiple financial accounts (e.g., checking, savings).
* **Transaction Tracking**: Log income and expense transactions with descriptions, categories, and dates.
* **Budget Planning**: Set monthly budgets for different spending categories.
* **Categorization**: Organize transactions with customizable, hierarchical categories.
* **Custom Exception Handling**: Global middleware for handling exceptions and returning consistent error responses.
* **Structured Logging**: In-depth logging with `Serilog`, including request/response bodies and user context for easier debugging.

## 🛠️ Technology Stack

| Component         | Technology / Library                                                                                                |
| ----------------- | ------------------------------------------------------------------------------------------------------------------- |
| **Framework** | [.NET 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)                                                    |
| **Database** | [SQL Server](https://www.microsoft.com/en-us/sql-server)                                                            |
| **ORM** | [Entity Framework Core 8](https://learn.microsoft.com/en-us/ef/core/)                                               |
| **API** | [ASP.NET Core Web API](https://learn.microsoft.com/en-us/aspnet/core/web-api/)                                      |
| **Architecture** | Clean Architecture, CQRS, Repository Pattern, Unit of Work                                                          |
| **Mediation** | [MediatR](https://github.com/jbogard/MediatR)                                                                       |
| **Authentication**| [JWT (JSON Web Tokens)](https://jwt.io/)                                                                            |
| **API Docs** | [Swagger (OpenAPI)](https://swagger.io/)                                                                            |
| **Logging** | [Serilog](https://serilog.net/)                                                                                     |
| **DI Scanning** | [Scrutor](https://github.com/khellang/Scrutor)                                                                      |
| **API Versioning**| [Asp.Versioning.Mvc](https://github.com/dotnet/aspnet-api-versioning)                                               |

## 🚀 Getting Started

Follow these instructions to get the project up and running on your local machine.

### Prerequisites

* [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
* [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (e.g., Express, Developer, or via Docker)
* A Git client

### Installation

1.  **Clone the repository:**
    ```bash
    git clone [https://github.com/bagheri-hamid/budgetbuddy.git](https://github.com/bagheri-hamid/budgetbuddy.git)
    cd budgetbuddy
    ```

2.  **Configure the database connection:**
   * Open the `BudgetBuddy/appsettings.Development.json` file.
   * Update the `SqlServerConnectionString` to point to your local SQL Server instance. The default is configured for a local server with a specific user and password.
       ```json
       "ConnectionStrings": {
         "SqlServerConnectionString": "data source=YOUR_SERVER;database=BudgetBuddy;user id=YOUR_USER;password=YOUR_PASSWORD;TrustServerCertificate=true;"
       }
       ```

3.  **Apply database migrations:**
   * Navigate to the main project directory (`/BudgetBuddy`).
   * Run the following command to create the database and apply the schema:
    ```bash
    dotnet ef database update --project ../src/BudgetBuddy.Infrastructure/ --startup-project .
    ```

4.  **Run the application:**
    ```bash
    dotnet run
    ```
    The API will start, and you can access it at `http://localhost:5264`.

## 📚 API Documentation

The API is fully documented using Swagger/OpenAPI. Once the application is running, navigate to the following URL in your browser to explore the interactive API documentation:

**`http://localhost:5264/swagger`**

Here you can view all available endpoints, see request/response models, and execute API calls directly.

## 📝 License

This project is licensed under the MIT License. See the [LICENSE](LICENSE.md) file for details.

## 📧 Contact

* Hamid Bagheri – https://linkedin.com/in/bagheri-hamid – HamidBagheri9846@gmail.com
* Samaneh Alimohamadi - https://linkedin.com/in/samaneh-alimohammadi-33b7a6248 - a.smneh79@gmail.com

Project Link: [https://github.com/bagheri-hamid/budgetbuddy](https://github.com/bagheri-hamid/budgetbuddy)
