namespace MoneyCounter.Dtos {
    public class PaymentFromClientDto {
        public int PaynowRef { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
    }    
}