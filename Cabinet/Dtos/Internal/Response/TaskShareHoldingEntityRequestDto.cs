using System.Collections.Generic;
using Cabinet.Dtos.External.Request;

namespace Cabinet.Dtos.Internal.Response {
    public class TaskShareHoldingEntityRequestDto {
        public TaskShareHoldingEntityRequestDto()
        {
            Subscriptions = new List<TaskPrivateEntityShareholderSubscriptionResponseDto>();
        }
        public string Name { get; set; }
        public string Reference { get; set; }

        public List<TaskPrivateEntityShareholderSubscriptionResponseDto> Subscriptions { get; set; }
    }
}