namespace Cabinet.Dtos.External.Request {
    public class NewPrivateEntityOfficeRequestDto {
        public NewPrivateEntityAddressRequestDto Address { get; set; }
        public string MobileNumber { get; set; }
        public string TelephoneNumber { get; set; }
        public string EmailAddress { get; set; }
    }
}