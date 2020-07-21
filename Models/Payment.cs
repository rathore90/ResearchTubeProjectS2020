using ResearchTube.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ResearchTube.Models
{
    [Table("Payment")]
    public class Payment
    {
#nullable disable
        [Key]
        public int PaymentId { get; set; }
        public ResearchTubeUser Users { get; set; }
        public PaymentType PlanType { get; set; }
        public List<PaymentType> PaymentTypes { get; set; }
        public string ResearchTubeUserId { get; set; }
#nullable enable
        public string? StripeUserId { get; set; }
        public string? PaymentMethodId { get; set; }
        public string? SubscriptionId { get; set; }

        [DataType(DataType.Date)]
        public DateTime? CurrenPeriodStart { get; set; }
        [DataType(DataType.Date)]
        public DateTime? CurrentPeriodEnd { get; set; }
        public int? Last4 { get; set; }
    }
}
