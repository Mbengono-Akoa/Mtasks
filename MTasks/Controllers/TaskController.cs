using Microsoft.AspNetCore.Mvc;
using MTasks.Models;

namespace MTasks.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private static List<TaskModel> tasks = new List<TaskModel>();
        private static int nextId = 1;

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);

            if (task == null)
                return NotFound($"Task with id {id} not found");

            return Ok(task);
        }

        [HttpPost]
        public IActionResult Create(TaskModel model)
        {
            if (model == null || string.IsNullOrWhiteSpace(model.Title))
                return BadRequest("Title is required");

            model.Id = nextId++;
            tasks.Add(model);

            return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, TaskModel updatedTask)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);

            if (task == null)
                return NotFound($"Task with id {id} not found");

            if (!string.IsNullOrWhiteSpace(updatedTask.Title))
                task.Title = updatedTask.Title;

            task.IsCompleted = updatedTask.IsCompleted;

            return Ok(task);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);

            if (task == null)
                return NotFound($"Task with id {id} not found");

            tasks.Remove(task);

            return NoContent();
        }
    }
}