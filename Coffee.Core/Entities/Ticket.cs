using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Core.Entities
{
    public class Ticket
    {
        public int Id { get; set; }

        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = null!;

        public int EventId { get; set; }
        public Event Event { get; set; } = null!;

        public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;
        public bool IsPaid { get; set; }
        public string? PaymentId { get; set; }
    }
}
