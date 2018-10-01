using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CalculadorDeJurosApi.Filters
{
    public class FormateComentariosXmlFilter : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            ConfigureCamposDaOperacao(operation);
        }

        public void Apply(Operation operation, OperationFilterContext context)
        {
            ConfigureCamposDaOperacao(operation);
        }

        private void ConfigureCamposDaOperacao(Operation operation){
            operation.Description = FormateTexto(operation.Description);
            operation.Summary = FormateTexto(operation.Summary);
            ConfigureParametrosDaOperacao(operation);
        }

        private string FormateTexto(string text)
        {
            if (text == null) {
                return null;
            }

            try{
                var resultString = Regex.Replace(text, @"(^[ \t]+)(?![^<]*>|[^>]*<\/)", "", RegexOptions.Multiline);
                return resultString;
            }
            catch
            {
                return text;
            }
        }

        private void ConfigureParametrosDaOperacao(Operation operation){
            const string req = "[Required]";
            if (operation.Parameters == null || !operation.Parameters.Any()){
                return;
            }
            foreach(var param in operation.Parameters){
                if (param.Description.Contains(req)){
                    param.Required = true;
                    param.Description = param.Description.Replace(req, string.Empty);
                }
            }
        }
    }
}