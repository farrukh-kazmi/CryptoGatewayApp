namespace CryptoGatewayApp.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public string PaymentId { get; set; }
        public string Status { get; set; }
        public string PayAddress { get; set; }
        public decimal PriceAmount { get; set; }
        public string PriceCurrency { get; set; }
        public decimal PayAmount { get; set; }
        public string PayCurrency { get; set; }
        public string OrderId { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
