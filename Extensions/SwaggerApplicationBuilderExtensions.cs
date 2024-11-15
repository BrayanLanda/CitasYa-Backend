using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CordiSimple.Extensions
{
    public static class SwaggerApplicationBuilderExtensions
    {
         public static IApplicationBuilder UseSwaggerServices(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Any name");
                c.RoutePrefix = string.Empty; // Esto hace que Swagger esté disponible en la raíz
            });

            return app;
        }
    }
}