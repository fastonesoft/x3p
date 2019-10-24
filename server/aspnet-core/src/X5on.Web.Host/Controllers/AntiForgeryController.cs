using Microsoft.AspNetCore.Antiforgery;
using X5on.Controllers;

namespace X5on.Web.Host.Controllers
{
    public class AntiForgeryController : X5onControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
