using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CRM.Data;
using CRM.Models;
using CRM.Services;
using CRM.Repositories;
using Newtonsoft.Json.Serialization;
using CRM.Services.AuthOptions;

namespace CRM
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConfiguration _configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(config =>
            {
                config.SignIn.RequireConfirmedEmail = Boolean.Parse(_configuration["IdentityOptions:SignIn:RequireConfirmedEmail"]);
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Identity Options
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = Boolean.Parse(_configuration["IdentityOptions:Password:RequireDigit"]);
                options.Password.RequiredLength = Int32.Parse(_configuration["IdentityOptions:Password:RequiredLength"]);
                options.Password.RequireNonAlphanumeric = Boolean.Parse(_configuration["IdentityOptions:Password:RequireNonAlphanumeric"]);
                options.Password.RequireUppercase = Boolean.Parse(_configuration["IdentityOptions:Password:RequireUppercase"]);
                options.Password.RequireLowercase = Boolean.Parse(_configuration["IdentityOptions:Password:RequireLowercase"]); ;
                options.Password.RequiredUniqueChars = Int32.Parse(_configuration["IdentityOptions:Password:RequiredUniqueChars"]); ;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(Int32.Parse(_configuration["IdentityOptions:Lookout:DefaultLockoutTimeSpan"]));
                options.Lockout.MaxFailedAccessAttempts = Int32.Parse(_configuration["IdentityOptions:Lookout:MaxFailedAccessAttempts"]);
                options.Lockout.AllowedForNewUsers = Boolean.Parse(_configuration["IdentityOptions:Lookout:AllowedForNewUsers"]);

                // User settings
                options.User.RequireUniqueEmail = Boolean.Parse(_configuration["IdentityOptions:User:RequireUniqueEmail"]);
            });

            // Add authentication options of third parties (e.g. SendGrid)
            services.Configure<EmailSenderAuthOptions>(_configuration.GetSection(nameof(EmailSenderSendGrid)));

            // Add application services.
            services.AddSingleton<IEmailSender, EmailSenderSendGrid>();

            // Add application repositories via UnitOfWOrk
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services
                .AddMvc()
                .AddJsonOptions(options => {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    // The option below solves the issue of circular reference between classes
                    // Antoher option is to add the data annoation "[Newtonsoft.Json.JsonIgnoreAttribute]" to the attributes that are not neccessarily to be serialized
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
