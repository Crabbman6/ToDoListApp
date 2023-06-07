# ToDoListApp

ToDoListApp is a simple web application that allows users to manage their tasks. It provides functionalities to create, view, and remove tasks.

## Technologies Used

1. **.NET 6.0:** The application is built on the .NET 6.0 platform
2. **ASP.NET Core MVC:** The application uses the Model-View-Controller (MVC) design pattern 
3. **Entity Framework Core:** 
4. **SQL Server:** The application uses a local SQL Server as the database to store tasks.
5. **Bootstrap:** The application uses Bootstrap for the front-end design.

## Code Structure

The application follows the MVC structure with Models, Views, and Controllers.

1. **Models:** The `TaskTodo` model represents a task with properties like `Id`, `Title`, `Description`, and `IsComplete`. The `ApplicationDbContext` class serves as the bridge between the models and the database.
2. **Views:** The views are written in Razor syntax and they define how the app's user interface will look and behave. There are views for creating a task (`Create.cshtml`) and for displaying all tasks (home page) (`Index.cshtml`).
3. **Controllers:** The `TasksController` handles all the interactions with the user. It has actions for displaying the list of tasks (`Index`), creating a new task (`Create`), and removing a task (`Delete`).
4. **Program.cs:** This is the entry point of the application. It sets up the web host, configures the app's services, and runs the app.

## How to Run the Application

If you wish to run the application, you will need an SQL server with a database named Tasks.

1. Clone the repository to your local machine.
2. Open the solution in Visual Studio.
3. Update the connection string in `appsettings.json` to point to your SQL Server instance.
4. Run the application by pressing `F5` or clicking the `Start Debugging` button.

## Incomplete Features
1. Currently the 'Remove' button on the index page is not functioning. 

## Future Improvements

- Adding user authentication and authorization.
- Allowing users to update tasks.
- Adding due dates for tasks.
- Allowing users to mark tasks as complete.
- Adding a search functionality to search tasks by title or description.

## Contributing

Pull requests for this project are encouraged. 

