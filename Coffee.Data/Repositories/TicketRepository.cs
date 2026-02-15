using Coffee.Core.Entities;
using Coffee.Core.Interfaces.Tickets;
using Coffee.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Data.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly ApplicationDbContext _context;
        public TicketRepository(ApplicationDbContext applicationDbContext) 
        {
            _context = applicationDbContext;
        }
        public async Task<IEnumerable<Ticket>> GetByUserIdAsync(string userId)
        {
            return await _context.Tickets
                .Include(t => t.Event)
                .Where(t => t.UserId == userId)
                .OrderByDescending(t => t.Event.StartDate)
                .ToListAsync();
        }
    }
}
