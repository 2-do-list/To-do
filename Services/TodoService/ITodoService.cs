using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoProject.Models;

namespace TodoProject.Services.TodoService
{
    public interface ITodoService
    {
         Task<ServiceResponse<List<GetTodoDto>>>GetAllTodos();
         Task<ServiceResponse<GetTodoDto>> GetTodoById(int id);
         Task<ServiceResponse<List<GetTodoDto>>> AddTodo(AddTodoDto newTodo);
         Task<ServiceResponse<GetTodoDto>> UpdateTodo(UpdateTodoDto updateTodo);
         Task<ServiceResponse<List<GetTodoDto>>> DeleteTodo(int id);

    } 
}