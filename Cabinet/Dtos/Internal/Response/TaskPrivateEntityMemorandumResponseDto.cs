using System.Collections.Generic;

namespace Cabinet.Dtos.Internal.Response {
    public class TaskPrivateEntityMemorandumResponseDto {
        public string LiabilityClause { get; set; }
        public List<TaskPrivateEntityShareClauseResponseDto> ShareClauses { get; set; }
        public List<TaskPrivateEntityMemorandumObjectResponseDto> MemorandumObjects { get; set; }
    }
}