using CRUD_Operation.Controllers;
using CRUD_Operation.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using dbTask = CRUD_Operation.Data.Task;
using Task = System.Threading.Tasks.Task;

namespace CRUD_Operation.DatabaseOperations;

public class DatabaseOperations
{
    private readonly AppDbContext _dbOperation;

    public DatabaseOperations(AppDbContext dbOperation)
    {
        _dbOperation = dbOperation;
    }


    public async Task<List<dbTask>> GetAllTasksFromDatabase()
    {
        return await _dbOperation.Tasks.ToListAsync<dbTask>();
    }

    public async Task<List<dbTask>> GetTaskByID_Async(Guid id)
    {
        return await _dbOperation.Tasks.Where(t => t.Id == id).ToListAsync();
    }

    public async Task<dbTask> AddTaskToDatabase(dbTask task)
    {
        var result = await _dbOperation.Tasks.AddAsync(task);
        await _dbOperation.SaveChangesAsync();
        return await Task.FromResult(result.Entity);
    }

    public async Task<dbTask> UpdateByID(Guid id, dbTask task)
    {
        var record = await _dbOperation.Tasks.FindAsync(id);
        if (record == null)
        {
            return await Task.FromResult<dbTask>(null);
        }
        record.Title = task.Title;
        record.Description = task.Description;
        record.DueDate = task.DueDate;
        record.IsComplete = task.IsComplete;
      
        await _dbOperation.SaveChangesAsync();
        return await Task.FromResult(record);
        
    }
}