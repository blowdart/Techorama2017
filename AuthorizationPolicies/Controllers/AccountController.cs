using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace AuthorizationPolicies.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            await HttpContext.SignOutAsync(AuthenticationConfiguration.AuthenticationScheme);
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ValidatePassport(string nationality, string returnUrl = null)
        {
            const string Issuer = "urn:CDG.PassportControl";
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Country, nationality.ToUpperInvariant(), ClaimValueTypes.String, Issuer)
            };

            switch (nationality.ToUpperInvariant())
            {
                case "GBR":
                    claims.Add(new Claim(ClaimTypes.Name, "Theresa May", ClaimValueTypes.String, Issuer));
                    break;
                case "USA":
                    claims.Add(new Claim(ClaimTypes.Name, "Donald Trump", ClaimValueTypes.String, Issuer));
                    break;
                case "NLD":
                    claims.Add(new Claim(ClaimTypes.Name, "Papa Smurf", ClaimValueTypes.String, Issuer));
                    break;
                case "PRK":
                    claims.Add(new Claim(ClaimTypes.Name, "Kim Jong-un", ClaimValueTypes.String, Issuer));
                    break;
                default:
                    break;
            }

            var userIdentity = new ClaimsIdentity("PassportControl");
            userIdentity.AddClaims(claims);
            var userPrincipal = new ClaimsPrincipal(userIdentity);

            await HttpContext.SignInAsync(
                AuthenticationConfiguration.AuthenticationScheme, 
                userPrincipal,
                new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(20),
                    IsPersistent = false,
                    AllowRefresh = false
                });

            return RedirectToLocal(returnUrl);
        }

        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(
                AuthenticationConfiguration.AuthenticationScheme);

            return RedirectToLocal("/");
        }

        public IActionResult Forbidden()
        {
            return View();
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}