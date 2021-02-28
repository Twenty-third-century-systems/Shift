using System;

namespace Cabinet.Dtos.Internal.Response {
    public class AllocatedTaskResponseDto {
        public int ExaminationTaskId { get; set; }
        public int ApplicationCount { get; set; }
        public string Service { get; set; }
        public DateTime ExpectedDateOfCompletion { get; set; }
    }
}