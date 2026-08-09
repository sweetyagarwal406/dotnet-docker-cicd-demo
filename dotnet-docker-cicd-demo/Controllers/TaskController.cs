using Microsoft.AspNetCore.Mvc;
using dotnet_docker_cicd_demo.Models;
using dotnet_docker_cicd_demo.Services;

namespace dotnet_docker_cicd_demo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TaskItem>> GetAll()
        {
            return Ok(_taskService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<TaskItem> GetById(int id)
        {
            var task = _taskService.GetById(id);
            if (task is null) return NotFound();
            return Ok(task);
        }

        [HttpPost]
        public ActionResult<TaskItem> Create([FromBody] TaskItem task)
        {
            var created = _taskService.Create(task);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] TaskItem task)
        {
            var updated = _taskService.Update(id, task);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _taskService.Delete(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}