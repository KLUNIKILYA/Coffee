using Coffee.Core.DTOs;
using Coffee.Core.DTOs.LecturersDTO;

namespace Coffee.Core.Interfaces.Lecturer
{
    public interface ILecturerService
    {
        Task UpdateLecturerAsync(LecturerUpdateDto model);
        Task<LecturerUpdateDto?> GetLecturerForUpdateAsync(int id);
        Task<List<LecturersIndexDto>> GetDtosAsync();
        Task DeleteLecturerAsync(int id);
        Task<int> CreateLecturerAsync(LecturerCreateDto model);
    }
}
