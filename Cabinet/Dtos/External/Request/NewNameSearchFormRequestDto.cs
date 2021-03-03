namespace Cabinet.Dtos.External.Request {
    public class NewNameSearchFormRequestDto {
        public int ServiceId { get; set; }
        public string Justification { get; set; }
        public int DesignationId { get; set; }
        public int ReasonForSearchId { get; set; }
        public string MainObject { get; set; }
        public int SortingOffice { get; set; }
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public string Name3 { get; set; }
        public string Name4 { get; set; }
        public string Name5 { get; set; }
    }
}