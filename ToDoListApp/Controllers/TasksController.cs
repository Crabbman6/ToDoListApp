//TasksControllers.cs
namespace ToDoListApp.Controllers
{
    // Required libraries
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading.Tasks;
    using ToDoListApp.Models;

    //  TasksController, where the tasks are handled.
    public class TasksController : Controller
    {
        // Context to communicate with the database
        private readonly ApplicationDbContext _context;

        // _logger for debugging 
        private readonly ILogger<TasksController> _logger;

        // When someone makes a new TasksController, they need to give us a context.
        public TasksController(ApplicationDbContext context, ILogger<TasksController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // When someone goes to /Tasks, they'll hit this action.
        public async Task<IActionResult> Index()
        {
            // We get all the tasks from the database and pass them to the view.
            return View(await _context.Tasks.ToListAsync());
        }

        // When someone goes to /Tasks/Create, they'll hit this action.
        public IActionResult Create()
        {
            // Show them the create view
            return View();
        }

        // When someone posts to /Tasks/Create, they'll hit this action.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description")] TaskTodo task)
        {
            // Log the value of ModelState.IsValid
            _logger.LogInformation("ModelState.IsValid: " + ModelState.IsValid); 

            // If their task looks good
            if (ModelState.IsValid)
            {
                // Set IsComplete to false
                task.IsComplete = false;

                // Add the task to the database
                _context.Add(task);
                await _context.SaveChangesAsync();

                // And then send them back to the list of tasks.
                return RedirectToAction(nameof(Index));
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.SelectMany(x => x.Value.Errors.Select(z => z.ErrorMessage));
                // Log these errors or print them to the console
                foreach (var error in errors)
                {
                    _logger.LogError(error?.ToString());
                }
                var exceptions = ModelState.SelectMany(x => x.Value.Errors.Select(z => z.Exception));
                foreach (var exception in exceptions)
                {
                    if (exception != null)
                    {
                        _logger.LogError(exception.ToString());
                    }
                }
                return View(task);
            }


            // If their task doesn't look good, show them the create view again.
            ModelState.AddModelError(string.Empty, "Error: Task could not be created. Please check your input and try again.");
            return View(task);
            
        }

        // When someone posts to /Tasks/Edit, they'll hit this action.
        public async Task<IActionResult> Edit(int id)
        {
            // We find the task they're trying to edit.
            var taskTodo = await _context.Tasks.FindAsync(id);
            if (taskTodo == null)
            {
                // If we can't find it, we return a 404 error.
                return NotFound();
            }

            // If we find it, we flip its IsComplete status.
            taskTodo.IsComplete = !taskTodo.IsComplete;
            _context.Update(taskTodo);
            await _context.SaveChangesAsync();

            // And then send them back to the list of tasks.
            return RedirectToAction(nameof(Index));
        }

        // When someone posts to /Tasks/Delete, they'll hit this action.
        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int Id)
        {
            _logger.LogInformation("Delete action called with id:" + Id);
            // We find the task they're trying to delete.
            var task = await _context.Tasks.FindAsync(Id);
            if (task == null)
            {
                // If we can't find it, we return a 404 error.
                return NotFound();
            }

            // If we find it, we remove it from the database.
            //_context.Tasks.Remove(task);
            _context.Entry(task).State = EntityState.Deleted; 
            await _context.SaveChangesAsync();

            // And then send them back to the list of tasks.
            return RedirectToAction(nameof(Index));
        }
    }
}
