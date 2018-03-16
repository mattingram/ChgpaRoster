using System.Linq;
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

        public string Result {get; private set;}
        public bool IsActive { get; private set; }
        public string Message {get; private set;}

        public void OnPost()
        {
            Message = null;
            Result = null;

            if (Ushpa == null && Email == null)
            {
                Message = "Please enter a Ushpa # or Email address.";
                return;
            }
            if (Ushpa != null || Email != null)
            {
                string filter = string.Empty;
                if (Ushpa != null)
                {
                    filter = $"USHPA eq '{Ushpa}'";
                }
                if (Email != null)
                {
                    if (filter.Length > 0)
                    {
                        filter += " or "; 
                    }
                    filter += $"Email eq '{Email.ToLower()}' or SecondaryEmail eq '{Email.ToLower()}'";
                }
                var member = MemberHelper.GetMembersByFilter(filter).FirstOrDefault();
                var validationResult = MemberHelper.ValidateMembership(member);
                Result = validationResult.message;
                IsActive = validationResult.isActive;
            }
        }
    }
}