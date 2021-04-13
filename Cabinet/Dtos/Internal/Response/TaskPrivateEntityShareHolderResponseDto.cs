using System;
using System.Collections.Generic;

namespace Cabinet.Dtos.Internal.Response {
    public class TaskPrivateEntityShareHolderResponseDto : TaskPrivateEntityPersonResponseDto {
        public TaskPrivateEntityShareHolderResponseDto()
        {
            Beneficiaries = new List<TaskPrivateEntityShareHolderResponseDto>();
            Subscriptions = new List<TaskPrivateEntityShareholderSubscriptionResponseDto>();
            RepresentedEntities = new List<TaskShareHoldingEntityRequestDto>();
        }

        public List<TaskPrivateEntityShareHolderResponseDto> Beneficiaries { get; set; }
        public List<TaskShareHoldingEntityRequestDto> RepresentedEntities { get; set; }
        public List<TaskPrivateEntityShareholderSubscriptionResponseDto> Subscriptions { get; set; }
    }
}