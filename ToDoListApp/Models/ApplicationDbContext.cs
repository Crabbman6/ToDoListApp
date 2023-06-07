//ApplicationDbContext.cs

namespace ToDoListApp.Models

{ using Microsoft.EntityFrameworkCore;
public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<TaskTodo> Tasks { get; set; }
    }
}
