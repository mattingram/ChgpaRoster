using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Roster.Models;
using Roster.Utilities;

namespace Roster.Pages.Admin
{
    [Authorize]
    public class AdminModel : PageModel
    {
        public List<Member> Members = new List<Member>();
        public string LastName;
        public string EmailAddress;

        public void OnGet()
        {
            // Member member = CloudTableHelper.GetMember("Ingram", "mingram@vt.edu");
            // Members = new List<Member>();
            // Members.Add(member);
        }
    }
}