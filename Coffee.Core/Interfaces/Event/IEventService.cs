using Coffee.Core.DTOs;
using Coffee.Core.DTOs.Event;

namespace Coffee.Core.Interfaces
{
    public interface IEventService
    {
        Task UpdateEventAsync(EventUpdateDto model);
        Task<EventUpdateDto?> GetEventForUpdateAsync(int id);
        Task<List<EventIndexDto>> GetDtosAsync();
        Task DeleteEventAsync(int id);
        Task<int> CreateEventAsync(EventCreateDto dto);
    }
}
