using HHL.Core.EmailTemplates;
using HHL.Core.Handlers;
using HHL.Core.Helpers;
using HHL.Core.Interfaces;
using HHL.Core.Models.EmailTemlates;
using Microsoft.CodeAnalysis;
using RazorLight;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HHL.Core.Services
{
    public class EmailNotifySvc : HHLDataAccessSvc
    {
        private readonly IRazorLightEngine _razorEngine;
        private readonly SmtpSvc _smtpSvc;
        private readonly SmtpClient _SmtpClient;
        private readonly InstantDatahandler _InstantDatahandler;
        

        public EmailNotifySvc(IHHLQueryExecutionSvc queryExecutionSvc, IRazorLightEngine razorEngine, SmtpSvc smtpSvc, InstantDatahandler instantDatahandler) : base(queryExecutionSvc)
        {
            _razorEngine = razorEngine;
            _smtpSvc = smtpSvc;
            _SmtpClient = smtpSvc.Get_SmptClient();
            _InstantDatahandler = instantDatahandler;
        }

        public async Task<bool> SendHelloWorld()
        {
            //string templatePath = "HelloWorldTemplate.cshtml";
            //var metadataReference = MetadataReference.CreateFromFile(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
            var metadataReference = MetadataReference.CreateFromFile(typeof(EmailTemplateLoader).Assembly.Location);
            var engine = new RazorLightEngineBuilder().AddMetadataReferences(metadataReference).Build();
            string result = await _razorEngine.CompileRenderAsync(Guid.NewGuid().ToString(), _InstantDatahandler.All_EmailTemplates["HelloWorldTemplate"], new HelloWorldViewModel { Name = "Prive Mike" });
            var mailMessage = _smtpSvc.Resolve_MailMessage("khokhlov.mikhail@gmail.com", "Test Subject", result, HHLConfigHdr.notify_smtp_From);
            await _SmtpClient.SendMailAsync(mailMessage);
            return true;
        }

        string GenerateFileAssemblyPath(string template, Assembly assembly)
        {
            string assemblyName = assembly.GetName().Name;
            return string.Format("{0}.{1}.{2}", assemblyName, template, "cshtml");
        }
    }
}
