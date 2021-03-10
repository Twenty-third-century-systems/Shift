namespace Cabinet.Dtos.External.Request {
    public class NewSecretaryRequestDto {
        public int ApplicationId { get; set; }
        public NewDirectorSecretaryRequestDto Secretary { get; set; }
    }
}