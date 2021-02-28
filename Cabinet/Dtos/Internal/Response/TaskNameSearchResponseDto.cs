using System;
using System.Collections.Generic;

namespace Cabinet.Dtos.Internal.Response {
    public class TaskNameSearchResponseDto {        
        public int NameSearchId { get; set; }
        public int Service { get; set; }
        public string Justification { get; set; }
        public int Designation { get; set; }
        public int ReasonForSearch { get; set; }

        public List<TaskNameSearchNameResponseDto> Names { get; set; }
    }
}