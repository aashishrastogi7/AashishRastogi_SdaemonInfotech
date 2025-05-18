using Microsoft.EntityFrameworkCore;

namespace CRUD_Operation.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions)
    {
    }
// this is the class which is supposed to be utilized exclusively for the database operation such \
// as connections and validations of connections ie heartbeat or continuous pinging of the database
// and also to include one time operations like seeding data to the database\
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

// this is what makes and tells the entity framework that based on this model table is to be construted ..ie migration script
    public DbSet<Task> Tasks { get; set; }
}