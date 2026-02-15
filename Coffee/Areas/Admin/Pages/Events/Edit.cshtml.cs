using Coffee.Core.DTOs;
using Coffee.Core.Entities;
using Coffee.Core.Interfaces;
using Coffee.Data.Repositories;
using Coffee.Services;
using Coffee.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering; 
using Microsoft.EntityFrameworkCore;

namespace Coffee.Areas.Admin.Pages.Events
{
    public class EditModel : PageModel
    {
        private readonly ILecturerRepository _lecturerRepository;
        private readonly IFileService _fileService;
        private readonly IEventService _eventService;

        public EditModel(ILecturerRepository lecturerRepository, IFileService fileService, IEventService eventService)
        {
            _lecturerRepository = lecturerRepository;
            _fileService = fileService;
            _eventService = eventService;
        }

        [BindProperty]
        public EventEditVM Input { get; set; } = default!;

        public SelectList LecturerSl { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            var eventInDb = await _eventService.GetEventForUpdateAsync(id.Value);

            if (eventInDb == null) return NotFound();

            Input = new EventEditVM
            {
                Id = eventInDb.Id,
                Title = eventInDb.Title,
                Description = eventInDb.Description,
                StartDate = eventInDb.StartDate,
                Price = eventInDb.Price,
                Capacity = eventInDb.Capacity,
                LecturerId = eventInDb.LecturerId,
            };

            await LoadLecturersAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadLecturersAsync();
                return Page();
            }

            try
            {
                string? uploadedPath = null;
                if (Input.NewUploadedCover != null)
                {
                    uploadedPath = await _fileService.SaveFileAsync(Input.NewUploadedCover, "events");
                }

                var dto = new EventUpdateDto
                {
                    Id = Input.Id,
                    Title = Input.Title,
                    Description = Input.Description,
                    StartDate = Input.StartDate,
                    Price = Input.Price,
                    Capacity = Input.Capacity,
                    LecturerId = Input.LecturerId,
                    NewImageUrl = uploadedPath
                };

                await _eventService.UpdateEventAsync(dto);

                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Ошибка при обновлении: " + ex.Message);
                await LoadLecturersAsync();
                return Page();
            }
        }

        private async Task LoadLecturersAsync()
        {
            var lecturers = await _lecturerRepository.GetActiveLecturersAsync();

            if (lecturers == null) lecturers = new List<Core.Entities.Lecturer>();

            var items = lecturers.Select(l => new {
                Id = l.Id,
                Name = l.FullName,
            });

            LecturerSl = new SelectList(items, "Id", "Name");
        }
    }
}