using BSBAdminDashboard.BL.Interface;
using BSBAdminDashboard.BL.Mapper;
using BSBAdminDashboard.BL.Repository;
using BSBAdminDashboard.DAL.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BSBAdminDashboard
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Security - Identity
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<DbContainer>()
                .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>(TokenOptions.DefaultProvider);

            services.Configure<IdentityOptions>(options =>
            {
                // Default Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 0;
            });

            // OR 

            //services.AddIdentity<IdentityUser, IdentityRole>(options => {
            //    // Default Password settings.
            //    options.Password.RequireDigit = true;
            //    options.Password.RequireLowercase = true;
            //    options.Password.RequireNonAlphanumeric = true;
            //    options.Password.RequireUppercase = true;
            //    options.Password.RequiredLength = 6;
            //    options.Password.RequiredUniqueChars = 1;
            //}).AddEntityFrameworkStores<DbContainer>();

            // Serialize
            services.AddControllersWithViews()

                // Take Inistance From Language
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization()

                .AddNewtonsoftJson(opt =>{
                    opt.SerializerSettings.ContractResolver = new DefaultContractResolver();
                });

            // Take Inistance From Language For Location
            //services.AddLocalization(opt => opt.ResourcesPath = "");

            services.AddAutoMapper(x => x.AddProfile(new DomainProfile()));

            services.AddDbContextPool<DbContainer>(opts =>
            opts.UseSqlServer(Configuration.GetConnectionString("SharpDbConnection")));

            // Inestance of DepartmentRep

            // 1- Take instance every request (Slow)
            //services.AddTransient<DepartmentRep>();

            // 2- Take instance for each user (Fast)
            services.AddScoped<IDepartmentRep, DepartmentRep>();
            services.AddScoped<IEmployeeRep, EmployeeRep>();
            services.AddScoped<ICountryRep, CountryRep>();
            services.AddScoped<ICityRep, CityRep>();
            services.AddScoped<IDistrictRep, DistrictRep>();

            // 3 - Take Shared instance for All users (Faster Than)
            //services.AddSingleton<DepartmentRep>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            // Take Inistance From Language
            var supportedCulture = new[] {
            new CultureInfo("ar-EG"),
            new CultureInfo("en-US")
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                // if you need firs load for page English "en-US" || "ar-EG" for Arabic
                DefaultRequestCulture = new RequestCulture("en-US"),
                SupportedCultures = supportedCulture,
                SupportedUICultures = supportedCulture,

                // Provider
                RequestCultureProviders = new List<IRequestCultureProvider>
                {
                    new QueryStringRequestCultureProvider(),
                    new CookieRequestCultureProvider()
                }

            });

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            // Areas
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "areas", // Not Resirved You Can Write any name
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            });

            // Default
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
            });

            


        }
    }
}
