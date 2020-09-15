using System.Collections.Generic;
using BarTender.Models;

namespace BarTender.Dtos {
    public class ShareHoldingPersonDto {
        public int ApplicationId { get; set; }
        public string PvtEntityId { get; set; }
        public List<ShareHoldingMember> People { get; set; }
    }
}