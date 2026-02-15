using Coffee.Core.Entities;
using Coffee.Core.Interfaces;
using Coffee.Core.Interfaces.Waitlist;
using Coffee.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Coffee.Pages.Cabinet
{
    [Authorize]
    public class WaitlistModel : PageModel
    {
        private readonly IWaitlistRepository _waitlistRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public WaitlistModel(IWaitlistRepository waitlistRepository, UserManager<ApplicationUser> userManager)
        {
            _waitlistRepository = waitlistRepository;
            _userManager = userManager;
        }

        public List<WaitlistVM> WaitlistItems { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToPage("/Account/Login");

            var entries = await _waitlistRepository.GetUserWaitlistAsync(user.Id);

            WaitlistItems = entries.Select(w => new WaitlistVM
            {
                Id = w.Id,
                EventId = w.EventId,
                EventTitle = w.Event.Title,
                LecturerName = w.Event.Lecturer?.FullName ?? "TBA",
                StartDate = w.Event.StartDate,
                ImageUrl = w.Event.ImageUrl,
                CreatedAt = w.CreatedAt
            }).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _waitlistRepository.DeleteAsync(id);
            return RedirectToPage();
        }
    }
}