using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Roster.Pages.Admin
{
    public class LoginModel : PageModel
    {
        public string UserName;
        public string Password;
        public string Message;

        public ActionResult OnGet()
        {
            return Page();
        }

        public ActionResult OnPost()
        {
            return RedirectToPage("./Index");
        }
    }
}