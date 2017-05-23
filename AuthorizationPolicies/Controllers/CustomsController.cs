using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace AuthorizationPolicies.Controllers
{
    [Authorize]
    public class CustomsController : Controller
    {
        [Authorize(Policy = AuthorizationPolicies.EUCustoms)]
        public IActionResult EuropeanUnionArrivals()
        {
            return View("Outside");
        }

        [Authorize(Policy = AuthorizationPolicies.NothingToDeclare)]
        public IActionResult NothingToDeclare()
        {
            return View("Outside");
        }

        public IActionResult GoodsToDeclare()
        {
            return View("Outside");
        }
    }
}