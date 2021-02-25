using System.Collections.Generic;

namespace Cabinet.Dtos.External.Request {
    public class NewShareClausesRequestDto {
        public int ApplicationId { get; set; }
        public List<NewShareClauseRequestDto> Clauses { get; set; }
    }
}