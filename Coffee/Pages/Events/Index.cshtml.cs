using Coffee.Core.Entities;
using Coffee.Data.Context;
using Microsoft.AspNetCore.Mvc;
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
        public IList<Lecturer> TopLecturers { get; set; } = new List<Lecturer>();

        public async Task OnGetAsync()
        {
            Events = await _context.Events
                .Where(e => e.StartDate >= DateTime.Now)
                .OrderBy(e => e.StartDate)
                .Take(5)
                .ToListAsync();

            TopLecturers = await _context.Lecturers
                .Take(6)
                .ToListAsync();
        }


        public class EventViewModel
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime StartDate { get; set; }
            public decimal Price { get; set; }
            public int Capacity { get; set; }
            public int PlacesTaken { get; set; }

            public int PlacesLeft => Capacity - PlacesTaken;
            public int FillPercentage => Capacity > 0 ? (int)((double)PlacesTaken / Capacity * 100) : 100;

            // Логика статуса
            public (string Text, string ColorClass) Status
            {
                get
                {
                    if (PlacesLeft <= 0) return ("Sold Out", "bg-danger");
                    if (PlacesLeft <= 5) return ($"Осталось {PlacesLeft}", "bg-warning text-dark");
                    return ("Места есть", "bg-success");
                }
            }
        }
    }
}
