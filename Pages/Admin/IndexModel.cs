using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Roster.Models;
using Roster.Utilities;

namespace Roster.Pages.Admin
{
    [Authorize(CookieAuthenticationDefaults.AuthenticationScheme)]
    public class IndexModel : PageModel
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