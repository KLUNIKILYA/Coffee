using Coffee.Core.DTOs.LecturersDTO;
using Coffee.Core.Entities;
using Coffee.Core.Interfaces;
using Coffee.Core.Interfaces.Lecturer;
using Coffee.Data.Context;
using Coffee.Services;
using Coffee.ViewModels;
using Coffee.ViewModels.LecturerVm;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Coffee.Areas.Admin.Pages.Lecturers
{
    public class EditModel : PageModel
    {
        private readonly IFileService _fileService;
        private readonly ILecturerService _lecturerService;

        public EditModel(ILecturerService lecturerService, IFileService fileService)
        {
            _lecturerService = lecturerService;
            _fileService = fileService;
        }

        [BindProperty]
        public LecturerEditVM Input { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            var lecturerInDb = await _lecturerService.GetLecturerForUpdateAsync(id.Value);

            if (lecturerInDb == null) return NotFound();

            Input = new LecturerEditVM
            {
                Id = id.Value,
                FullName = lecturerInDb.FullName,
                Bio = lecturerInDb.Bio,
                YoutubeLink = lecturerInDb.YoutubeLink,
                ExistingPhotoUrl = lecturerInDb.PhotoUrl
            };

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
                string? uploadedPath = null;
                if (Input.UploadedPhoto != null)
                {
                    uploadedPath = await _fileService.SaveFileAsync(Input.UploadedPhoto, "lecturers");
                }

                var dto = new LecturerUpdateDto
                {
                    Id = Input.Id,
                    FullName = Input.FullName,
                    Bio = Input.Bio,
                    YoutubeLink = Input.YoutubeLink,
                    PhotoUrl = uploadedPath
                };

                await _lecturerService.UpdateLecturerAsync(dto);
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Ошибка: " + ex.Message);
                return Page();
            }
        }
    }
}