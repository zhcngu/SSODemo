using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Account.Center;

namespace SSO.Center.Controllers
{
    [Authorize]
    public class Login : Controller
    {
        [AllowAnonymous]
        public IActionResult Index(string returnurl)
        {
            return View("Index");
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SignIn([FromForm]string userName , [FromForm] string password, string? returnUrl)
        {
            if (userName != null && password != null)
            {
                var claims = new List<Claim>() 
                {
                    new Claim(ClaimTypes.Name, userName)
                };
              
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                if (string.IsNullOrWhiteSpace(returnUrl))
                {
                    return RedirectToAction("index", "home");
                }
                else
                {
                    return Redirect(returnUrl);
                }

       
            }
            return RedirectToAction("index");
        }


        [HttpGet]
        public  async Task  <IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("index");
        }
    }
}
