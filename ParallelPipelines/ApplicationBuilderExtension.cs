using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParallelPipelines
{
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder UseParallel(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }


            return app.UseMiddleware<ParallelMiddleware>(app);
        }
    }
   
}
