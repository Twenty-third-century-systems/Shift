using System;

namespace Cabinet.Dtos.Internal.Response {
    public class UnallocatedApplicationResponseDto {        
        public int ApplicationId { get; set; }
        public string Service { get; set; }
        public Guid User { get; set; }
        public DateTime DateSubmitted { get; set; }
        public DateTime DateExamined { get; set; }
    }
}