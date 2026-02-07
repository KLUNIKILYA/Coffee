using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Core.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public int Capacity { get; set; }
        public decimal Price { get; set; }


        public int? LecturerId { get; set; }
        public Lecturer? Lecturer { get; set; }

        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

        public ICollection<WaitlistEntry> WaitlistEntries { get; set; } = new List<WaitlistEntry>();
    }
}
