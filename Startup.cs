using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace ChgpaRoster
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddRazorPagesOptions(options =>
            {
                options.RootDirectory = "/Pages";
                //options.Conventions.AuthorizeFolder("/Pages/Admin");
            });
            
            services.AddSingleton<IConfiguration>(Configuration);

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    // Cookie settings
                    options.Cookie.HttpOnly = true;
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(120);
                    options.LoginPath = "/Admin/Login"; // If the LoginPath is not set here, ASP.NET Core will default to /Account/Login
                    options.LogoutPath = "/Admin/Logout"; // If the LogoutPath is not set here, ASP.NET Core will default to /Account/Logout
                    options.AccessDeniedPath = "/Admin/AccessDenied"; // If the AccessDeniedPath is not set here, ASP.NET Core will default to /Account/AccessDenied
                    options.SlidingExpiration = true;
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "ChgpaRoster", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            Environment.SetEnvironmentVariable("DefaultConnection", GetConnectionString("DefaultConnection"));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }

            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ChgpaRoster v1");
            });
        }

        public string GetConnectionString(string name)
        {
            string value = Configuration.GetConnectionString(name);
            if (string.IsNullOrEmpty(value))
            {
                value = GetConfig(name);
            }
            return value;
        }

        public string GetConfig(string name)
        {
            string value = Configuration[name];
            if (string.IsNullOrEmpty(value))
            {
                throw new Exception($"{name} not found in Configuration.");
            }
            return value;
        }
    }
}
