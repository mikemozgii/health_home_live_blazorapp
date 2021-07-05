using Dapper.NodaTime;
using HHL.Auth.Core.Models;
using HHL.Auth.Core.Services;
using HHL.Common;
using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace HHL.Auth.Core.Handlers
{
    public static class AuthConfigHdr
    {

#if DEBUGPROD
        public static Hashtable Config = ConfigReader.ReadConfig(@"C:\Configs\system.production.config");
#elif DEBUG
        public static Hashtable Config = ConfigReader.ReadConfig(@"C:\HHL\Configs\development.config");
#else
        public static Hashtable Config = ConfigReader.ReadConfig(@"C:\Configs\system.production.config");
#endif


        public static string BcryptAccountPasswordSalt = Config.Get("auth:passwordsalt");
        public static string Environment = Config.Get("auth:environment");
        public static string Db_Server = Config.Get("auth:db_server");
        public static string Db_Name = Config.Get("auth:db_name");
        public static string Db_UserId = Config.Get("auth:db_userId");
        public static string Db_Password = Config.Get("auth:db_password");
        public static string ConnStr { get; set; } = $"Host={Db_Server};Database={Db_Name};Username={Db_UserId};Password={Db_Password}";


        public static string JwtKey = Config.Get("auth:JwtKey");
        public static string JwtIssue = Config.Get("auth:JwtIssue");
        public static string JwtAudience = Config.Get("auth:JwtAudience");

        public static string JwtAccountClaimName = "account";
        public static string JwtRoleClaimName = "role";


        public static string Redis_Server = Config.Get("auth:redis_server");
        public static int Redis_Port = Convert.ToInt32(Config.Get("auth:redis_port"));
        public static string Redis_Password = Config.Get("auth:redis_password");


        public static string AuthCookieName= Config.Get("auth:AuthCookieName");
        public static string AuthHeaderName = Config.Get("auth:AuthHeaderName");
        public static int AuthCookieExpiresHours = Convert.ToInt32(Config.Get("auth:AuthCookieExpiresHours"));

        

        static AuthConfigHdr()
        {
          
        }

    }
}
