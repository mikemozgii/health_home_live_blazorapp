using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using SecurityAlgorithms = Microsoft.IdentityModel.Tokens.SecurityAlgorithms;
using SecurityToken = Microsoft.IdentityModel.Tokens.SecurityToken;
using SigningCredentials = Microsoft.IdentityModel.Tokens.SigningCredentials;
using Microsoft.IdentityModel.Tokens;
using HHL.Auth.Core.Models;
using System.Collections.ObjectModel;

namespace HHL.Auth.Core.Services
{
    public class JwtSvc
    {
        public string Create(JwtOption option)
        {
            var token = new JwtSecurityToken(
                issuer: option.Issuer,
                audience: option.Audience,
                claims: option.Claims,
                expires: option.Expiration,
                notBefore: option.NotBefore,
                signingCredentials: option.SigningCredentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //public SecurityToken Read(string basicToken)
        //{
        //    return new JwtSecurityTokenHandler().ReadToken(basicToken);
        //}

        public bool Validate(string encodedJwt, JwtOption option, out List<Claim> claims)
        {
           

            claims = new List<Claim>();
            if (string.IsNullOrWhiteSpace(encodedJwt)) return false;
            try
            {
                var basicValidationParams = new TokenValidationParameters
                {
                    ValidIssuer = option.Issuer,
                    ValidAudiences = new Collection<string> { option.Audience },
                    IssuerSigningKey = option.SignKey,
                    ClockSkew = new TimeSpan(0, 0, 0, 0, 0)

                };
                var claimPrinciplces = new JwtSecurityTokenHandler().ValidateToken(encodedJwt, basicValidationParams, out var validatedToken);
                claims.AddRange(claimPrinciplces.Claims);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
