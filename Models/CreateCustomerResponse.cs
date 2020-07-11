using Newtonsoft.Json;
using Stripe;

public class CreateCustomerResponse
{
    [JsonProperty("customer")]
    public Customer Customer { get; set; }

    //[JsonProperty("paymentMethodId")]
    //public string PaymentMethod { get; set; }

    //[JsonProperty("invoiceId")]
    //public string Invoice { get; set; }
}