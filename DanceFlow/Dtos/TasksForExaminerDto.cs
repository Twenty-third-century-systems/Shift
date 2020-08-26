using System.Collections.Generic;
using DanceFlow.Models;

namespace DanceFlow.Dtos {
    public class TasksForExaminerDto {
        public List<TaskSummary> NameSearchTasks { get; set; } = new List<TaskSummary>();
        public List<TaskSummary> PvtEntityTasks { get; set; } = new List<TaskSummary>();
    }
}