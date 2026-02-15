namespace Coffee.Core.Interfaces
{
    public interface ILecturerRepository
    {
        Task<Entities.Lecturer?> GetByIdAsync(int id);
        Task<IEnumerable<Entities.Lecturer>> GetAllAsync();
        Task<IEnumerable<Entities.Lecturer>> GetUpcomingAsync(int count);
        Task<IEnumerable<Entities.Lecturer>> GetByDateAsync(DateTime date);
        Task<IEnumerable<Entities.Lecturer>> GetActiveLecturersAsync();



        Task AddAsync(Entities.Lecturer lecturerEntity);
        Task UpdateAsync(Entities.Lecturer lecturerEntity);
        Task DeleteAsync(int id);

        Task<bool> ExistsAsync(int id);
        Task<int> GetTotalCountAsync();
    }
}
