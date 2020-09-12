using Dab.Models;

namespace Dab.Dtos {
    public class OfficeInformationDto {
        public int ApplicationId { get; set; }
        public string PvtEntityId { get; set; }
        public Office Office { get; set; }
    }
}