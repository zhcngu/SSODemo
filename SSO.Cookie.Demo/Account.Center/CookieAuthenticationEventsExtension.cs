using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace SSO.Center
{
    public class CookieAuthenticationEventsExtension : CookieAuthenticationEvents
    {


        public override Task RedirectToAccessDenied(RedirectContext<CookieAuthenticationOptions> context)
        {
            return base.RedirectToAccessDenied(context);
        }
        //

        public override Task RedirectToLogin(RedirectContext<CookieAuthenticationOptions> context)
        {
            return base.OnRedirectToLogin(context);
        }

        public override Task RedirectToLogout(RedirectContext<CookieAuthenticationOptions> context)
        {
            return base.RedirectToLogout(context);
        }

        public override Task RedirectToReturnUrl(RedirectContext<CookieAuthenticationOptions> context)
        {
            return base.RedirectToReturnUrl(context);
        }

        
        public virtual Task SignedIn(CookieSignedInContext context)
        {
            return base.SignedIn(context);
        }


        public override Task SigningIn(CookieSigningInContext context)
        {
            return base.SigningIn(context);
        }
        
        public override Task SigningOut(CookieSigningOutContext context)
        {
            return base.SigningOut(context);
        }
        
        public override Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {
            return base.ValidatePrincipal(context);
        }
    }
}
