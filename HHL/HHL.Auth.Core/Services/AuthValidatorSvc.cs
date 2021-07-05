using HHL.Auth.Core.DataAccess.Views;
using HHL.Auth.Core.Handlers;
using HHL.Auth.Core.Interfaces;
using HHL.Auth.Core.Models;
using HHL.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HHL.Auth.Core.Services
{
    public class AuthValidatorSvc: AuthDataAccessSvc
    {
        public AuthValidatorSvc(IAuthQueryExecutionSvc queryExecutionSvc) : base(queryExecutionSvc)
        {
        }

        public static int AccountMaxLength { get; set; } = 40;
        public static int AccountMinLength { get; set; } = 6;
        public static int EmailMaxLength { get; set; } = 150;
        public static int EmailMinLength { get; set; } = 6;
        public static int PasswordMaxLength { get; set; } = 50;
        public static int PasswordMinLength { get; set; } = 8;

        public async Task<RegisterResponse> IsValidateRequest(RegisterRequest model)
        {

            var errors = new List<ErrorMdl>();

            if (!Try_AccountNameValidation_Required(model.AccountName, out var error1))
            {
                errors.Add(error1);
            }
            else
            {
                if (!Try_AccountNameValidation_LerrerAndNumbersOnly(model.AccountName, out var error2)) errors.Add(error2); 
                if (!Try_AccountNameValidation_MinLength(model.AccountName, out var error3)) errors.Add(error3);
                if (!Try_AccountNameValidation_MaxLength(model.AccountName, out var error4)) errors.Add(error4);
            }


            if (!Try_EmailValidation_Required(model.Email, out var error5))
            {
                errors.Add(error5);
            }
            else
            {
                if (!Try_EmailValidation_Regex(model.Email, out var error6)) errors.Add(error6);
                if (!Try_EmailValidation_MaxLength(model.Email, out var error7)) errors.Add(error7);
                if (!Try_EmailValidation_MinLength(model.Email, out var error8)) errors.Add(error8);
            }

            if (!Try_PasswordValidation_Required(model.Password, out var error9))
            {
                errors.Add(error9);
            }
            else
            {
                if (!Try_PasswordValidation_UpperCase(model.Password, out var error11)) errors.Add(error11);
                if (!Try_PasswordValidation_LowerCase(model.Password, out var error10)) errors.Add(error10);
                if (!Try_PasswordValidation_Number(model.Password, out var error14)) errors.Add(error14);
                if (!Try_PasswordValidation_Special(model.Password, out var error15)) errors.Add(error15);
                if (!Try_PasswordValidation_MaxLength(model.Password, out var error12)) errors.Add(error12);
                if (!Try_PasswordValidation_MinLength(model.Password, out var error13)) errors.Add(error13);


                
            }

            if (errors.Any())
            {
                return new RegisterResponse(errors);
            }
            var q = new v_AccountAuth().SELECT(model.AccountName, model.Email);
            var response = await _AuthQueryExecutionSvc.ExecuteResultQueryAsync<v_AccountAuth>(q);
            if (response.TryGetFirstOrDefault(out var account))
            {
                errors.Add(new ErrorMdl(3, "Account Name or Email is already in use"));
                return new RegisterResponse(errors);
            }

            return new RegisterResponse(true);
        }

        public async Task<Tuple<SignInResponse, v_AccountAuth>> IsValidateRequest(SignInRequest model)
        {
            var errors = new List<ErrorMdl>();
          
            if (!Try_AccountNameValidation_Required(model.Name, out var error1))
            {
                errors.Add(error1);
            }
            else
            {
                if (model.Name.Contains("@"))
                {
                    if (!Try_EmailValidation_Regex(model.Name, out var error6)) errors.Add(error6);
                    if (!Try_EmailValidation_MaxLength(model.Name, out var error7)) errors.Add(error7);
                    if (!Try_EmailValidation_MinLength(model.Name, out var error8)) errors.Add(error8);
                }
                else
                {
                    if (!Try_AccountNameValidation_LerrerAndNumbersOnly(model.Name, out var error2)) errors.Add(error2);
                    if (!Try_AccountNameValidation_MinLength(model.Name, out var error3)) errors.Add(error3);
                    if (!Try_AccountNameValidation_MaxLength(model.Name, out var error4)) errors.Add(error4);

                }


            }


            if (!Try_PasswordValidation_Required(model.Password, out var error9))
            {
                errors.Add(error9);
            }
            else
            {
                if (!Try_PasswordValidation_UpperCase(model.Password, out var error11)) errors.Add(error11);
                if (!Try_PasswordValidation_LowerCase(model.Password, out var error10)) errors.Add(error10);
                if (!Try_PasswordValidation_Number(model.Password, out var error14)) errors.Add(error14);
                if (!Try_PasswordValidation_Special(model.Password, out var error15)) errors.Add(error15);               
                if (!Try_PasswordValidation_MaxLength(model.Password, out var error12)) errors.Add(error12);
                if (!Try_PasswordValidation_MinLength(model.Password, out var error13)) errors.Add(error13);
            }

            if (errors.Any())
            {
                return new Tuple<SignInResponse, v_AccountAuth>(new SignInResponse(errors), null);
            }



            var response = await _AuthQueryExecutionSvc.ExecuteResultQueryAsync<v_AccountAuth>(new v_AccountAuth().SELECT(model.Name, model.NormalizedEmail));
            if (!response.TryGetFirstOrDefault(out var account))
            {
                errors.Add(new ErrorMdl(3, "Account is not found"));
                return new Tuple<SignInResponse, v_AccountAuth>(new SignInResponse(errors), null);
            }
            else
            {
                if (!account.IsEmailVerified)
                {
                    errors.Add(new ErrorMdl(5, "Email address is not verified"));
                    return new Tuple<SignInResponse, v_AccountAuth>(new SignInResponse(errors), account);
                }

            }

            return new Tuple<SignInResponse, v_AccountAuth>(new SignInResponse(true), account);
        }
        public async Task< Tuple<RefreshTokenResponse, IEnumerable<Claim>>> IsValidateRequest(RefreshTokenRequest model)
        {
            var errors = new List<ErrorMdl>();
            var claims = new List<Claim>();
            if (!Try_TokenValidation_Required(model.OriginalToken, out var error1))
            {
                errors.Add(error1);
            }
            else
            {
                var jwtOption = new JwtOption(AuthConfigHdr.JwtKey, AuthConfigHdr.JwtIssue, AuthConfigHdr.JwtAudience);

                if (!new JwtSvc().Validate(model.OriginalToken, jwtOption, out var tClaims))
                {
                    errors.Add(new ErrorMdl(3, "Token is not valid or expired"));
                }
                else
                {
                    claims = tClaims;
                }
                
            }

            if (errors.Any())
            {
                return  new Tuple<RefreshTokenResponse, IEnumerable<Claim>>(new RefreshTokenResponse(errors), null);
            }

            return new Tuple<RefreshTokenResponse, IEnumerable<Claim>>(new RefreshTokenResponse(errors), claims);
        }

        public bool Try_TokenValidation_Required(string token, out ErrorMdl error)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                error = new ErrorMdl(1, "Token is required");
                return false;
            }
            error = null;
            return true;
        }

        public bool Try_AccountNameValidation_Required(string accountName, out ErrorMdl error)
        {
            if (string.IsNullOrWhiteSpace(accountName))
            {
                error = new ErrorMdl(1, "Account Name is required");
                return false;
            }
            error = null;
            return true;
        }

        public bool Try_AccountNameValidation_MinLength(string accountName, out ErrorMdl error)
        {
            if (accountName.Length < AccountMinLength)
            {
                error = new ErrorMdl(11, "Account Name is too short");
                return false;
            }
            error = null;
            return true;
        }

        public bool Try_AccountNameValidation_MaxLength(string accountName, out ErrorMdl error)
        {
            if (accountName.Length > AccountMaxLength)
            {
                error = new ErrorMdl(12, "Account Name is too long");
                return false;
            }
            error = null;
            return true;
        }

        public bool Try_AccountNameValidation_LerrerAndNumbersOnly(string accountName, out ErrorMdl error)
        {
            if (!Regex.IsMatch(accountName, RegexConst.LerrerAndNumbersOnly))
            {
                error = new ErrorMdl(13, "Account Name has inappropriate symbols");
                return false;
            }
            error = null;
            return true;
        }

        public bool Try_EmailValidation_Required(string email, out ErrorMdl error)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                error = new ErrorMdl(1, "Email Name is required");
                return false;
            }
            error = null;
            return true;
        }

        public bool Try_EmailValidation_MaxLength(string email, out ErrorMdl error)
        {
            if (email.Length > EmailMaxLength)
            {
                error = new ErrorMdl(12, "Email is too long");
                return false;
            }
            error = null;
            return true;
        }

        public bool Try_EmailValidation_MinLength(string email, out ErrorMdl error)
        {
            if (email.Length < EmailMinLength)
            {
                error = new ErrorMdl(11, "Email is too short");
                return false;
            }
            error = null;
            return true;
        }

        public bool Try_EmailValidation_Regex(string email, out ErrorMdl error)
        {
            if (!Regex.IsMatch(email, RegexConst.EmailValidator))
            {
                error = new ErrorMdl(11, "Email is not valid");
                return false;
            }
            error = null;
            return true;
        }

        public bool Try_PasswordValidation_Required(string password, out ErrorMdl error)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                error = new ErrorMdl(1, "Password is required");
                return false;
            }
            error = null;
            return true;
        }

        public bool Try_PasswordValidation_MinLength(string password, out ErrorMdl error)
        {
            if (password.Length < PasswordMinLength)
            {
                error = new ErrorMdl(11, "Password is too short");
                return false;
            }
            error = null;
            return true;
        }

        public bool Try_PasswordValidation_MaxLength(string password, out ErrorMdl error)
        {
            if (password.Length > PasswordMaxLength)
            {
                error = new ErrorMdl(12, "Password is too long");
                return false;
            }
            error = null;
            return true;
        }

        public bool Try_PasswordValidation_UpperCase(string password, out ErrorMdl error)
        {
            if (!password.Any(char.IsUpper))
            {
                error = new ErrorMdl(12, "Password must contain at least one uppercase, or capital, letter (ex: A, B, etc.)");
                return false;
            }
            error = null;
            return true;
        }

        public bool Try_PasswordValidation_LowerCase(string password, out ErrorMdl error)
        {
            if (!password.Any(char.IsLower))
            {
                error = new ErrorMdl(12, "Your password must contain at least one lowercase letter");
                return false;
            }
            error = null;
            return true;
        }

        public bool Try_PasswordValidation_Number(string password, out ErrorMdl error)
        {
            if (!password.Any(char.IsDigit))
            {
                error = new ErrorMdl(12, "Your password must contain at least one number digit (ex: 0, 1, 2, 3, etc.)");
                return false;
            }
            error = null;
            return true;
        }

        public bool Try_PasswordValidation_Special(string password, out ErrorMdl error)
        {

            var specials = new char[] { '!', '@', '#', '$', '%', '^', '&', '*' };
            if (password.IndexOfAny(specials) == -1)
            {
                error = new ErrorMdl(12, "Your password must contain at least one special symbol (ex: !@#$%^&* etc.)");
                return false;
            }
            error = null;
            return true;
        }
    }
}
