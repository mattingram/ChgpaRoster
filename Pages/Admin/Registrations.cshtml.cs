using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Roster.Models;
using Roster.Utilities;

namespace Roster.Pages.Admin
{
    [Authorize]
    public class RegistrationsModel : PageModel
    {
        public List<Registration> Registrations = new List<Registration>();

        private IConfiguration _config;

        public RegistrationsModel(IConfiguration config)
        {
            _config = config;
        }
        
        public void OnGet()
        {
            //GravityFormsApi api = new GravityFormsApi(_config);
            //Console.WriteLine(api.GetLatestEntries());
        }
    }
}