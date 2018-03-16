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
        //public List<Registration> Registrations = new List<Registration>();
        public Registration[] Registrations = new Registration[] {};

        public string url { get; set; }

        private GravityFormsApi _api;
        private HttpClient _client;

        public RegistrationsModel(GravityFormsApi api, HttpClient client)
        {
            _api = api;
            _client = client;
        }
        
        public void OnGetAsync()
        {
            url = _api.GetLatestEntries(100);
            
            var streamTask = _client.GetStreamAsync(url);
            streamTask.Wait();
            var serializer = new DataContractJsonSerializer(typeof(GravityFormRegistration));
            var gfRegistration = serializer.ReadObject(streamTask.Result) as GravityFormRegistration;
            Registrations = gfRegistration?.Response?.Registrations;
            Console.WriteLine(Registrations?.Length);
        }
    }
}