using System.Collections.Generic;

namespace Cabinet.Dtos.External.Request {
    public class NomineeBeneficiariesRequestDto {        
        public int ApplicationId { get; set; }
        public List<NomineeBeneficiaryRequestDto> ShareHolders { get; set; }
    }
}