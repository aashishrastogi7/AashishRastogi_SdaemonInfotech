using Microsoft.EntityFrameworkCore;

namespace CRUD_Operation.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Task>().HasData(
            new Task
            {
                Description = "Testing the data insertion on time of Creation i.e Master data",
                Title = "Test Task",
                DueDate = DateTime.Now,
                Id = Guid.NewGuid(),
                IsComplete = true
            },
            new Task
            {
                Description = "task 2 for testing getall api",
                Title = "Task 2",
                DueDate = DateTime.Now,
                Id = Guid.NewGuid(),
                IsComplete = true
            },
            new Task
            {
                Description = "task 3 for testing getbyID api",
                Title = "Task3",
                DueDate = DateTime.Now,
                Id = Guid.NewGuid(),
                IsComplete = true
            }
        );
    }


    public DbSet<Task> Tasks { get; set; }
}