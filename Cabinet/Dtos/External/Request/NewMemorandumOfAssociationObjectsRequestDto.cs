using System.Collections.Generic;

namespace Cabinet.Dtos.External.Request {
    public class NewMemorandumOfAssociationObjectsRequestDto {
        public int ApplicationId { get; set; }
        public List<NewMemorandumOfAssociationObjectRequestDto> Objects { get; set; }
    }
}