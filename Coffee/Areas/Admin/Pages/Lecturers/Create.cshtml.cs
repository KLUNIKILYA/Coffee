using Coffee.Core.Entities;
using Coffee.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Coffee.Areas.Admin.Pages.Lecturers
{
    // [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Lecturer Lecturer { get; set; } = default!;

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

            _context.Lecturers.Add(Lecturer);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index", new { area = "" });
        }
    }
}
