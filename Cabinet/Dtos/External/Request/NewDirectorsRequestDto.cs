using System.Collections.Generic;

namespace Cabinet.Dtos.External.Request {
    public class NewDirectorsRequestDto {
        public int ApplicationId { get; set; }
        public List<NewDirectorSecretaryRequestDto> Directors { get; set; }
    }
}