using Coffee.Core.Interfaces;
using Coffee.Core.Interfaces.Lecturer;
using Coffee.ViewModels.LecturerVm;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Coffee.Areas.Admin.Pages.Lecturers
{
    public class IndexModel : PageModel
    {
        private readonly IFileService _fileService;
        private readonly ILecturerService _lecturerService;

        public IndexModel(IFileService fileService, ILecturerService lecturerService)
        {
            _fileService = fileService;
            _lecturerService = lecturerService;
        }

        public IList<LecturerIndexVM> Lecturers { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var dto = await _lecturerService.GetDtosAsync();
            Lecturers = dto.Select(x => new LecturerIndexVM
            {
                Id = x.Id,
                Bio = x.Bio,
                FullName = x.FullName,
                PhotoUrl = x.ImageUrl,
            }).ToList();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _lecturerService.DeleteLecturerAsync(id);
            return RedirectToPage();
        }
    }
}