using Coffee.Core.Entities;
using Coffee.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering; 
using Microsoft.EntityFrameworkCore;

namespace Coffee.Areas.Admin.Pages.Events
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Event Event { get; set; } = default!;

        public SelectList LecturerSl { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            Event = await _context.Events.FindAsync(id);
            if (Event == null) return NotFound();

            var lecturers = await _context.Lecturers
                .Select(l => new { Id = l.Id, FullName = l.FullName })
                .ToListAsync();

            LecturerSl = new SelectList(lecturers, "Id", "FullName");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var lecturers = await _context.Lecturers
                     .Select(l => new { Id = l.Id, FullName = l.FullName })
                     .ToListAsync();
                LecturerSl = new SelectList(lecturers, "Id", "FullName");
                return Page();
            }

            _context.Attach(Event).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Events.Any(e => e.Id == Event.Id)) return NotFound();
                else throw;
            }

            return RedirectToPage("./Index");
        }
    }
}