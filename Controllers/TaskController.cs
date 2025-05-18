using System.Runtime.InteropServices.JavaScript;
using CRUD_Operation.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using static System.String;
using Task = CRUD_Operation.Data.Task;

namespace CRUD_Operation.Controllers
{
    [Route("api/task")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly DatabaseOperations.DatabaseOperations _dbOperation;

// This is the controller for the Task CRUD operations.

//there are two classes that are used using dependency injection. via constructor initialization.
//the first one is the database operations class which is used to perform the database operations.
// the second one is db context which is used to connect to the database and is initialized in database operations class..
        public TaskController(DatabaseOperations.DatabaseOperations dbOperation)
        {
            _dbOperation = dbOperation;
        }

//in total 4 methods are defined here. which enables to get create update delete operations on the database.
//all the api are asynchronous i.e., they do disturb the main thread, execute itself independently and then finish or
//save their result

//all the api do return some status code and a message.defined within the method body.
// the exception from the database are caught and an appropiate error and status code is generated.
//because database is sqlite which resides in the local machine .its never disconnected so those exceptions cannot
//be tested in this programming enviroment



        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _dbOperation.GetAllTasksFromDatabase();

                if (result == null)
                {
                    return NotFound("No datatable found ");
                }

                if (result.Count == 0)
                {
                    return StatusCode(204,
                        new
                        {
                            StatusCode = 204,
                            Message = " database connected and Empty database"
                        });
                }

                return StatusCode(200,
                    new
                    {
                        StatusCode = 200,
                        Message = " database connected all Entries in the Database",
                        Data = result
                    });
            }
            catch (DbUpdateException dbException)
            {
                return StatusCode(500,
                    new { Message = "Internal Server Error", Error = dbException.InnerException.Message });
            }
            catch (InvalidOperationException opException)
            {
                return StatusCode(500,
                    new { Message = "Database Config Error", Error = opException.InnerException.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    new { Message = "Internal Server Error", Error = ex.Message });
            }
        }

        [HttpGet("GetByID/{id}")]
        public async Task<IActionResult> GetByID_async([FromRoute] Guid id)
        {
            try
            {
                var result = await _dbOperation.GetTaskByID_Async(id);

                if (result == null)
                {
                    return NotFound("No datatable found ");
                }

                if (result.Count == 0)
                {
                    return StatusCode(404,
                        new
                        {
                            StatusCode = 404,
                            Message = " database connected and searched No Entry by this ID Exists in The Database"
                        });
                }

                return StatusCode(200,
                    new
                    {
                        StatusCode = 200,
                        Message = " database connected and searched Entry by this ID Exists in The Database",
                        Data = result
                    });
            }
            catch (DbUpdateException dbException)
            {
                return StatusCode(500,
                    new { Message = "Internal Server Error", Error = dbException.InnerException.Message });
            }
            catch (InvalidOperationException opException)
            {
                return StatusCode(500,
                    new { Message = "Database Config Error", Error = opException.InnerException.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    new { Message = "Internal Server Error", Error = ex.Message });
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> AddNewTask([FromBody] Task task)
        {
            if (task.Id == Guid.Empty)
            {
                task.Id = Guid.NewGuid();
            }

            if (task.Title == null || task.Title == "" || task.Title == " ")
            {
                return await System.Threading.Tasks.Task.FromResult(StatusCode(400, new
                {
                    StatusCode = 400,
                    Message = "Task title cannot be empty or whitespace. Please provide a valid title."
                }));
            }

            if (task.Description == null || task.Description == "" || task.Description == " ")
            {
                return await System.Threading.Tasks.Task.FromResult(StatusCode(400, new
                {
                    StatusCode = 400,
                    Message = "Task description cannot be empty or whitespace. Please provide a valid description."
                }));
            }

            if (task.DueDate == DateTime.MinValue || task.DueDate == DateTime.MaxValue || task.DueDate == null)
            {
                return await System.Threading.Tasks.Task.FromResult(StatusCode(400, new
                {
                    StatusCode = 400,
                    Message = "Task due date cannot be empty or whitespace. Please provide a valid due date."
                }));
            }

            task.DueDate = DateTime.Now;


            var result = await _dbOperation.AddTaskToDatabase(task);
            return StatusCode(201,
                new
                {
                    StatusCode = 201,
                    Message = " database connected and new task has been added to the database with status code 201"
                });
        }

        [HttpPut("UpdateByID/{id}")]
        public async Task<IActionResult> UpdateTask([FromRoute] Guid id, [FromBody] Task task)
        {
            var result = await _dbOperation.UpdateByID(id, task);

            if (result == null)
            {
                return StatusCode(404,
                    new
                    {
                        StatusCode = 404,
                        Message = " database connected and searched No Entry by this ID Exists in The Database"
                    });
            }

            return StatusCode(200,
                new
                {
                    StatusCode = 200,
                    Message = " database connected and searched Entry by this ID updated as requested"
                });
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteTask([FromRoute] Guid id)
        {
            var result = await _dbOperation.deleteByID(id);

            if (result == null)
            {
                return StatusCode(404,
                    new
                    {
                        StatusCode = 404,
                        Message = " database connected and searched No Entry by this ID Exists in The Database"
                    });
            }

            return StatusCode(200,
                new
                {
                    StatusCode = 200,
                    Message = " database connected and searched  Entry by this ID deleted from The Database"
                });


            ;
        }
    }
}