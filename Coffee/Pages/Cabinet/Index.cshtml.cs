using Coffee.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Coffee.Pages.Cabinet
{
    [Authorize] 
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public ApplicationUser CurrentUser { get; set; } = default!;
        public string Initials { get; set; } = "U";

        public async Task<IActionResult> OnGetAsync()
        {
            CurrentUser = await _userManager.GetUserAsync(User);

            if (CurrentUser == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            if (!string.IsNullOrEmpty(CurrentUser.FirstName))
            {
                Initials = CurrentUser.FirstName[0].ToString().ToUpper();
                if (!string.IsNullOrEmpty(CurrentUser.LastName))
                {
                    Initials += CurrentUser.LastName[0].ToString().ToUpper();
                }
            }
            else
            {
                Initials = CurrentUser.Email?[0].ToString().ToUpper() ?? "U";
            }

            return Page();
        }
    }
}