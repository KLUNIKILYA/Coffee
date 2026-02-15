using Coffee.Core.Entities;
using Coffee.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Data.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly ApplicationDbContext _context;

        public EventRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Event eventEntity)
        {
            await _context.Events.AddAsync(eventEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Events.FindAsync(id);

            if (entity != null)
            {
                entity.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }

        public Task<bool> ExistsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Event>> GetAllAsync()
        {
            return await _context.Events.Include(e => e.Lecturer).ToListAsync();
        }

        public Task<IEnumerable<Event>> GetByDateAsync(DateTime date)
        {
            throw new NotImplementedException();
        }

        public async Task<Event?> GetByIdAsync(int id)
        {
            return await _context.Events.Include(e => e.Lecturer).FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<Event?> GetByIdWithLecturerAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Event>> GetByLecturerIdAsync(int lecturerId)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetTotalCountAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Event>> GetUpcomingAsync(int count)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Event>> SearchAsync(string searchTerm)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Event eventEntity)
        {
            _context.Events.Update(eventEntity);
            await _context.SaveChangesAsync();
        }
    }
}
