using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Roster.Utilities;

namespace Roster.Pages.Admin
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [BindProperty]
        [Required]
        public string Password { get; set; }

        [BindProperty]
        public string Message { get; set; }

        private readonly AuthenticationHelper _authHelper;

        public LoginModel(AuthenticationHelper authHelper)
        {
            _authHelper = authHelper;
        }

        public ActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = _authHelper.AuthenticateUser(Email, Password);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login.");
                return Page();
            }

           await _authHelper.SignInAsync(HttpContext, user);
            
            return RedirectToPage("./Index");
        }
    }
}