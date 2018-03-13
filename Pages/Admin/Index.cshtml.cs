using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Roster.Models;
using Roster.Utilities;
using System;

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
        
        [BindProperty]
        public string Message {get; set;}

        public void OnGet()
        {
            Console.WriteLine("GetAll");
            Members = MemberHelper.GetAll().ToList();
            Console.WriteLine("DoneGetAll");
        }

        // public void OnPost()
        // {
        //     string filter = string.Empty;
        //     if (LastName != null)
        //     {
        //         filter = $"LastName eq '{LastName}'";
        //     }
        //     if (Email != null)
        //     {
        //         if (filter.Length > 0)
        //         {
        //             filter += " or "; 
        //         }
        //         filter += $"Email eq '{Email.ToLower()}' or SecondaryEmail eq '{Email.ToLower()}'";
        //     }
        //     Members = MemberHelper.GetMembersByFilter(filter);
        // }
    }
}