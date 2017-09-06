using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Startup;

[assembly: ProviderStartup(typeof(Module.B.ProviderStartup))]
namespace Module.B
{
    // To be able to build as <OutputType>Exe</OutputType>
    //internal class Program { public static void Main() { } }
    public class ProviderStartup : IProvidersStartup
    {
        public void Configure(IApplicationBuilder app)
        {
           
        }
       
        public void ConfigureServices(IServiceCollection services)
        {
            services.TryAddTransient<ITestClassB, TestClassB>();
        }
    }
}