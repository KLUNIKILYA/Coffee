using Coffee.Core.Entities;
using Coffee.Core.Interfaces; 
using Coffee.Data.Context;
using Coffee.Data.Repositories;
using Coffee.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Coffee.Areas.Admin.Pages.Events
{
    public class IndexModel : PageModel
    {
        private readonly IEventService _eventService;

        public IndexModel(IEventService eventService)
        {
            _eventService = eventService;
        }

        public IList<EventIndexVM> Events { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var dtos = await _eventService.GetDtosAsync();

            Events = dtos.Select(d => new EventIndexVM
            {
                Id = d.Id,
                Title = d.Title,
                PlacesTaken = d.PlacesTaken,
                StartDate = d.StartDate,
                LecturerId = d.LecturerId,
                Price = d.Price
            }).ToList();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _eventService.DeleteEventAsync(id);

            return RedirectToPage();
        }
    }
}