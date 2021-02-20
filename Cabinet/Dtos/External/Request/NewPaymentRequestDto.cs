namespace Cabinet.Dtos.External.Request {
    public class NewPaymentRequestDto {        
        public string Email { get; set; }
        public int WalletProvider { get; set; }
        public string PhoneNumber { get; set; }
        public double Amount { get; set; }
    }
}