using Coffee.Core.Entities;
using Coffee.Core.Interfaces; 
using Coffee.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Coffee.Areas.Admin.Pages.Events
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

        public IList<Event> Events { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Events = await _context.Events
                .Include(e => e.Lecturer)
                .OrderByDescending(e => e.StartDate)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var eventEntity = await _context.Events.FindAsync(id);

            if (eventEntity == null)
            {
                return NotFound();
            }

            var hasTickets = await _context.Tickets.AnyAsync(t => t.EventId == id);

            if (hasTickets)
            {
                TempData["ErrorMessage"] = "Нельзя удалить это мероприятие, так как на него уже куплены билеты! Вместо удаления отредактируйте его или отмените.";
                return RedirectToPage();
            }

            if (!string.IsNullOrEmpty(eventEntity.ImageUrl))
            {
                _fileService.DeleteFile(eventEntity.ImageUrl);
            }

            _context.Events.Remove(eventEntity);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Мероприятие успешно удалено.";
            return RedirectToPage();
        }
    }
}