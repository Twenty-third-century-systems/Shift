using System;

namespace Cabinet.Dtos.Internal.Response {
    public class AllocatedNameSearchTaskApplicationResponseDto {        
        public int ApplicationId { get; set; }
        public int Service { get; set; }
        public Guid User { get; set; }
        public DateTime DateSubmitted { get; set; }
        public DateTime DateExamined { get; set; }

        public TaskNameSearchResponseDto NameSearch { get; set; }
    }
}