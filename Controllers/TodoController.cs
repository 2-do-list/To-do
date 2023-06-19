using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TodoProject.Models;

namespace TodoProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
      
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetTodoDto>>> GetTodos()
        {
            return Ok(await _todoService.GetAllTodos()); // Return the array of todos as the response
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<ServiceResponse<GetTodoDto>>> GetSingle(int id)
        {
            return Ok(await _todoService.GetTodoById(id)); // Return todo with matched id
        }

        [HttpPost]

        public async Task<ActionResult<ServiceResponse<List<GetTodoDto>>>> AddTodo(AddTodoDto newTodo) 
        { 
            return Ok(await _todoService.AddTodo(newTodo));
        }

        [HttpPut]

        public async Task<ActionResult<ServiceResponse<GetTodoDto>>> UpdateTodo(UpdateTodoDto updatedTodo) 
        { 
            var response = await _todoService.UpdateTodo(updatedTodo);
            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response );
        }

        [HttpDelete]

        public async Task<ActionResult<ServiceResponse<GetTodoDto>>> DeleteTodo(int id) 
        { 
            var response = await _todoService.DeleteTodo(id);
            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response );
        }


    }
}








// namespace TodoProject.Controllers
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     public class TodoController : Controller
//     {
//         private readonly ILogger<TodoController> _logger;

//         public TodoController(ILogger<TodoController> logger)
//         {
//             _logger = logger; 
//         }

//         public IActionResult Index()
//         {
//             return View();
//         }

//         [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//         public IActionResult Error()
//         {
//             return View("Error!");
//         }
//     }
// }