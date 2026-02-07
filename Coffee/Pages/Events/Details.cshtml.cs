using Coffee.Core.Entities;
using Coffee.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Coffee.Pages.Events
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context; 

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Event Event { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Event = await _context.Events
                                  //.Include(e => e.Lecturer) // Если есть связь с лектором
                                  .FirstOrDefaultAsync(e => e.Id == id);

            if (Event == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
