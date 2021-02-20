namespace Cabinet.Dtos.External.Response {
    public class SubmittedApplicationResponseDto {
        public int ApplicationId { get; set; }
        public string DateSubmitted { get; set; }
        public string Status { get; set; }
        public NameSearchResponseDto NameSearch { get; set; }
        public PrivateEntityResponseDto Entity { get; set; }
    }
}