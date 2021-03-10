namespace Cabinet.Dtos.External.Request {
    public class PrivateOfficeInformationRequestDto {
        public int ApplicationId { get; set; }
        public PrivateOfficeAddressRequestDto AddressInformation { get; set; }
    }
}