using HHL.Common;
using HHL.Core.Handlers;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace HHL.Core.Services
{
    public class SmtpSvc 
    {
        public  SmtpClient Get_SmptClient()
        {
            return new SmtpClient()
            {
                Host = HHLConfigHdr.notify_smtp_Host,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential(HHLConfigHdr.notify_smtp_UserName, HHLConfigHdr.notify_smtp_Password),
                Port = HHLConfigHdr.notify_smtp_Port,
                EnableSsl = true,
                Timeout = 5 * 60 * 1000

            };
        }

        public  SmtpClient Get_Backup_SmptClient()
        {
           return new SmtpClient()
            {
                Host = HHLConfigHdr.notify_smtp_buckup_Host,
                Credentials = new System.Net.NetworkCredential(HHLConfigHdr.notify_smtp_buckup_UserName, HHLConfigHdr.notify_smtp_buckup_Password),
                Port = HHLConfigHdr.notify_smtp_buckup_Port,
                EnableSsl = true,
                Timeout = 5 * 60 * 1000
            };

        }

        public MailMessage Resolve_MailMessage(string email, string subject, string body, string fromAddress)
        {
            return Resolve_MailMessage(new List<string>() { email }, subject.Trim(), body, fromAddress);
        }

        public MailMessage Resolve_MailMessage(List<string> emails, string subject, string body, string fromAddress)
        {

            if (emails.IsNullOrEmpty()) return null;

            var mailMessage = new MailMessage
            {
                From = new MailAddress(fromAddress),
                Subject = subject,
                Body = body,
                IsBodyHtml = true

            };

            mailMessage.Subject = subject;
            mailMessage.Body = body;

            foreach (var email in emails)
            {
                if (!string.IsNullOrWhiteSpace(email))
                {
                    mailMessage.To.Add(new MailAddress(email));
                }
            }

            if (mailMessage.To.IsNullOrEmpty()) return null;

            return mailMessage;

        }


        //public async Task SendHelloWorldEmail(string email, string name)
        //{
        //    string template = "Templates.HelloWorldTemplate";

        //    RazorParser renderer = new RazorParser(typeof(HHL.Core).Assembly);
        //    var body = renderer.UsingTemplateFromEmbedded(template,
        //        new HelloWorldViewModel { Name = name });

        //    await SendEmailAsync(email, "Email Subject", body);
        //}
    }
}
