using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Roster.Models;
using Roster.Utilities;

namespace Roster.Pages.Admin
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public List<Member> Members = new List<Member>();
        
        [BindProperty]
        public string LastName {get; set;}
        
        [BindProperty]
        public string Email {get; set;}

        public void OnPost()
        {
            if (LastName == null || Email == null)
            {
                return;
            }
            Member member = CloudTableHelper.GetMember(LastName, Email);
            Members = new List<Member>();
            Members.Add(member);
        }
    }
}