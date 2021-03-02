using System.Collections.Generic;

namespace Cabinet.Dtos.External.Request {
    public class NewNameSearchRequestDto {
        public int ServiceId { get; set; }
        public string Justification { get; set; }
        public int DesignationId { get; set; }
        public int ReasonForSearchId { get; set; }
        public string MainObject { get; set; }
        public int SortingOffice { get; set; }
        public List<SuggestedEntityNameRequestDto> Names { get; set; }
    }
}