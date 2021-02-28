using System.Collections.Generic;
using Cabinet.Dtos.External.Request;

namespace Cabinet.Dtos.Internal.Response {
    public class TaskPrivateEntityLocalEntityShareHoldersRequestDto {
        public TaskPrivateEntityLocalEntityShareHoldersRequestDto()
        {
            Nominees = new List<TaskPrivateEntityShareHolderResponseDto>();
        }
        public string Name { get; set; }
        public string Reference { get; set; }

        public List<TaskPrivateEntityShareHolderResponseDto> Nominees { get; set; }
        public List<ShareholderSubscriptionDto> Subscriptions { get; set; }
    }
}