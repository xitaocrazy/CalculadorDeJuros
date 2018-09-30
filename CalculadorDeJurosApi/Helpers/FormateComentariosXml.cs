using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CalculadorDeJurosApi.Helpers
{
    public class FormateComentariosXml : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            operation.Description = Formate(operation.Description);
            operation.Summary = Formate(operation.Summary);
        }

        public void Apply(Operation operation, OperationFilterContext context)
        {
            operation.Description = Formate(operation.Description);
            operation.Summary = Formate(operation.Summary);
        }

        private string Formate(string text)
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
    }
}