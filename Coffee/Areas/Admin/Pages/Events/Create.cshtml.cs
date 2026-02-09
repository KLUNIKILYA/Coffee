using Coffee.Core.Entities;
using Coffee.Core.Interfaces; // Подключаем интерфейс работы с файлами
using Coffee.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Coffee.Areas.Admin.Pages.Events
{
    [Area("Admin")]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileService _fileService; 

        public CreateModel(ApplicationDbContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        [BindProperty]
        public Event Event { get; set; } = default!;

        [BindProperty]
        public IFormFile? UploadedCover { get; set; } 

        public SelectList LecturerSl { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadLecturersAsync();
            Event = new Event { StartDate = DateTime.Today.AddDays(1).AddHours(19) };
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Event.ImageUrl");

            if (!ModelState.IsValid)
            {
                await LoadLecturersAsync();
                return Page();
            }

            if (UploadedCover != null)
            {
                Event.ImageUrl = await _fileService.SaveFileAsync(UploadedCover, "events");
            }
            else
            {
                Event.ImageUrl = "https://via.placeholder.com/600x400";
            }

            _context.Events.Add(Event);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index", new { area = "" });
        }

        private async Task LoadLecturersAsync()
        {
            var lecturers = await _context.Lecturers
                .Where(l => !l.IsDeleted)
                .Select(l => new { Id = l.Id, FullName = l.FullName })
                .ToListAsync();

            LecturerSl = new SelectList(lecturers, "Id", "FullName");
        }
    }
}