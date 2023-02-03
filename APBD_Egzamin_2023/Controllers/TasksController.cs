using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace ProjectsAndTasks.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ProjectsContext _db;
    public TasksController(ProjectsContext db)
    {
        _db = db;
    }

    [HttpPost]
    public IActionResult Add(TaskRequest task)
    {
        using (_db)
        {
            using var transaction = _db.Database.BeginTransaction();
            try
            {
                if (!_db.TaskTypes.Any(t => t.Name == task.TaskType.Name))
                {
                    _db.TaskTypes.Add(task.TaskType);
                };
                var taskToAdd = new Task
                {
                    Name = task.Name,
                    Description= task.Description,
                    Deadline= task.Deadline,
                    IdTaskTypeNavigation = task.TaskType,
                    IdCreator = task.IdCreator,
                    IdAssignedTo = task.IdAssignedTo
                };

                _db.Tasks.Add(taskToAdd);

                _db.SaveChanges();

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                return BadRequest();
            }
        }

        return Created(string.Empty, task);
    }
}
