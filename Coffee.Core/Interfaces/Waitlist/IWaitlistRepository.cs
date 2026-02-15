using Coffee.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Core.Interfaces.Waitlist
{
    public interface IWaitlistRepository
    {
        Task<IEnumerable<WaitlistEntry>> GetUserWaitlistAsync(string userId);
        Task DeleteAsync(int id);

        Task<bool> IsUserInWaitlistAsync(string userId, int eventId);
        Task AddAsync(WaitlistEntry entry);
    }
}
