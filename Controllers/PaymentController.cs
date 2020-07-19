using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ResearchTube.Areas.Identity.Data;
using ResearchTube.Data;
using Stripe;
using Stripe.Checkout;
using Newtonsoft.Json;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ResearchTube.Controllers
{
    public class PaymentController : Controller
    {
        // GET: /<controller>/
        private readonly ResearchTubeDbContext db;
        //private readonly StripeClient client;
        //public string StripePublishableKey = ;
        private readonly IOptions<StripeOptions> options;

        private readonly UserManager<ResearchTubeUser> userManager;
        public PaymentController(ResearchTubeDbContext db, IOptions<StripeOptions> options,
            UserManager<ResearchTubeUser> userManager)
        {
            this.db = db;
            this.options = options;
            this.userManager = userManager;
            StripeConfiguration.ApiKey = options.Value.StripeSecretKey;
        }

        //[HttpGet("public-keys")]
        //public ActionResult<PublicKeyResponse> GetPublicKey()
        //{
        //    return new PublicKeyResponse
        //    {
        //        PublicKey = this.options.Value.StripePublishableKey,
        //    };
        //}

        public IActionResult Plans()
        {
            
            return View();
        }

        public IActionResult PremiumPlanPage()
        {
            return View();
        }
        public IActionResult BasicPlanPage()
        {
            return View();
        }

        
        public IActionResult TestPage(string uid)
        {
            return View();
          
        }
        [HttpPost]
        public ActionResult Upgrade(string uid)
        {
            var paymentUser = db.Payments.First(a => a.UserId == uid);
            paymentUser.PlanType = "Pro";
            //_db.Payments.Update(paymentUser);
            db.SaveChanges();

            Console.WriteLine("You have successfully upgraded your account!");

            return View("Plans");
        }

        [HttpPost]
        public async Task<IActionResult> StripeCustIdAsync(string uid)
        {
            StripeConfiguration.ApiKey = "sk_test_51GtSONETdjG0caU1tLawSk3OAdAKzFHNlDZy0HEMUP4pluP8rFlyEM53LLCa2OyM1XioPKGf8klJcWIvtxwoHMih00HlWR2oUx";

            var user = await userManager.GetUserAsync(User);

            string userEmail = user.UserName;
            
            var options = new CustomerCreateOptions
            {
                Email = userEmail,
                Description = "New customer generated in PaymentController.cs",
            };
            var service = new CustomerService();
            service.Create(options);

            //might nee to use some HttpGet to obtain custId through Json, then pass it through database again.

            var paymentUser = db.Payments.First(a => a.UserId == uid);
            paymentUser.StripeUserId = "test";
            return View("Plans");
        }

        [HttpPost("create_customer_portal_session")]
        public Session StripePortal(string StripeCustId)
        {
            StripeConfiguration.ApiKey = "sk_test_51GtSONETdjG0caU1tLawSk3OAdAKzFHNlDZy0HEMUP4pluP8rFlyEM53LLCa2OyM1XioPKGf8klJcWIvtxwoHMih00HlWR2oUx";

            var options = new SessionCreateOptions
            {
                Customer = StripeCustId,
                SuccessUrl = "https://localhost:5001/Plans",
                CancelUrl = "https://localhost:5001/Home",
                PaymentMethodTypes = new List<string>{
                    "card",
                },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {   
                        Price = "price_1GuOq7ETdjG0caU1H8UFk6M9",
                        
                        Quantity=1,
                    },
                },
                Mode = "subscription",
            };
            var service = new SessionService();
            Session session = service.Create(options);
            return (session);
            
            //return View(session);
        }
        //[HttpGet("public-key")]

        //public ActionResult<PublicKeyResponse> GetPublishableKey()
        //{
        //    return new PublicKeyResponse
        //    {
        //        PublicKey = this.options.Value.StripePublishableKey,
        //    };
        //}

        //[HttpPost("create-customer")]
        //public ActionResult<CreateCustomerResponse> CreateCustomer([FromBody] CreateCustomerRequest req)
        //{
        //    var options = new CustomerCreateOptions
        //    {
        //        Email = req.Email,
        //    };
        //    var service = new CustomerService();
        //    var customer = service.Create(options);
        //    return new CreateCustomerResponse
        //    {
        //        Customer = customer,
        //    };
        //}

        //    var subscriptionService = new SubscriptionService(this.client);

        //    var subscription = await subscriptionService.CreateAsync(new SubscriptionCreateOptions
        //    {
        //        Items = new List<SubscriptionItemOptions>
        //        {
        //            new SubscriptionItemOptions
        //            {
        //                Price = this.options.Value.SubscriptionPriceId,
        //            },
        //        },
        //        Customer = customer.Id,
        //        Expand = new List<string>
        //        {
        //            "latest_invoice.payment_intent",
        //        }
        //    });
        //    return subscription;
        //}

        //[HttpPost("webhook")]

        //public async Task<IActionResult> ProcessWebhookEvent()
        //{
        //    var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

        //    try
        //    {
        //        var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], this.options.Value.StripeWebhookSecret);
        //        _logger.LogInformation($"Webhook event type: {stripeEvent.Type}");
        //        _logger.LogInformation(json);
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Exception occured while process webhook event.");
        //        return BadRequest();
        //    }
        //}

    }
}
