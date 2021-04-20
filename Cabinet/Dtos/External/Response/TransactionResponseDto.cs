namespace Cabinet.Dtos.External.Response {
    public class TransactionResponseDto {
        public string Description { get; set; }
        public string Date { get; set; }
        public string PayNowReference { get; set; }
        public double CreditAmount { get; set; }
        public double DebitAmount { get; set; }
    }
}