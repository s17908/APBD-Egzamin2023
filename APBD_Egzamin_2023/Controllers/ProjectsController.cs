using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ProjectsAndTasks.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly ProjectsContext _db;

    public ProjectsController( ProjectsContext db )
    {
        _db = db;
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var project = _db.Projects
            .Include(p => p.Tasks)
            .ThenInclude(t => t.IdTaskTypeNavigation)
            .FirstOrDefault(p => p.IdProject == id);

        if (project == null)
        {
            return NotFound();
        }

        project.Tasks = project.Tasks.OrderByDescending(t => t.Deadline).ToList();

        return Ok(project);
    }
}