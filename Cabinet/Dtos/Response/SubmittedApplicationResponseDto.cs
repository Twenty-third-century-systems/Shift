namespace Cabinet.Dtos.Response {
    public class SubmittedApplicationRequestDto {
        public int Id { get; set; }
        public string Service { get; set; }
        public string DateSubmitted { get; set; }
        public string Status { get; set; }
        public int? NameSearch { get; set; }
        public string Reference { get; set; }
    }
}