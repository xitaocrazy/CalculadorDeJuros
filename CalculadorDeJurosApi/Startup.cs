using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;
using CalculadorDeJurosApi.Filters;
using CalculadorDeJurosApi.Services;
using CalculadorDeJurosApi.Wrappers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using NLog;
using CalculadorDeJurosApi.Extensions;

namespace CalculadorDeJurosApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            TenteLerConfiguracaoDoNlog();
            Configuration = configuration;
        }        

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
            services.AddTransient<ICalculadorDeJuros, CalculadorDeJurosService>();
            services.AddSingleton<ILoggerManager, LoggerManager>();

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
            app.ConfigureCustomExceptionMiddleware();
            
            app.UseStaticFiles();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CalculadorDeJurosApi");
                c.RoutePrefix = string.Empty;
            });

            app.UseMvc();
        }


        private void TenteLerConfiguracaoDoNlog()
        {
            if (!TenteLerDoDiretorioDaSolution())
            {
                TenteLerDoDiretorioDoProjeto();
            }
        }

        private bool TenteLerDoDiretorioDaSolution()
        {
            var nlogConfigurationPath = String.Concat(Directory.GetCurrentDirectory(), "/CalculadorDeJurosApi/nlog.config");
            if (File.Exists(nlogConfigurationPath))
            {
                LogManager.LoadConfiguration(nlogConfigurationPath);
                return true;
            }
            return false;
        }

        private bool TenteLerDoDiretorioDoProjeto()
        {
            var nlogConfigurationPath = String.Concat(Directory.GetCurrentDirectory(), "/nlog.config");
            if (File.Exists(nlogConfigurationPath))
            {
                LogManager.LoadConfiguration(nlogConfigurationPath);
                return true;
            }
            return false;
        }
    }
}
