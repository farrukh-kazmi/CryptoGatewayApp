namespace CryptoGatewayApp.Models
{
    public class NowPaymentResponse
    {
        public string payment_id { get; set; }
        public string payment_status { get; set; }
        public string pay_address { get; set; }
        public decimal price_amount { get; set; }
        public string price_currency { get; set; }
        public decimal pay_amount { get; set; }
        public string pay_currency { get; set; }
        public string order_id { get; set; }
        public string order_description { get; set; }
    }
}
