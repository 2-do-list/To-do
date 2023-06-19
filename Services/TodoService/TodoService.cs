using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoProject.Models;


namespace TodoProject.Services.TodoService
{
    public class TodoService : ITodoService
    {
        private static List<Todo> todos = new List<Todo>{
            
            new Todo { id = 1, name = "John Doe", todoTitle = "Task 1", todoContext = "Do something", createdAt = 123456 },
            new Todo { id = 2, name = "Jane Smith", todoTitle = "Task 2", todoContext = "Do something else", createdAt = 789012 },
            //Add more Todo objects as needed
        };
        private readonly IMapper _mapper;

        public TodoService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetTodoDto>>> AddTodo(AddTodoDto newTodo)
        {
            var serviceResponse = new ServiceResponse<List<GetTodoDto>>();
            var todo = _mapper.Map<Todo>(newTodo);
            todo.id = todos.Max(t => t.id) + 1; 
            todos.Add(todo);
            serviceResponse.Data = todos.Select(t => _mapper.Map<GetTodoDto>(t)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetTodoDto>>> DeleteTodo(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetTodoDto>>();
              
            try
            {
                var todo = todos.First (t => t.id == t.id);
                if (todo is null)
                    throw new Exception($"character with id '{id}' not found"); 
                
                todos.Remove(todo); 
            
                serviceResponse.Data = todos.Select(t => _mapper.Map<GetTodoDto>(t)).ToList();  
            }
            catch (Exception ex) 
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
 
            } 

            return serviceResponse;

        }

        public async Task<ServiceResponse<List<GetTodoDto>>> GetAllTodos()
        {
            var serviceResponse = new ServiceResponse<List<GetTodoDto>>();  
            serviceResponse.Data = todos.Select(t => _mapper.Map<GetTodoDto>(t)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetTodoDto>> GetTodoById(int id)
        {
            var serviceResponse = new ServiceResponse<GetTodoDto>(); 
            var todo = todos.FirstOrDefault(t => t.id == id);
            serviceResponse.Data = _mapper.Map<GetTodoDto>(todo);
            return serviceResponse; 
     
        }

        public async Task<ServiceResponse<GetTodoDto>> UpdateTodo(UpdateTodoDto updatedTodo)
        {
            var serviceResponse = new ServiceResponse<GetTodoDto>();
            
             try
            {
                var todo = todos.FirstOrDefault(t => t.id == updatedTodo.id);
                if (todo is null)
                    throw new Exception($"character with id '{updatedTodo.id}' not found");

                _mapper.Map(updatedTodo, todo);  

                todo.name = updatedTodo.name;
                todo.todoTitle = updatedTodo.todoTitle;
                todo.todoContext = updatedTodo.todoContext;
                todo.createdAt = updatedTodo.createdAt; 

                serviceResponse.Data = _mapper.Map<GetTodoDto>(todo);
            }
            catch (Exception ex) 
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
 
            } 

            return serviceResponse;    
        }

    }
}