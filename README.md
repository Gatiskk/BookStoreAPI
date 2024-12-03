# BookStoreAPI Project

This is a .NET WEB API for managing a book store. It provides functionality for CRUD operations on books, including features such as sorting, pagination, and filtering by genre. The API is built using .NET 8 and utilizes **Entity Framework Core** with **Serilog** for logging.

### Prerequisites

Before setting up and running the application, ensure you have the following installed:

- **.NET 8 SDK** or later: [Download .NET SDK](https://dotnet.microsoft.com/download)
- **Visual Studio 2022** or later (if you're using Visual Studio), or any code editor (like Visual Studio Code).
- **SQL Server** (based on your project configuration).
- **Git**: To clone the repository.

## Setting Up the Project

### 1. Clone the Repository

To get started, clone the repository using the following command:

```bash
git clone https://github.com/Gatiskk/BookStoreAPI.git
```
### 2. Navigate to the Project Folder
After cloning, go to the project directory:
```bash
cd BookStoreAPI
```
### 3. Restore NuGet Packages
Restore the required NuGet packages using the dotnet restore command:
```bash
dotnet restore
```
This will download all the dependencies defined in the .csproj file.

### 4. Set Up Database
The project uses Entity Framework Core for data storage. 
Make sure to configure your database connection string in appsettings.json or use environment variables for production secrets. 
The default setup assumes SQL Server. You may need to create or configure a local database - appsettings.json.

Example connection string in appsettings.json (update for your environment):
```bash
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=BookstoreDB;Trusted_Connection=True;TrustServerCertificate=True;"
},
```
### 5. Create Database Migrations (if using EF Core)
If you're using Entity Framework Core for database operations, create the database using migrations:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```
Running the Application
Once you’ve set up the project, you can run the application locally:

```bash
dotnet run
```
This will start the API on port (https://localhost:44372/).
### 6. Navigate to the Test Folder
After cloning, go to the project directory:
```bash
cd bookstoreapitests
```
And run
```bash
dotnet test
```
To run tests
### License
This project is licensed under the MIT License - see the LICENSE file for details.
