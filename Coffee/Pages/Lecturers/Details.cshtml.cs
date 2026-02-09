using Coffee.Core.Entities;
using Coffee.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Coffee.Pages.Lecturers
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Lecturer Lecturer { get; set; }
        public List<Event> UpcomingEvents { get; set; } = new List<Event>();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id <= 0) return NotFound();

            Lecturer = await _context.Lecturers
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Lecturer == null)
            {
                return NotFound();
            }

            UpcomingEvents = await _context.Events
                .Where(e => e.LecturerId == id && e.StartDate >= DateTime.Now)
                .OrderBy(e => e.StartDate)
                .Take(3)
                .ToListAsync();

            return Page();
        }

        public string GetEmbedUrl(string url)
        {
            if (string.IsNullOrEmpty(url)) return null;

            // Простая регулярка для поиска ID видео
            var regex = new Regex(@"(?:youtube\.com\/(?:[^\/]+\/.+\/|(?:v|e(?:mbed)?)\/|.*[?&]v=)|youtu\.be\/)([^""&?\/\s]{11})");
            var match = regex.Match(url);

            if (match.Success)
            {
                return $"https://www.youtube.com/embed/{match.Groups[1].Value}";
            }
            return null;
        }
    }
}