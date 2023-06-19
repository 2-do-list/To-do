using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoProject
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Todo, GetTodoDto>();
            CreateMap<AddTodoDto, Todo>();
            CreateMap<UpdateTodoDto, Todo>();
        }
    }
}