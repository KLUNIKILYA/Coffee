using Coffee.Core.DTOs;
using Coffee.Core.DTOs.LecturersDTO;
using Coffee.Core.Entities;
using Coffee.Core.Interfaces;
using Coffee.Core.Interfaces.Lecturer;
using System.Runtime.Serialization.Formatters;

namespace Coffee.Core.Services
{
    public class LecturerService : ILecturerService
    {
        private readonly ILecturerRepository _lecturerRepository; 
        private readonly IFileService _fileService;

        public LecturerService(ILecturerRepository lecturerRepository, IFileService fileService) 
        {
            _lecturerRepository = lecturerRepository;
            _fileService = fileService;
        }

        public async Task<int> CreateLecturerAsync(LecturerCreateDto model)
        {
            var lecturer = new Lecturer
            {
                FullName = model.FullName,
                Bio = model.Bio,
                YoutubeLink = model.YoutubeLink,
                PhotoUrl = model.PhotoUrl,
            };

            _lecturerRepository.AddAsync(lecturer);
            return lecturer.Id;
        }

        public async Task DeleteLecturerAsync(int id)
        {
            var entity = await _lecturerRepository.GetByIdAsync(id);
            if (entity == null) return;

            if(!string.IsNullOrEmpty(entity.PhotoUrl))
            {
                _fileService.DeleteFile(entity.PhotoUrl); 
            }

            await _lecturerRepository.DeleteAsync(id);
        }

        public async Task<List<LecturersIndexDto>> GetDtosAsync()
        {
            var entitys = await _lecturerRepository.GetAllAsync();

            var dto = entitys.Select(x => new LecturersIndexDto
            {
                Id = x.Id,
                Bio = x.Bio,
                FullName = x.FullName,
                ImageUrl = x.PhotoUrl
            }).ToList();

            return dto;
        }

        public async Task<LecturerUpdateDto?> GetLecturerForUpdateAsync(int id)
        {
            var entity = await _lecturerRepository.GetByIdAsync(id);

            var dto = new LecturerUpdateDto
            {
                FullName = entity.FullName,
                Bio = entity.Bio,
                YoutubeLink = entity.YoutubeLink,
                PhotoUrl = entity.PhotoUrl,
            };
            return dto;
        }

        public async Task UpdateLecturerAsync(LecturerUpdateDto dto)
        {
            var entity = await _lecturerRepository.GetByIdAsync(dto.Id);
            if (entity == null) { throw new Exception("Lecturer was no found"); }

            entity.FullName = dto.FullName;
            entity.Bio = dto.Bio;
            entity.YoutubeLink = dto.YoutubeLink;

            if(!string.IsNullOrWhiteSpace(dto.PhotoUrl))
            {
                if(!string.IsNullOrEmpty(entity.PhotoUrl))
                {
                    _fileService.DeleteFile(entity.PhotoUrl);
                }
                entity.PhotoUrl = dto.PhotoUrl;
            }

            await _lecturerRepository.UpdateAsync(entity);
        }
    }
}
