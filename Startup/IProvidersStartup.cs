using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Startup
{
    public interface IProvidersStartup
    {
        void Configure(IApplicationBuilder app);
        void ConfigureServices(IServiceCollection services);

    }
}
