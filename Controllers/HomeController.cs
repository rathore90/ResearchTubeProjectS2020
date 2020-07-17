using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ResearchTube.Models;
using Stripe;
using Microsoft.Extensions.Options;

namespace ResearchTube.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly StripeClient client;
        private readonly IOptions<StripeOptions> options;

        public HomeController(ILogger<HomeController> logger, IOptions<StripeOptions> options)
        {
            this.options = options;
            this.client = new StripeClient(options.Value.StripeSecretKey);
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Plans()
        {
            return View();
        }

        [HttpGet("public-key")]

        public ActionResult<PublicKeyResponse> GetPublishableKey()
        {
            return new PublicKeyResponse
            {
                PublicKey = this.options.Value.StripePublishableKey,
            };
        }

        [HttpPost("create-customer")]

        public async Task<ActionResult<Subscription>> CreateCustomerAsync([FromBody] CustomerCreateRequest request)
        {
            var customerService = new CustomerService(this.client);

            var customer = await customerService.CreateAsync(new CustomerCreateOptions
            {
                Email = request.Email,
                PaymentMethod = request.PaymentMethod,
                InvoiceSettings = new CustomerInvoiceSettingsOptions
                {
                    DefaultPaymentMethod = request.PaymentMethod,
                }
            });

            var subscriptionService = new SubscriptionService(this.client);

            var subscription = await subscriptionService.CreateAsync(new SubscriptionCreateOptions
            {
                Items = new List<SubscriptionItemOptions>
                {
                    new SubscriptionItemOptions
                    {
                        Price = this.options.Value.SubscriptionPriceId,
                    },
                },
                Customer = customer.Id,
                Expand = new List<string>
                {
                    "latest_invoice.payment_intent",
                }
            });
            return subscription;
        }

        [HttpPost("webhook")]

        public async Task<IActionResult> ProcessWebhookEvent()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            try
            {
                var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], this.options.Value.StripeWebhookSecret);
                _logger.LogInformation($"Webhook event type: {stripeEvent.Type}");
                _logger.LogInformation(json);
                return Ok();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Exception occured while process webhook event.");
                return BadRequest();
            }
        }
       
       
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
