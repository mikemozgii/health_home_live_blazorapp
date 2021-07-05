using HHL.Auth.Core.DataAccess;
using HHL.Auth.Core.Interfaces;
using HHL.Auth.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace HHL.Auth.Core
{
    public static class AuthServiceCollectionExtension
    {
        public static IServiceCollection AddAuthDI(this IServiceCollection services)
        {

            services.AddScoped<IAuthQueryExecutionSvc, AuthQueryExecutionSvc>();

            services.Scan(
    x =>
    {
        var entryAssembly = Assembly.GetEntryAssembly();
        var referencedAssemblies = entryAssembly.GetReferencedAssemblies().Select(Assembly.Load);
        var assemblies = new List<Assembly> { entryAssembly }.Concat(referencedAssemblies);
        x.FromAssemblies(assemblies)
            .AddClasses(classes => classes.AssignableTo(typeof(IAuthDataAccessWorker)))
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
            .AddClasses(classes => classes.AssignableTo(typeof(AuthBaseRepository<>)))
                .AsSelf()
                .WithScopedLifetime();
    });
            return services;
        }
    }
}
