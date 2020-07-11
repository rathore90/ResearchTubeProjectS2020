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
        public ActionResult<CreateCustomerResponse> CreateCustomer([FromBody] CustomerCreateRequest req)
        {
            var options = new CustomerCreateOptions
            {
                Email = req.Email,
            };
            var service = new CustomerService();
            var customer = service.Create(options);

            var a = new CreateCustomerResponse
            {
                Customer = customer,
            };
            Console.WriteLine(a);
            return new CreateCustomerResponse
            {
                Customer = customer,
            };
        }

        [HttpPost("create-subscription")]
        public ActionResult<Subscription> CreateSubscription([FromBody] CreateSubscriptionRequest req)
        {
            // Attach payment method
            var options = new PaymentMethodAttachOptions
            {
                Customer = req.Customer,
            };
            var service = new PaymentMethodService();
            var paymentMethod = service.Attach(req.PaymentMethod, options);

            // Update customer's default invoice payment method
            var customerOptions = new CustomerUpdateOptions
            {
                InvoiceSettings = new CustomerInvoiceSettingsOptions
                {
                    DefaultPaymentMethod = paymentMethod.Id,
                },
            };
            var customerService = new CustomerService();
            customerService.Update(req.Customer, customerOptions);

            // Create subscription
            var subscriptionOptions = new SubscriptionCreateOptions
            {
                Customer = req.Customer,
                Items = new List<SubscriptionItemOptions>
        {
            new SubscriptionItemOptions
            {
                Price = Environment.GetEnvironmentVariable(req.Price),
            },
        },
            };
            subscriptionOptions.AddExpand("latest_invoice.payment_intent");
            var subscriptionService = new SubscriptionService();
            try
            {
                Subscription subscription = subscriptionService.Create(subscriptionOptions);
                return subscription;
            }
            catch (StripeException e)
            {
                Console.WriteLine($"Failed to create subscription.{e}");
                return BadRequest();
            }
        }

        [HttpPost("retry-invoice")]
        public ActionResult<Invoice> RetryInvoice([FromBody] RetryInvoiceRequest req)
        {
            // Attach payment method
            var options = new PaymentMethodAttachOptions
            {
                Customer = req.Customer,
            };
            var service = new PaymentMethodService();
            var paymentMethod = service.Attach(req.PaymentMethod, options);

            // Update customer's default invoice payment method
            var customerOptions = new CustomerUpdateOptions
            {
                InvoiceSettings = new CustomerInvoiceSettingsOptions
                {
                    DefaultPaymentMethod = paymentMethod.Id,
                },
            };
            var customerService = new CustomerService();
            customerService.Update(req.Customer, customerOptions);

            var invoiceOptions = new InvoiceGetOptions();
            invoiceOptions.AddExpand("payment_intent");
            var invoiceService = new InvoiceService();
            Invoice invoice = invoiceService.Get(req.Invoice, invoiceOptions);
            return invoice;
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occured while process webhook event.");
                return BadRequest();
            }
        }
        public IActionResult PremiumPlanPage()
        {
            return View();
        }
        public IActionResult BasicPlanPage()
        {
            return View();
        }
        public IActionResult FreePlanPage()
        {
            // Set your secret key. Remember to switch to your live secret key in production!
            // See your keys here: https://dashboard.stripe.com/account/apikeys
            StripeConfiguration.ApiKey = "sk_test_51GtSONETdjG0caU1tLawSk3OAdAKzFHNlDZy0HEMUP4pluP8rFlyEM53LLCa2OyM1XioPKGf8klJcWIvtxwoHMih00HlWR2oUx";

            var options = new PaymentIntentCreateOptions
            {
                Amount = 1000,
                Currency = "cad",
                PaymentMethodTypes = new List<string>
        {
            "card",
        },
                ReceiptEmail = "jenny.rosen@example.com",
            };

            var service = new PaymentIntentService();
            var paymentIntent = service.Create(options);

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
