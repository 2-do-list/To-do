using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoProject.Models
{
    public class Todo
    {
        public int id { get; set; }

        public string name { get; set; } = "test";

        public string todoTitle { get; set; } = "testing";

        public string todoContext { get; set; } = "testing";

        public int createdAt { get; set; }

        
    }
} 