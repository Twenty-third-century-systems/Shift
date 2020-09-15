using System.Collections.Generic;
using BarTender.Models;

namespace BarTender.Dtos {
    public class ShareHoldingEntityDto {
        public int ApplicationId { get; set; }
        public string PvtEntityId { get; set; }
        public List<ShareHoldingEntity> Entities { get; set; }
    }
}