using Coffee.Core.Entities;
using Coffee.Core.Interfaces.Tickets;
using Coffee.ViewModels.Ticket;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Coffee.Pages.Cabinet
{
    [Authorize]
    public class MyTicketsModel : PageModel
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public MyTicketsModel(ITicketRepository ticketRepository, UserManager<ApplicationUser> userManager)
        {
            _ticketRepository = ticketRepository;
            _userManager = userManager;
        }

        public List<TicketVM> ActiveTickets { get; set; } = new();
        public List<TicketVM> PastTickets { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToPage("/Account/Login");

            var tickets = await _ticketRepository.GetByUserIdAsync(user.Id);

            foreach (var t in tickets)
            {
                var vm = new TicketVM
                {
                    Id = t.Id,
                    EventTitle = t.Event.Title,
                    StartDate = t.Event.StartDate,
                    ImageUrl = t.Event.ImageUrl,
                    Price = t.Event.Price
                };

                if (vm.IsPast) PastTickets.Add(vm);
                else ActiveTickets.Add(vm);
            }

            return Page();
        }
    }
}