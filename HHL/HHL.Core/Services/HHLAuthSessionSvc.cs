using HHL.Auth.Core.Handlers;
using HHL.Auth.Core.Models;
using HHL.Auth.Core.Services;
using HHL.Core.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace HHL.Core.Services
{
    public class HHLAuthSessionSvc
    {
        public string Token { get; set; }
        public bool IsAuthenticated { get; set; }
        public HHLAccountSession AccountSession { get; set; }

        private readonly IHttpContextAccessor _httpContextAccessor;
        public HHLAuthSessionSvc(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            var token = _httpContextAccessor.HttpContext.Request.Headers[AuthConfigHdr.AuthHeaderName];
            if (string.IsNullOrWhiteSpace(token))
            {
                token = _httpContextAccessor.HttpContext.Request.Cookies[AuthConfigHdr.AuthCookieName];
            }

            Token = token;
            Init();

            //IsAuthenticated = false;
            //AccountSession = new AccountSession()
            //{
            //    AccountName = "universeusermike32",
            //    AccountId = new Guid("dba3ec39-25a9-0096-a361-016f5c51c866"),
            //    EmailName = "uig.trand@htmal.com",
            //    ContractorInfo = new ContractorInfo() {
            //        ContractName = "peragovacakes",
            //        EmailName = "order@peragovacakes.com"
            //}
            //};


        }

        public void Init()
        {
            try
            {
                var jwtOption = new JwtOption(AuthConfigHdr.JwtKey, AuthConfigHdr.JwtIssue, AuthConfigHdr.JwtAudience);
                if (new JwtSvc().Validate(Token, jwtOption, out var claims))
                {
                    var accountId = claims.FirstOrDefault(q => q.Type == AuthConfigHdr.JwtAccountClaimName)?.Value;
                    var role = claims.FirstOrDefault(q => q.Type == AuthConfigHdr.JwtRoleClaimName)?.Value;
                    if (!string.IsNullOrWhiteSpace(accountId))
                    {

                        var accountSession = RedisHdr.redisStorage.GetValue($"accountId:{accountId}");
                        if (accountSession != null)
                        {
                            if (!string.IsNullOrWhiteSpace(accountSession))
                            {
                                AccountSession = JsonConvert.DeserializeObject<HHLAccountSession>(accountSession);
                                IsAuthenticated = true;
                            }
                        }
                    }
                }
            }
            catch
            {
                IsAuthenticated = false;
            }
        }




    }
}
