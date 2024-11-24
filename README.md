Task_Xceed
.NET Core MVC application that demonstrates an implementation of authentication, session management, and product management using a Repository Pattern and Unit of Work architecture.
This project is designed to showcase clean architectural principles, proper database handling with Entity Framework, and how to create a web application with admin and user roles.




Technologies

.NET Core MVC: Backend framework.
Entity Framework Core: ORM for database handling.
SQL Server: Database management system.
Bootstrap: Frontend styling framework.
NUnit: Unit testing framework.




Prerequisites

.NET 8 SDK or higher installed on your system
SQL Server installed locally or accessible on your network.
Visual Studio 2022 or any IDE that supports .NET projects.




Run

Download or Clone the project 
then u can open Tools -> Nuget Package Manager -> Package Manager Console 
then choose default project : "RepositoryPatternWithUOW.EF"
then write : "update-database" 




Usage

Admin Role
Use the Admin role to:

Manage categories and products (CRUD operations).
View detailed product information.
Access the admin dashboard for management tasks.
To set up an admin user:
Register a new user.
Update the Role field for the user in the database to Admin.

Client Role
Regular users (clients) can:

View product details.
Access a limited product list view.
Cannot perform CRUD operations.

Unauthenticated Users
Unauthenticated users cannot view product or category details.
Attempting to access these pages redirects them to the login page.




Running Unit Tests

The project includes NUnit small test cases for controllers and services.
Open the Test Explorer in Visual Studio. "View -> Test Explorer -> Run "
Run all tests to ensure the functionality works as expected.
