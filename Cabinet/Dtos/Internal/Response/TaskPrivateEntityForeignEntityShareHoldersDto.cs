using System.Collections.Generic;
using Cabinet.Dtos.External.Request;

namespace Cabinet.Dtos.Internal.Response {
    public class TaskPrivateEntityForeignEntityShareHoldersDto {
        
        public string Name { get; set; }
        public string Reference { get; set; }
        public string Country { get; set; }

        public List<TaskPrivateEntityShareHolderResponseDto> Nominees { get; set; }
        public List<ShareholderSubscriptionDto> Subscriptions { get; set; }
    }
}