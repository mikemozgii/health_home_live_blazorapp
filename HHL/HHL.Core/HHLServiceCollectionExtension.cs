
using HHL.Core.DataAccess;
using HHL.Core.EmailTemplates;
using HHL.Core.Interfaces;
using HHL.Core.Services;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using RazorLight;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace HHL.Core
{
    public static class HHLServiceCollectionExtension
    {
        public static IServiceCollection AddHHLDI(this IServiceCollection services)
        {

            services.AddScoped<SmtpSvc>();

            //var metadataReference = MetadataReference.CreateFromFile();
           

            //var path = typeof(EmailTemplateLoader).Assembly.Location.Replace("HHL.Core.dll", "");
            services.AddScoped<IRazorLightEngine>(sp =>
            {
                var engine = new RazorLightEngineBuilder().UseFilesystemProject(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)).Build();
                return engine;
            });

            services.AddScoped<IRazorLightEngine>(sp =>
            {
                var engine = new RazorLightEngineBuilder()
                .UseEmbeddedResourcesProject(typeof(EmailTemplateLoader))
                    .Build();
                return engine;
            });


            services.Scan(
    x =>
    {
        var entryAssembly = Assembly.GetEntryAssembly();
        var referencedAssemblies = entryAssembly.GetReferencedAssemblies().Select(Assembly.Load);
        var assemblies = new List<Assembly> { entryAssembly }.Concat(referencedAssemblies);
        x.FromAssemblies(assemblies)
            .AddClasses(classes => classes.AssignableTo(typeof(IHHLDataAccess)))
                .AsSelf()
                .WithScopedLifetime();
    });



            services.Scan(
    x =>
    {
        var entryAssembly = Assembly.GetEntryAssembly();
        var referencedAssemblies = entryAssembly.GetReferencedAssemblies().Select(Assembly.Load);
        var assemblies = new List<Assembly> { entryAssembly }.Concat(referencedAssemblies);

        x.FromAssemblies(assemblies)
            .AddClasses(classes => classes.AssignableTo(typeof(HHLBaseRepository<>)))
                .AsSelf()
                .WithScopedLifetime();
    });
            return services;
        }
    }
}
