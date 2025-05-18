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
// this is the class which is supposed to be utilized exclusively for the database operation such which comes directly
//from api controller ....... only the code which is directly responsible for the operations .ie query done in th edatebase is w
//written here and not in the controller....
// all the validation and response code are written in the controller and not here!
//over here only the queries are formed.The results from the database are returned to the controller
//this keeps the code more and segregated, and it becomes easy to update the code without making changes in the controller or 
//other parts of the code which are not directly responsible for the database operation


// again all these are asynchronous so the time expended to retrieving results form the database ...during that time the 
//main application or api does not hang or suspend ......these execute independently of the main thread
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

    public async Task<dbTask> deleteByID(Guid id)
    {
            var task = await _dbOperation.Tasks.FindAsync(id);
            if (task == null)
            {
                return await Task.FromResult<dbTask>(null);
            }
            _dbOperation.Tasks.Remove(task);
            await _dbOperation.SaveChangesAsync();
            return await Task.FromResult(task);
    }
}