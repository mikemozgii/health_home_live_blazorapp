using HHL.Auth.Core.Handlers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HHL.WebApp.Controllers
{

    public class CookieController : Controller
    {
        
        [HttpGet]
        [Route("api/cookie/SetAuthCookie")]
        public IActionResult SetAuthCookie([FromQuery]string id)
        {
            Response.Cookies.Append(AuthConfigHdr.AuthCookieName, id, new Microsoft.AspNetCore.Http.CookieOptions() { Expires = DateTime.Now.AddHours(AuthConfigHdr.AuthCookieExpiresHours) });
            return Redirect("/");
        }

        [HttpGet]
        [Route("api/cookie/DeleteAuthCookie")]
        public IActionResult DeleteAuthCookie([FromQuery]string id)
        {
            Response.Cookies.Delete(AuthConfigHdr.AuthCookieName);
            return Redirect("/");
        }
    }
}
