using Coffee.Core.DTOs.LecturersDTO;
using Coffee.Core.Entities;
using Coffee.Core.Interfaces;
using Coffee.Core.Interfaces.Lecturer;
using Coffee.Data.Context;
using Coffee.ViewModels.LecturerVm;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Coffee.Areas.Admin.Pages.Lecturers
{
    // [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class CreateModel : PageModel
    {
        private readonly IFileService _fileService;
        private readonly ILecturerService _lecturerService;

        public CreateModel(ILecturerService lecturerService, IFileService fileService)
        {
            _fileService = fileService;
            _lecturerService = lecturerService;
        }

        [BindProperty]
        public LecturerCreateVM Input { get; set; } = default!;

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                string? photoPath = null;

                if (Input.UploadedPhoto != null)
                {
                    photoPath = await _fileService.SaveFileAsync(Input.UploadedPhoto, "lecturers");
                }

                var dto = new LecturerCreateDto
                {
                    FullName = Input.FullName,
                    Bio = Input.Bio,
                    YoutubeLink = Input.YoutubeLink,
                    PhotoUrl = photoPath
                };

                await _lecturerService.CreateLecturerAsync(dto);

                return RedirectToPage("/Index", new { area = "" });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Ошибка при создании лектора: " + ex.Message);
                return Page();
            }
        }
    }
}