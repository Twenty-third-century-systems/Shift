namespace Cabinet.Dtos.External.Request {
    public class NewPrivateEntityOfficeRequestDto {
        public int ApplicationId { get; set; }
        public NewPrivateEntityAddressRequestDto Address { get; set; }
        public string MobileNumber { get; set; }
        public string TelephoneNumber { get; set; }
        public string EmailAddress { get; set; }
    }
}