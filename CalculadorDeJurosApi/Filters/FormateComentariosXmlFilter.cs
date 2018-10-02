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

        private string FormateTexto(string texto)
        {
            if (texto == null) {
                return null;
            }
            
            try{
                var textoFormatado = RemovaEspacosDoInicio(texto);
                textoFormatado = RemovaTagsDeComentario(textoFormatado);
                textoFormatado = FormateBlocosPreFormatados(textoFormatado);
                return textoFormatado;
            }
            catch
            {
                return texto;
            }
        }

        private string RemovaEspacosDoInicio(string texto)
        {
            var textoSemEspacosNoInicio = Regex.Replace(texto, @"(^[ \t]+)(?![^<]*>|[^>]*<\/)", string.Empty, RegexOptions.Multiline);            
            return textoSemEspacosNoInicio;
        }

        private string RemovaTagsDeComentario(string texto)
        {
            var textoFormatado = Regex.Replace(texto, @"<!--", string.Empty, RegexOptions.Multiline);
            textoFormatado = Regex.Replace(textoFormatado, @"-->", string.Empty, RegexOptions.Multiline);
            return textoFormatado;
        }

        private string FormateBlocosPreFormatados(string texto)
        {
            const string pattern = @"<pre\b[^>]*>(.*?)</pre>";
            foreach (Match match in Regex.Matches(texto, pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Multiline ))
            {
                var blocoFormatado = FormateBlocoPreFormatado(match.Value);
                texto = texto.Replace(match.Value, blocoFormatado);
            }
            return texto;
        }

        private string FormateBlocoPreFormatado(string blocoPreFormatado)
        {
            var linhas = blocoPreFormatado.Split('\n');
            if (linhas.Length < 2)
            {
                return blocoPreFormatado;
            }
            else
            {
                var primeiraLinha = linhas[1];
                var linhaSemTabInicial = primeiraLinha.TrimStart(' ', '\t');
                var padding = primeiraLinha.Length - linhaSemTabInicial.Length;
                RemovaPadding(linhas, padding);
                var formattedPreBlock = string.Join("", linhas);
                return formattedPreBlock; 
            }

        }

        private void RemovaPadding(string[] linhas, int padding)
        {
            for (int i = 1; i < linhas.Length-1; i++)
            {
                linhas[i] = linhas[i].Substring(padding);
            }
        }

        private void ConfigureParametrosDaOperacao(Operation operation)
        {
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