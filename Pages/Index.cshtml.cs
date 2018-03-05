using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Roster.Utilities;

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
                var member = MemberHelper.GetMemberByUshpa(Ushpa);
                UshpaResult = member?.ExpirationDate.ToString();
            }
            if (Email != null)
            {
                var member = MemberHelper.GetMemberByEmail(Email);
                EmailResult = member?.ExpirationDate.ToString();
            }
        }
    }
}