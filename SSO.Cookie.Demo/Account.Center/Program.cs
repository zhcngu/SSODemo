using Account.Center;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;

namespace SSO.Center
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDataProtection().PersistKeysToFileSystem(new DirectoryInfo(@"c:\a0")) .SetApplicationName("SharedCookieApp");

           //CookieAuthenticationDefaults.AuthenticationScheme
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options => 
            {
                options.ExpireTimeSpan = TimeSpan.FromSeconds(60);
                options.Cookie.Name = ".AspNet.SharedCookie";
                options.Cookie.Path = "/";
 
                options.Cookie.Domain = ".smart.com";
 

                options.LoginPath = "/login/index"; 
                options.Events = new CookieAuthenticationEventsExtension();
                var data = options.DataProtectionProvider;
            }); 
           var app = builder.Build();

            builder.WebHost.UseUrls("http://home.smart.com:10000");

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.Use(async (context, next) =>
            {
                if (context.Request.Path=="/")
                {
                    context.Request.Path = "/home/index";
                }
                 Console.WriteLine($"http request :{context.Request.Path}");
                var currentEndpoint = context.GetEndpoint();

                if (currentEndpoint is null)
                {
                    await next(context);
                    return;
                }

                Console.WriteLine($"Endpoint: {currentEndpoint.DisplayName}");

                if (currentEndpoint is RouteEndpoint routeEndpoint)
                {
                    Console.WriteLine($"  - Route Pattern: {routeEndpoint.RoutePattern}");
                }

                foreach (var endpointMetadata in currentEndpoint.Metadata)
                {
                    Console.WriteLine($"  - Metadata: {endpointMetadata}");
                }

                await next(context);
            });

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