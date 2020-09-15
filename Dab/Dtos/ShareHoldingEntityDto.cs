using System.Collections.Generic;
using Dab.Models;

namespace Dab.Dtos {
    public class ShareHoldingEntityDto {
        public int ApplicationId { get; set; }
        public string PvtEntityId { get; set; }
        public List<ShareHoldingEntity> Entities { get; set; }
    }
}