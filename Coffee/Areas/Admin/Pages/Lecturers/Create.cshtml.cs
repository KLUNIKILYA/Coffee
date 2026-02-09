using Coffee.Core.Entities;
using Coffee.Data.Context;
using Coffee.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Coffee.Areas.Admin.Pages.Lecturers
{
    // [Authorize(Roles = "Admin")]
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
        public Lecturer Lecturer { get; set; } = default!;

        [BindProperty]
        public IFormFile? UploadedPhoto { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Lecturer.PhotoUrl");

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (UploadedPhoto != null)
            {
                Lecturer.PhotoUrl = await _fileService.SaveFileAsync(UploadedPhoto, "lecturers");
            }
            else
            {
                Lecturer.PhotoUrl = "https://via.placeholder.com/300";
            }

            _context.Lecturers.Add(Lecturer);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index", new { area = "" });
        }
    }
}