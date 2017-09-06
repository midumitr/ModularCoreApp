using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.Extensions.DependencyInjection;
using Startup;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Swagger;

namespace ParallelPipelines
{
    public class ParallelMiddleware
    {
        private RequestDelegate _next;
        private IApplicationBuilder _rootApp;

        public ParallelMiddleware(RequestDelegate next, IApplicationBuilder app)
        {
            _next = next;
            _rootApp = app;
        }

        public async Task Invoke(HttpContext context)
        {
            var parallel = BuildPipeline();

            await parallel(context);

        }

        public RequestDelegate BuildPipeline()
        {
            var webHost = WebHost.CreateDefaultBuilder().
                ConfigureServices(x =>
                {
                    x.AddMvc();
                    x.AddSwaggerGen(c =>
                    {
                        c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
                    });
                   
                    var dir = new DirectoryInfo(Directory.GetCurrentDirectory());



                    foreach (var item in dir.GetFileSystemInfos("Module.*.dll", SearchOption.AllDirectories))
                    {
                        var assembly = Assembly.LoadFrom(item.FullName);



                        x.AddMvc().AddApplicationPart(assembly).AddControllersAsServices();

                        foreach (var attribute in assembly.GetCustomAttributes<ProviderStartupAttribute>())
                        {
                            var hostingStartup = (IProvidersStartup)Activator.CreateInstance(attribute.ProviderStartupType);
                            hostingStartup.ConfigureServices(x);
                        }

                    }



                })
                .UseStartup<EmptyStartup>().Build();
            var serviceProvider = webHost.Services;

            var branchBuilder = _rootApp.New();
            branchBuilder.ApplicationServices = serviceProvider;

            branchBuilder.Use(async (ccontext, next) =>
            {
                var factory = serviceProvider.GetRequiredService<IServiceScopeFactory>();

                // Store the original request services in the current ASP.NET context.
                ccontext.Items[typeof(IServiceProvider)] = ccontext.RequestServices;

                try
                {
                    using (var scope = factory.CreateScope())
                    {
                        ccontext.RequestServices = scope.ServiceProvider;

                        await next();
                    }
                }

                finally
                {
                    ccontext.RequestServices = null;
                }
            });
            branchBuilder.Run(_next);

            return branchBuilder.Build();


        }


        private class EmptyStartup
        {
            public void ConfigureServices(IServiceCollection services)
            {
            }

            public void Configure(IApplicationBuilder app) { }
        }
    }
}
