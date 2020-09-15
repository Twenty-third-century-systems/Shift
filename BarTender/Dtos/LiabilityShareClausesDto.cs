using BarTender.Models;

namespace BarTender.Dtos {
    public class LiabilityShareClausesDto {
        public int ApplicationId { get; set; }
        public string PvtEntityId { get; set; }
        public LiabilityShareClauses Clauses { get; set; }
    }
}