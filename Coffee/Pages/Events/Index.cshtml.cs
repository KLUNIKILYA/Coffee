using Coffee.Core.Entities;
using Coffee.Data.Context;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Coffee.Pages.Events
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Event> Events { get; set; } = new List<Event>();

        public HashSet<DateTime> EventDates { get; set; } = new HashSet<DateTime>();

        public async Task OnGetAsync()
        {
            var today = DateTime.Now.Date;
            var endDate = today.AddDays(90);

            Events = await _context.Events
                .Where(e => e.StartDate >= today && e.StartDate <= endDate)
                .OrderBy(e => e.StartDate)
                .ToListAsync();

            EventDates = Events
                .Select(e => e.StartDate.Date)
                .ToHashSet();
        }
    }
}