using Coffee.Core.Entities;
using Coffee.Core.Interfaces.Waitlist;
using Coffee.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Coffee.Data.Repositories
{
    public class WaitlistRepository : IWaitlistRepository
    {
        private readonly ApplicationDbContext _context;

        public WaitlistRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WaitlistEntry>> GetUserWaitlistAsync(string userId)
        {
            return await _context.Waitlist
                .Include(w => w.Event)
                .ThenInclude(e => e.Lecturer)
                .Where(w => w.UserId == userId)
                .OrderByDescending(w => w.CreatedAt)
                .ToListAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entry = await _context.Waitlist.FindAsync(id);
            if (entry != null)
            {
                _context.Waitlist.Remove(entry);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> IsUserInWaitlistAsync(string userId, int eventId)
        {
            return await _context.Waitlist.AnyAsync(w => w.UserId == userId && w.EventId == eventId);
        }

        public async Task AddAsync(WaitlistEntry entry)
        {
            await _context.Waitlist.AddAsync(entry);
            await _context.SaveChangesAsync();
        }
    }
}
