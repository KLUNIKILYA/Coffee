using Coffee.Core.Entities;
using Coffee.Core.Interfaces; // Добавь этот using
using Coffee.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Coffee.Areas.Admin.Pages.Lecturers
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileService _fileService;

        public IndexModel(ApplicationDbContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        public IList<Lecturer> Lecturers { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Lecturers = await _context.Lecturers.Where(l => !l.IsDeleted).ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var lecturer = await _context.Lecturers.FindAsync(id);

            if (lecturer != null)
            {
                if (!string.IsNullOrEmpty(lecturer.PhotoUrl))
                {
                    _fileService.DeleteFile(lecturer.PhotoUrl);
                }

                lecturer.IsDeleted = true;
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}