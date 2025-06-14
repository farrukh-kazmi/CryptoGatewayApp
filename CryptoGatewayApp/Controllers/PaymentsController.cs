using CryptoGatewayApp.Data;
using CryptoGatewayApp.Models;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RestSharp;

namespace CryptoGatewayApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private const string ApiKey = "2YH5H3G-A3M4QMG-K579GHZ-BXT63J3";

        public PaymentsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost("create-payment")]
        public async Task<IActionResult> CreatePayment([FromBody] PaymentRequest requestModel)
        {
            var client = new RestClient("https://api.nowpayments.io/v1/payment");
            var request = new RestRequest("", Method.Post);
            request.AddHeader("x-api-key", ApiKey);
            request.AddHeader("Content-Type", "application/json");

            request.AddJsonBody(new
            {
                price_amount = requestModel.PriceAmount,
                price_currency = requestModel.PriceCurrency,
                pay_currency = requestModel.PayCurrency,
                order_id = requestModel.OrderId,
                order_description = requestModel.OrderDescription
            });

            var response = await client.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                return BadRequest(response.Content);
            }

            // Deserialize the response JSON
            var result = JsonConvert.DeserializeObject<NowPaymentResponse>(response.Content);

            // Create a new Payment record
            var payment = new Payment
            {
                PaymentId = result.payment_id,
                Status = result.payment_status,
                PayAddress = result.pay_address,
                PriceAmount = result.price_amount,
                PriceCurrency = result.price_currency,
                PayAmount = result.pay_amount,
                PayCurrency = result.pay_currency,
                OrderId = result.order_id,
                Description = result.order_description,
                CreatedAt = DateTime.UtcNow
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return Ok(payment);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllPayments()
        {
            var payments = await _context.Payments.ToListAsync();
            return Ok(payments);
        }

       
        [HttpPost("webhook")]
        public async Task<IActionResult> HandleWebhook([FromBody] PaymentResponse webhookData)
        {
            try
            {
                var payment = await _context.Payments
                    .FirstOrDefaultAsync(p => p.PaymentId == webhookData.PaymentId); // C# PascalCase

                if (payment == null)
                    return NotFound();

                // Update fields
                payment.Status = webhookData.PaymentStatus;
                payment.PayAmount = webhookData.PayAmount;
                payment.PayAddress = webhookData.PayAddress;
                payment.PriceAmount = webhookData.PriceAmount;
                payment.PriceCurrency = webhookData.PriceCurrency;
                payment.PayCurrency = webhookData.PayCurrency;
                payment.CreatedAt = webhookData.CreatedAt;

                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Webhook processing failed", error = ex.Message });
            }
        }

    }
}
