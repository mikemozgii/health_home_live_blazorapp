using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using HHL.WebApp.Components;
using HHL.WebApp.Services;
using HHL.Auth.Core.Services;
using HHL.Auth.Core.Handlers;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using AutoMapper;
using HHL.Core.Services;
using HHL.Core.Handlers;
using System.Net.Http;
using HHL.FileReader;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Routing;
using HHL.WebApp.Handlers;
using HHL.PostreSQL.Core.Services;
using HHL.Core.DataAccess;
using System.Reflection;
using HHL.Auth.Core.DataAccess;
using HHL.Auth.Core;
using HHL.Core;
using HHL.Core.Interfaces;

namespace HHL.WebApp
{
    public class Startup
    {
        public object InstantDataHdr { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddHttpContextAccessor();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<HttpContextAccessor>();
            services.AddHttpClient();
            services.AddScoped<HttpClient>();
            services.AddScoped<UiJsHandler>();
            services.AddScoped<IFileReaderService, FileReaderService>();
            services.AddAutoMapper();
            services.AddMvc().AddNewtonsoftJson();

            //services.AddScoped<HHLAuthSessionSvc>();

            services.AddFileReaderService(options => options.InitializeOnFirstCall = true);
            services.AddScoped<IQueryExecutionSvc, QueryExecutionSvc>();


            services.AddScoped<HHLAuthSessionSvc>();
            services.AddScoped<IHHLQueryExecutionSvc, HHLQueryExecutionSvc>();
            services.AddScoped<IHHLDataAccess, HHLDataAccessSvc>();

            services.AddAuthDI();
            services.AddHHLDI();
            services.AddSingleton<InstantDatahandler>();


            HHLConfigHdr.Init();
            //InstantDatahandler.Init();

            //was used to increase balzor buffer size
            //services.Configure<FormOptions>(options =>
            //{
            //    options.ValueCountLimit = 10; //default 1024
            //    options.ValueLengthLimit = int.MaxValue; //not recommended value
            //    options.MultipartBodyLengthLimit = long.MaxValue; //not recommended value
            //});

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            //app.UseRouting(routes =>
            //{
            //    routes.MapRazorPages();
            //    routes.MapControllers();
            //    routes.MapBlazorHub<App>("app");
            //    endpoints.MapFallbackToPage("/_Host");
            //});
            
            app.UseEndpoints(endpoints =>
            {

            //ComponentHub.DefaultPath,
                //o =>
                //{
                //    o.ApplicationMaxBufferSize = 102400000; // larger size
                //    o.TransportMaxBufferSize = 102400000; // larger size
                //});

//            endpoints.MapHub<ComponentHub>(
//ComponentHub.DefaultPath,
//o =>
//{
//o.ApplicationMaxBufferSize = 102400000; // larger size
//                o.TransportMaxBufferSize = 102400000; // larger size
//            });

                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });

        }



    }
}
