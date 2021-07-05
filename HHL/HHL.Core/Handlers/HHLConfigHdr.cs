using HHL.Common;
using Npgsql;
using Stripe;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;

namespace HHL.Core.Handlers
{
    public static class HHLConfigHdr
    {

#if DEBUGPROD
        public static Hashtable Config = ConfigReader.ReadConfig(@"C:\Configs\system.production.config");
#elif DEBUG
        public static Hashtable Config = ConfigReader.ReadConfig(@"C:\HHL\Configs\development.config");
#else
        public static Hashtable Config = ConfigReader.ReadConfig(@"C:\Configs\system.production.config");
#endif

        public static string Environment = Config.Get("hhl:environment");
        public static string Db_Server = Config.Get("hhl:db_server");
        public static string Db_Name = Config.Get("hhl:db_name");
        public static string Db_UserId = Config.Get("hhl:db_userId");
        public static string Db_Password = Config.Get("hhl:db_password");
        public static string ConnStr { get; set; } = $"Host={Db_Server};Database={Db_Name};Username={Db_UserId};Password={Db_Password}";

        public static decimal PricePerHourSell { get; set; } = (decimal)25.00;
        public static decimal PricePerHourCost { get; set; } = (decimal)20.00;

        public static string Stripe_Publishable_Apikey = Config.Get("hhl:stripe_publishable_apikey");
        public static string Stripe_Secret_Apikey = Config.Get("hhl:stripe_secret_apikey");
        public static string Hsot_Name = Config.Get("host_name");


        public static string notify_smtp_Host = Config.Get("notify:smtp_Host");
        public static int notify_smtp_Port = Convert.ToInt32(Config.Get("notify:smtp_Port"));
        public static string notify_smtp_UserName = Config.Get("notify:smtp_UserName");
        public static string notify_smtp_From = Config.Get("notify:smtp_From");
        public static string notify_smtp_Password = Config.Get("notify:smtp_Password");

        public static string notify_smtp_buckup_Host = Config.Get("notify:smtp_backup_Host");
        public static int notify_smtp_buckup_Port = Convert.ToInt32(Config.Get("notify:smtp_backup_Port"));
        public static string notify_smtp_buckup_UserName = Config.Get("notify:smtp_backup_UserName");
        public static string notify_smtp_buckup_From = Config.Get("notify:smtp_backup_From");
        public static string notify_smtp_buckup_Password = Config.Get("notify:smtp_backup_Password");

        public static void Init()
        {

            StripeConfiguration.SetApiKey(Stripe_Secret_Apikey);
            //StripeConfiguration.
        }



    }
}
