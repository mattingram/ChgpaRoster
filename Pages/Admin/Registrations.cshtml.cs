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

        public string url;

        private GravityFormsApi _api;

        public RegistrationsModel(GravityFormsApi api)
        {
            _api = api;
        }
        
        public void OnGet()
        {
            url = _api.GetLatestEntries();
        }
    }
}