using Coffee.Core.Entities;

namespace Coffee.Core.Interfaces.Tickets
{
    public interface ITicketRepository
    {
        Task<IEnumerable<Ticket>> GetByUserIdAsync(string userId);
    }
}
