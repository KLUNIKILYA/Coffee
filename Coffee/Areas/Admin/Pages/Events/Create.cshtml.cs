using Coffee.Core.DTOs;
using Coffee.Core.DTOs.Event;
using Coffee.Core.Interfaces;
using Coffee.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Coffee.Areas.Admin.Pages.Events
{
    [Area("Admin")]
    public class CreateModel : PageModel
    {
        private readonly IEventService _eventService;
        private readonly IFileService _fileService;
        private readonly ILecturerRepository _lecturerRepository;

        public CreateModel(IEventService eventService, IFileService fileService, ILecturerRepository lecturerRepository)
        {
            _eventService = eventService;
            _fileService = fileService;
            _lecturerRepository = lecturerRepository;
        }

        [BindProperty]
        public EventCreateVM Input { get; set; }

        public SelectList LecturerSl { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadLecturersAsync();

            Input = new EventCreateVM { StartDate = DateTime.Today.AddDays(1).AddHours(19) };
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadLecturersAsync();
                return Page();
            }

            string? imagePath = null;

            try
            {
                if (Input.UploadedCover != null)
                {
                    imagePath = await _fileService.SaveFileAsync(Input.UploadedCover, "events");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Input.UploadedCover", "Ошибка при загрузке файла: " + ex.Message);
                await LoadLecturersAsync();
                return Page();
            }

            var dto = new EventCreateDto
            {
                Title = Input.Title,
                Description = Input.Description,
                StartDate = Input.StartDate,
                Price = Input.Price,
                Capacity = Input.Capacity,
                LecturerId = Input.LecturerId,
                ImageUrl = imagePath
            };

            try
            {
                await _eventService.CreateEventAsync(dto);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Не удалось создать событие. Ошибка сервера.");
                await LoadLecturersAsync();
                return Page();
            }

            return RedirectToPage("/Index", new { area = "" });
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