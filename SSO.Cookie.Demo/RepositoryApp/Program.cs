using Microsoft.AspNetCore.DataProtection;

namespace RepositoryApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDataProtection().PersistKeysToFileSystem(new DirectoryInfo(@"c:\a0")).SetApplicationName("SharedCookieApp");

            //CookieAuthenticationDefaults.AuthenticationScheme
            builder.Services.AddAuthentication(MyAuthenticationSchemeOptions.AuthenticationScheme).AddCookie(MyAuthenticationSchemeOptions.AuthenticationScheme, options =>
            {
               // options.ExpireTimeSpan = TimeSpan.FromSeconds(15);
                options.Cookie.Name = ".AspNet.SharedCookie";
                options.Cookie.Path = "/";
                //  options.Cookie.Domain = ".myapp.com";
                options.LoginPath = "/login/proxy";
                var data = options.DataProtectionProvider;
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}