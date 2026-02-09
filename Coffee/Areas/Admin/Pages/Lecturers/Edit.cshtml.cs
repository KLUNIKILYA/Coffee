using Coffee.Core.Entities;
using Coffee.Core.Interfaces;
using Coffee.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Coffee.Areas.Admin.Pages.Lecturers
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileService _fileService;

        public EditModel(ApplicationDbContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        [BindProperty]
        public Lecturer Lecturer { get; set; } = default!;

        [BindProperty]
        public IFormFile? UploadedPhoto { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            Lecturer = await _context.Lecturers.FindAsync(id);

            if (Lecturer == null) return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("UploadedPhoto");
            ModelState.Remove("Lecturer.PhotoUrl");

            if (!ModelState.IsValid) return Page();

            var lecturerInDb = await _context.Lecturers.FindAsync(Lecturer.Id);
            if (lecturerInDb == null) return NotFound();

            // 2. Обновляем текстовые поля
            lecturerInDb.FullName = Lecturer.FullName;
            lecturerInDb.Bio = Lecturer.Bio;
            lecturerInDb.YoutubeLink = Lecturer.YoutubeLink;

            if (UploadedPhoto != null)
            {
                if (!string.IsNullOrEmpty(lecturerInDb.PhotoUrl))
                {
                    _fileService.DeleteFile(lecturerInDb.PhotoUrl);
                }

                lecturerInDb.PhotoUrl = await _fileService.SaveFileAsync(UploadedPhoto, "lecturers");
            }

            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}