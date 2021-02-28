using System;

namespace Cabinet.Dtos.Internal.Response {
    public class AllocatedPrivateEntityTaskApplicationResponseDto {
        public int ApplicationId { get; set; }
        public Guid User { get; set; }
        public string Service { get; set; }
        public DateTime DateSubmitted { get; set; }
        public DateTime DateExamined { get; set; }
        public string Status { get; set; }

        public TaskPrivateEntityResponseDto PrivateEntity { get; set; }
    }
}