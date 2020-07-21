namespace ResearchTube.Models
{
    public class PaymentType
    {
        public string PaymentTypeId { get; set; }
        public string AccessLevel { get; set; }

        public int CurrentPaymentId { get; set; }
        public Payment Payment { get; set; }
    }
}