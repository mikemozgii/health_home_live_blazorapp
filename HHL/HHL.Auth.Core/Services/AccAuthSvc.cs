using HHL.Auth.Core.DataAccess.Views;
using HHL.Auth.Core.Services;
using HHL.Auth.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using HHL.Auth.Core.Handlers;
using System.Security.Claims;
using Newtonsoft.Json;
using HHL.Auth.Core.DataAccess.Workers;
using NodaTime;
using HHL.Auth.Core.DataAccess.Entities;
using HHL.PostreSQL.Core.Handlers;
using HHL.PostreSQL.Core.Uuid;
using HHL.PostreSQL.Core.Services;
using System.Text.RegularExpressions;
using HHL.PostreSQL.Core.Models;
using HHL.Auth.Core.Interfaces;

namespace HHL.Auth.Core.Services
{
    public class AccAuthSvc: AuthDataAccessSvc
    {
        public AccAuthSvc(IAuthQueryExecutionSvc queryExecutionSvc) : base(queryExecutionSvc)
        {
        }

        public async Task<RegisterResponse> Register(RegisterRequest model)
        {

            var validation = await new AuthValidatorSvc(_AuthQueryExecutionSvc).IsValidateRequest(model);
            if (!validation.Success) return validation;

            var register_response = validation;
            var accountId = new PKuuidSvc().GenereateHHLGuid();
            var emailId  = new PKuuidSvc().GenereateHHLGuid();

            var qRequest_email = new e_Email().INSERT(model, emailId);
            var qRequest_acc = new e_Account().INSERT(model, emailId, accountId);


            var resp =  await _AuthQueryExecutionSvc.ExecuteQueryAsync(qRequest_email, qRequest_acc);

            if (resp.Success)
            {
                var generated = await new EmailSvc(_AuthQueryExecutionSvc).GenerateVerificationCode(model.Email);

                register_response.Success = generated;
                register_response.AccountId = accountId;
            }

            return register_response;
        }

 

        public async  Task<SignInResponse> SignIn(SignInRequest model)
        {           
            var validation = await new AuthValidatorSvc(_AuthQueryExecutionSvc).IsValidateRequest(model);
            if (!validation.Item1.Success)
            {
               await _AuthQueryExecutionSvc.ExecuteQueryAsync(new e_SignInLogs().INSERT(model.UserAgent, model.IpAddress, validation.Item1.Success, model.Name, null));
               return validation.Item1;
            }

               
            var signin_response = validation.Item1;
            var account = validation.Item2;

            if (account != null && new BCryptSvc().IsValid(model.Password, account.AccountPassword, AuthConfigHdr.BcryptAccountPasswordSalt))
            {

                signin_response.Token = GreateToken(account.AccountId.ToString());

                var accountSession = new AccountSession() {
                    AccountId = account.AccountId,
                    AccountName = account.AccountName,
                    Token = signin_response.Token,
                    EmailName = account.EmailName,

                };
                signin_response.AccountSession = accountSession;

                signin_response.Success = await RedisHdr.redisStorage.SetValueAsync($"accountId:{account.AccountId}", JsonConvert.SerializeObject(accountSession));

                await _AuthQueryExecutionSvc.ExecuteQueryAsync(new e_SignInLogs().INSERT(model.UserAgent, model.IpAddress, validation.Item1.Success, model.Name, accountSession.AccountId));
                return signin_response;

            }

            signin_response.Errors = new List<ErrorMdl>() { new ErrorMdl(999, "The username or password is incorrect") };  
            signin_response.Success = false;
            var resp = await _AuthQueryExecutionSvc.ExecuteQueryAsync(new e_SignInLogs().INSERT(model.UserAgent, model.IpAddress, validation.Item1.Success, model.Name, null));
            return signin_response;
        }

        public async Task<bool> UpdateAccountSession(AccountSession model)
        {
            var update = await RedisHdr.redisStorage.SetValueAsync($"accountId:{model.AccountId}", JsonConvert.SerializeObject(model));
            return update;
        }

        public async Task<AccountSession> GetAccountSession(Guid accountId)
        {
            var accSession = await RedisHdr.redisStorage.GetValueAsync($"accountId:{accountId}");
            return JsonConvert.DeserializeObject<AccountSession>(accSession);
        }
        

        public async Task<bool> SignOut(Guid accountId)
        {
            var response = await RedisHdr.redisStorage.DeleteValueAsync($"accountId:{accountId}");
            return response;
        }
        public async Task<RefreshTokenResponse> RefreshToken(RefreshTokenRequest model)
        {
            var validation = await new AuthValidatorSvc(_AuthQueryExecutionSvc).IsValidateRequest(model);
            if (!validation.Item1.Success) return validation.Item1;
            var refreshToken_response = validation.Item1;

            var accountId = validation.Item2.First(q => q.Type == AuthConfigHdr.JwtAccountClaimName).Value;
            refreshToken_response.Token = GreateToken(accountId);

            return refreshToken_response;

        }

        public string GreateToken(string accountId)
        {

            var jwtOption = new JwtOption(AuthConfigHdr.JwtKey, AuthConfigHdr.JwtIssue, AuthConfigHdr.JwtAudience)
            {
                Claims = new List<Claim>() {
                    new Claim(AuthConfigHdr.JwtAccountClaimName, accountId),
                    new Claim(AuthConfigHdr.JwtRoleClaimName, "user"),
                }
            };

            return new JwtSvc().Create(jwtOption);
        }





  
    }
}
