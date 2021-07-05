using HHL.Auth.Core.Handlers;
using HHL.Auth.Core.Models;
using HHL.Auth.Core.Services;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HHL.WebApp.Services
{
    public class AuthSignIn
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        //public AuthSignIn(IHttpContextAccessor httpContextAccessor)
        //{
        //    _httpContextAccessor = httpContextAccessor;
         

        //}
        //public async Task<SignInResponse> SignIn(SignInRequest model)
        //{
        //    var resp = await new AccAuthSvc().SignIn(model);

        //    if (resp.Success)
        //    {
        //        //_httpContextAccessor.HttpContext.Response.Cookies.Append(AuthConfigHdr.AuthCookieName, "bbb", new Microsoft.AspNetCore.Http.CookieOptions() {
        //        //    Expires = DateTime.Now.AddHours(AuthConfigHdr.AuthCookieExpiresHours),
        //        //    IsEssential = false
        //        //});

        //        //_httpContextAccessor.HttpContext.Request.Cookies.Add(newCookie)
        //        //_httpContextAccessor.HttpContext.Request.Cookies.Append(AuthConfigHdr.AuthCookieName, "bbb");
        //    }

        //    return resp;
        //}


    }
}
