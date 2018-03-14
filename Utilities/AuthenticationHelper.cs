using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        public async Task SignInAsync(HttpContext context, IdentityUser user)
        {
             var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email)
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                //AllowRefresh = <bool>,
                // Refreshing the authentication session should be allowed.

                //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                // The time at which the authentication ticket expires. A 
                // value set here overrides the ExpireTimeSpan option of 
                // CookieAuthenticationOptions set with AddCookie.

                //IsPersistent = true,
                // Whether the authentication session is persisted across 
                // multiple requests. Required when setting the 
                // ExpireTimeSpan option of CookieAuthenticationOptions 
                // set with AddCookie. Also required when setting 
                // ExpiresUtc.

                //IssuedUtc = <DateTimeOffset>,
                // The time at which the authentication ticket was issued.

                //RedirectUri = <string>
                // The full path or absolute URI to be used as an http 
                // redirect response value.
            };

            await context.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme, 
                new ClaimsPrincipal(claimsIdentity), 
                authProperties);
        }
    }
}