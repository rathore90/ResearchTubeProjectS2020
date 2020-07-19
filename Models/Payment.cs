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

        [Required]
        public string UserId { get; set; }
        public string PlanType { get; set; }
#nullable enable
        public string? StripeUserId { get; set; }
        public string? PaymentMethodId { get; set; }
        public string? subscriptionId { get; set; }

        [DataType(DataType.Date)]
        public DateTime? current_period_start { get; set; }
        [DataType(DataType.Date)]
        public DateTime? current_period_end { get; set; }
        public int? Last4 { get; set; }

        public Payment()
        {
            //this.PaymentId
            this.PlanType = "free";
        }
    }
}
