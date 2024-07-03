using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;

namespace OrderApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //builder.WebHost.ConfigureAppConfiguration((hostingContext, config)=> { 
            //    var env = hostingContext.HostingEnvironment;
            //    if (env.IsDevelopment())
            //    {
            //        config.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
            //    }
            //    else if (env.IsProduction())
            //    {
            //        config.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
            //    }
              
            //   // config.AddJsonFile("appsettings.json", optional: true);
            //});

          
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDataProtection().PersistKeysToFileSystem(new DirectoryInfo(@"c:\a0")).SetApplicationName("SharedCookieApp");

     
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromSeconds(60);
                options.Cookie.Name = ".AspNet.SharedCookie";
                options.Cookie.Path = "/";
 
                 options.Cookie.Domain = ".smart.com";
 

                options.LoginPath = "/login/proxy";
            });

            builder.WebHost.UseUrls("http://order.smart.com:10001");

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}