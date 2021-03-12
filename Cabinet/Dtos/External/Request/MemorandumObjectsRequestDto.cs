using System.Collections.Generic;

namespace Cabinet.Dtos.External.Request {
    public class MemorandumObjectsRequestDto {        
        public int ApplicationId { get; set; }
        public List<SingleObjectiveRequestDto> Objects { get; set; }
    }
}