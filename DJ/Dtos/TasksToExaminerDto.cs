using System.Collections.Generic;
using DJ.Models;

namespace DJ.Dtos {
    public class TasksToExaminerDto {
        public List<Task> NameSearchTasks { get; set; } = new List<Task>();
        public List<Task> PvtEntityTasks { get; set; } = new List<Task>();
    }
}