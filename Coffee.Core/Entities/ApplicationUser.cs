using Microsoft.AspNetCore.Identity;

namespace Coffee.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
        public ICollection<WaitlistEntry> WaitlistEntries { get; set; } = new List<WaitlistEntry>();
    }
}
