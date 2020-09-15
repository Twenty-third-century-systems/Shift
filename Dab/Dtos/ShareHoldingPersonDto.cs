using System.Collections.Generic;
using Dab.Models;

namespace Dab.Dtos {
    public class ShareHoldingPersonDto {
        public int ApplicationId { get; set; }
        public string PvtEntityId { get; set; }
        public List<ShareHoldingMember> People { get; set; }
    }
}