using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Core.Entities
{
    public class WaitlistEntry
    {
        public int Id { get; set; }

        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = null!;

        public int EventId { get; set; }
        public Event Event { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsNotified { get; set; }
    }
}
