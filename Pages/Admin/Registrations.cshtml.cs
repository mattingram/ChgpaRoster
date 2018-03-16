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
using System.Net.Http;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.IO;

namespace Roster.Pages.Admin
{
    [Authorize]
    public class RegistrationsModel : PageModel
    {
        public List<Registration> Registrations;// = new Registration[] {};
        private GravityFormsApi _api;
        private HttpClient _client;

        public RegistrationsModel(GravityFormsApi api, HttpClient client)
        {
            _api = api;
            _client = client;
        }
        
        public void OnGetAsync()
        {
            Registrations = _api.GetRegistrationsSince("2018-02-01");
        }
        
        public IActionResult OnGetUrl()
        {
            return Content(_api.GetUrlForEntriesSince("2018-02-01"));
        }
    }
}