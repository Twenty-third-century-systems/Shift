using BarTender.Models;

namespace BarTender.Dtos {
    public class OfficeInformationDto {
        public int ApplicationId { get; set; }
        public string PvtEntityId { get; set; }
        public Office Office { get; set; }
    }
}