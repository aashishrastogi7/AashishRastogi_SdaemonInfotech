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

        public TaskController(DatabaseOperations.DatabaseOperations dbOperation)
        {
            _dbOperation = dbOperation;
        }

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
            return Ok(result);
        }

        [HttpPut("UpdateByID/{id}")]
        public async Task<IActionResult> UpdateTask([FromRoute] Guid id, [FromBody] Task task)
        {
            var result = await _dbOperation.UpdateByID(id , task);
         
            if (result == null)
            {
                return StatusCode(404,
                    new
                    {
                        StatusCode = 404,
                        Message = " database connected and searched No Entry by this ID Exists in The Database"
                    });
            }
            
          
            return Ok(result);
        }
        
        
    }
}