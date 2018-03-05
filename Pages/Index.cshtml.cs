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

        public string UshpaResult {get; set;}

        public string EmailResult {get; set;}

        public string Message {get; set;}

        public void OnPost()
        {
            Message = null;
            UshpaResult = null;
            EmailResult = null;

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