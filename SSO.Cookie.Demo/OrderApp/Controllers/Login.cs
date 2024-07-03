using Microsoft.AspNetCore.Mvc;

namespace OrderApp.Controllers
{
    public class Login:Controller
    {
        public async Task<IActionResult> Proxy(string returnUrl)
        {
       
            string url = "";
#if DEBUG
            url = string.Concat("http://home.smart.com:10000/login?returnUrl=http://order.smart.com:10001", returnUrl);
#else
             url = string.Concat("http://home.smart.com:10000/login?returnUrl=http://order.smart.com", returnUrl);
#endif


            return await Task.FromResult( Redirect(url));
        }
    }
}
