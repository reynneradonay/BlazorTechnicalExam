using BlazorTechnicalExam.Server.Interfaces;
using BlazorTechnicalExam.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlazorTechnicalExam.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoService _toDoService;

        public ToDoController(IToDoService toDoService)
        {
            _toDoService = toDoService;
        }

        [HttpGet]
        public IEnumerable<ToDo> Get()
        {
            return _toDoService.GetToDoList()
                .ToArray();
        }

        [HttpPost]
        public IActionResult Insert([FromBody] ToDo toDo)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            toDo = _toDoService.InsertToDo(toDo);

            return Ok(toDo);
        }

        [HttpPut]
        public IActionResult Update([FromBody] ToDo toDo)
        {
            toDo = _toDoService.UpdateToDo(toDo);

            return Ok(toDo);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _toDoService.DeleteToDo(id);

            return Ok();
        }
    }
}
