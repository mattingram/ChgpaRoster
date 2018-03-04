using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Roster.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string Ushpa {get; set;}

        [BindProperty]
        public string Email {get; set;}

        [TempData]
        public string UshpaResult {get; set;}

        [TempData]
        public string EmailResult {get; set;}

        [TempData]
        public string Message {get; set;}

        public void OnGet()
        {
            TempData.Clear();
        }

        public void OnPost()
        {
            if (Ushpa == null && Email == null)
            {
                Message = "Please enter a Ushpa # or Email address.";
                return;
            }
            if (Ushpa != null)
            {
                UshpaResult = "Found Ushpa!";
            }
            if (Email != null)
            {
                EmailResult = "Found email!";
            }
        }
    }
}