namespace CryptoGatewayApp.Models
{
    public class PaymentRequest
    {
        public decimal PriceAmount { get; set; }
        public string PriceCurrency { get; set; }
        public string PayCurrency { get; set; }
        public string OrderId { get; set; }
        public string OrderDescription { get; set; }
    }
}
