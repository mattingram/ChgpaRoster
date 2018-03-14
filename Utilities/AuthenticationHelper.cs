using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Roster.Utilities
{
    public class AuthenticationHelper
    {
        private List<string> AdminUsers;
        private List<string> AdminKeys;

        public AuthenticationHelper(IConfiguration config)
        {
            if (config["AdminUsers"] == null || config["AdminKeys"] == null)
            {
                throw new Exception("Missing config AdminUsers or AdminKeys.");
            }
            AdminUsers = config["AdminUsers"].Split(";").ToList();
            AdminKeys = config["AdminKeys"].Split(";").ToList();
        }

        public IdentityUser AuthenticateUser(string email, string password)
        {
            if (AdminUsers.Contains(email) && AdminKeys.Contains(password))
            {
                return new IdentityUser()
                {
                    Email = email
                };
            }
            else
            {
                return null;
            }
        }
    }
}