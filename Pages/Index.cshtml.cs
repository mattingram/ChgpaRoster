using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Roster.Models;
using Roster.Utilities;

namespace Roster.Pages
{
    public class IndexModel : PageModel
    {
        public List<Member> Members;

        public void OnGet()
        {
            Member member = CloudTableHelper.GetMember("Ingram", "mingram@vt.edu");
            Members = new List<Member>();
            Members.Add(member);
        }
    }
}