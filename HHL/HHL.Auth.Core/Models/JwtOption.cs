using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HHL.Auth.Core.Models
{
    public class JwtOption
    {
        public string Issuer { get; set; }
        public string Subject { get; set; }
        public string Audience { get; set; }
        public DateTime Expiration => IssuedAt.Add(ValidFor);
        public DateTime NotBefore => DateTime.UtcNow;
        public DateTime IssuedAt => DateTime.UtcNow;
        public TimeSpan ValidFor { get; set; } = TimeSpan.FromHours(24);
       // public Func<Task<string>> JtiGenerator =>() => Task.FromResult(Guid.NewGuid().ToString());
        public SigningCredentials SigningCredentials { get; set; }
        public SecurityKey SignKey { get; set; }
        public IEnumerable<Claim> Claims { get; set; }
        public SymmetricSecurityKey SigningKey { get; set; }



    public JwtOption(string key, string basicIssuer, string basicAudience, string subject = null)
        {
            Issuer = basicIssuer;
            Audience = basicAudience;
            Subject = subject;
            SignKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(key));
            SigningCredentials = new SigningCredentials(SignKey, SecurityAlgorithms.HmacSha256Signature);
           // SigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));
        }
    }
}
