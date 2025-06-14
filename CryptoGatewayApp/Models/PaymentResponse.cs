using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace CryptoGatewayApp.Models
{
    public class PaymentResponse
    {
        [JsonPropertyName("payment_id")]
        public string PaymentId { get; set; }

        [JsonPropertyName("payment_status")]
        public string PaymentStatus { get; set; }

        [JsonPropertyName("pay_address")]
        public string PayAddress { get; set; }

        [JsonPropertyName("price_amount")]
        public decimal PriceAmount { get; set; }

        [JsonPropertyName("price_currency")]
        public string PriceCurrency { get; set; }

        [JsonPropertyName("pay_amount")]
        public decimal PayAmount { get; set; }

        [JsonPropertyName("pay_currency")]
        public string PayCurrency { get; set; }

        [JsonPropertyName("order_id")]
        public string OrderId { get; set; }

        [JsonPropertyName("order_description")]
        public string Description { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}
