using System;
using System.Collections.Generic;

namespace Cabinet.Dtos.Internal.Response {
    public class TaskNameSearchResponseDto {        
        public int NameSearchId { get; set; }
        public string Service { get; set; }
        public string Justification { get; set; }
        public string Designation { get; set; }
        public string ReasonForSearch { get; set; }

        public List<TaskNameSearchNameResponseDto> Names { get; set; }
    }
}