using System.Collections.Generic;
using DJ.Models;

namespace DJ.Dtos {
    public class TasksToExaminerDto {
        public List<TaskSummary> NameSearchTasks { get; set; } = new List<TaskSummary>();
        public List<TaskSummary> PvtEntityTasks { get; set; } = new List<TaskSummary>();
    }
}