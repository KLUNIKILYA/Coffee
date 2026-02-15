using Coffee.Core.Entities;
using Coffee.Core.Interfaces;
using Coffee.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Data.Repositories
{
    public class LecturerRepository : ILecturerRepository
    {
        private readonly ApplicationDbContext _context;
        public LecturerRepository(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task AddAsync(Lecturer lecturerEntity)
        {
            await _context.Lecturers.AddAsync(lecturerEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            _context.Lecturers.FirstOrDefault(x => x.Id == id).IsDeleted = true;
            await _context.SaveChangesAsync();
        }

        public Task<bool> ExistsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Lecturer>> GetAllAsync()
        {
            return await _context.Lecturers.Where(x => x.IsDeleted == false).ToListAsync();
        }

        public async Task<IEnumerable<Lecturer>> GetActiveLecturersAsync()
        {
            return await _context.Lecturers
                .Where(l => !l.IsDeleted)
                .OrderBy(l => l.FullName)
                .ToListAsync();
        }

        public Task<IEnumerable<Lecturer>> GetByDateAsync(DateTime date)
        {
            throw new NotImplementedException();
        }

        public async Task<Lecturer?> GetByIdAsync(int id)
        {
            return await _context.Lecturers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<int> GetTotalCountAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Lecturer>> GetUpcomingAsync(int count)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Lecturer lecturerEntity)
        {
            _context.Lecturers.Update(lecturerEntity);
            await _context.SaveChangesAsync();
        }
    }
}
