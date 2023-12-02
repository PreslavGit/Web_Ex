using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace webex.Pages
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public LogoutModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnGet()
        {
            // Perform the user logout
            await _signInManager.SignOutAsync();

            // Optionally, sign out from additional external authentication providers
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            // Redirect to the home page or any other desired page after logout
            return RedirectToPage("/Index");
        }
    }
}