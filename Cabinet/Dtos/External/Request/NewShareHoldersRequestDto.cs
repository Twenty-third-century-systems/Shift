using System.Collections.Generic;

namespace Cabinet.Dtos.External.Request {
    public class NewShareHoldersRequestDto {
        public int ApplicationId { get; set; }
        public List<NewShareHolderRequestDto> People { get; set; }
        public List<NewShareHoldingEntityRequestDto> Entities { get; set; }
    }
}