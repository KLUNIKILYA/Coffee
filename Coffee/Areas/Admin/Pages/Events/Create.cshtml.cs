using Coffee.Core.Entities;
using Coffee.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Coffee.Areas.Admin.Pages.Events
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
        public Event Event { get; set; } = default!;

        public IActionResult OnGet()
        {
            Event = new Event { StartDate = DateTime.Today.AddDays(1).AddHours(10) };
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Events.Add(Event);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index", new { area = "" });
        }
    }
}
