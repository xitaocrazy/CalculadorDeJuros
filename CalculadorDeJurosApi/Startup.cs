using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;
using CalculadorDeJurosApi.Filters;
using CalculadorDeJurosApi.Services;

namespace CalculadorDeJurosApi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
            services.AddTransient<ICalculadorDeJuros, CalculadorDeJurosService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "CalculadorDeJurosApi",
                    Description = "Uma Web API ASP.NET Core com documentação em Swagger.",
                    Contact = new Contact
                    {
                        Name = "Daniel de Souza Martins",
                        Email = string.Empty,
                        Url = "https://www.linkedin.com/in/daniel-de-souza-martins/"
                    },
                    License = new License
                    {
                        Name = "MIT",
                        Url = "https://opensource.org/licenses/MIT"
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.OperationFilter<FormateComentariosXmlFilter>();
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CalculadorDeJurosApi");
                c.RoutePrefix = string.Empty;
            });

            app.UseMvc();
        }
    }
}
