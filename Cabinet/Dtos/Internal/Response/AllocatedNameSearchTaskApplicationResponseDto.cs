using System;

namespace Cabinet.Dtos.Internal.Response {
    public class AllocatedNameSearchTaskApplicationResponseDto {        
        public int ApplicationId { get; set; }
        public string Service { get; set; }
        public Guid User { get; set; }
        public DateTime DateSubmitted { get; set; }
        public bool Examined { get; set; }

        public TaskNameSearchResponseDto NameSearch { get; set; }
    }
}