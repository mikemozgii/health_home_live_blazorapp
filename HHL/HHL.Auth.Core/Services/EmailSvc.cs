using HHL.Auth.Core.DataAccess.Entities;
using HHL.Auth.Core.Interfaces;
using HHL.Auth.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HHL.Auth.Core.Services
{
    public class EmailSvc: AuthDataAccessSvc
    {
        public EmailSvc(IAuthQueryExecutionSvc queryExecutionSvc) : base(queryExecutionSvc)
        {
        }

        public async Task<bool> GenerateVerificationCode(string email)
        {
            var code = new Random().Next(100000, 999999);  
            return   (await  _AuthQueryExecutionSvc.ExecuteQueryAsync(new e_Email().UPDATE(email.ToUpper(), code.ToString()))).Success;
        }


        public async Task<EmailVerificationResponse> VerifyEmail(EmailVerificationRequest model)
        {

            if (!model.IsValid) return new EmailVerificationResponse(false, new ErrorMdl(1, "Entry is Invlaid"));

            if ((await _AuthQueryExecutionSvc.ExecuteResultQueryAsync<e_Email>(new e_Email().UPDATE(model.NormalizedEmail, model.Code, DateTime.Now))).TryGetFirstOrDefault(out var r))
            {

                return new EmailVerificationResponse(true);
            }
 

            return new EmailVerificationResponse(false, new ErrorMdl(1, "Entry is Invlaid"));

        }
    }
}
