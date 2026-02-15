using Coffee.Core.DTOs;
using Coffee.Core.DTOs.Event;
using Coffee.Core.Entities;
using Coffee.Core.Interfaces;
using Coffee.Data.Repositories;

namespace Coffee.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IFileService _fileService;

        public EventService(IEventRepository eventRepository, IFileService fileService)
        {
            _eventRepository = eventRepository;
            _fileService = fileService;
        }

        public async Task DeleteEventAsync(int id)
        {
            var entity = await _eventRepository.GetByIdAsync(id);

            if (entity == null) return;

            if (!string.IsNullOrEmpty(entity.ImageUrl))
            {
                _fileService.DeleteFile(entity.ImageUrl);
            }

            await _eventRepository.DeleteAsync(id);
        }

        public async Task<List<EventIndexDto>> GetDtosAsync()
        {
            var entitys = await _eventRepository.GetAllAsync();
            var dtos = entitys.Select(l => new EventIndexDto
            {
                Id = l.Id,
                LecturerId = l.LecturerId ?? 0,
                Price = l.Price,
                PlacesTaken = l.PlacesTaken,
                Title = l.Title,
                StartDate = l.StartDate,
            }).ToList();

            return dtos;
        }

        public async Task<EventUpdateDto?> GetEventForUpdateAsync(int id)
        {
            var eventInDb = await _eventRepository.GetByIdAsync(id);
            if (eventInDb == null) return null;

            return new EventUpdateDto
            {
                Id = eventInDb.Id,
                Title = eventInDb.Title,
                Description = eventInDb.Description,
                StartDate = eventInDb.StartDate,
                Price = eventInDb.Price,
                Capacity = eventInDb.Capacity,
                LecturerId = eventInDb.LecturerId ?? 0
            };
        }

        public async Task UpdateEventAsync(EventUpdateDto dto)
        {
            var entity = await _eventRepository.GetByIdAsync(dto.Id);
            if (entity == null) throw new Exception("Not found");

            entity.Title = dto.Title;
            entity.Description = dto.Description;
            entity.StartDate = dto.StartDate;
            entity.Price = dto.Price;
            entity.Capacity = dto.Capacity;
            entity.LecturerId = dto.LecturerId;

            if (!string.IsNullOrEmpty(dto.NewImageUrl))
            {
                entity.ImageUrl = dto.NewImageUrl;
            }

            await _eventRepository.UpdateAsync(entity);
        }

        public async Task<int> CreateEventAsync(EventCreateDto dto)
        {
            var newEvent = new Event
            {
                Title = dto.Title,
                Description = dto.Description,
                StartDate = dto.StartDate,
                Price = dto.Price,
                Capacity = dto.Capacity,
                LecturerId = dto.LecturerId,
                ImageUrl = dto.ImageUrl,
                PlacesTaken = 0
            };

            await _eventRepository.AddAsync(newEvent);

            return newEvent.Id;
        }
    }
}
