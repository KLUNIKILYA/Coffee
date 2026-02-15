using Coffee.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Data.Repositories
{
    public interface IEventRepository
    {
        Task<Event?> GetByIdAsync(int id);
        Task<Event?> GetByIdWithLecturerAsync(int id);
        Task<IEnumerable<Event>> GetAllAsync();
        Task<IEnumerable<Event>> GetUpcomingAsync(int count);
        Task<IEnumerable<Event>> GetByDateAsync(DateTime date);
        Task<IEnumerable<Event>> GetByLecturerIdAsync(int lecturerId);

        Task<IEnumerable<Event>> SearchAsync(string searchTerm);

        Task AddAsync(Event eventEntity);
        Task UpdateAsync(Event eventEntity);
        Task DeleteAsync(int id);

        Task<bool> ExistsAsync(int id);
        Task<int> GetTotalCountAsync();
    }
}
