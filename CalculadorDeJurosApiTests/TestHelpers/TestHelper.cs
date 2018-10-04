using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Primitives;
using NLog;
using NLog.Config;
using NLog.Targets;
using Swashbuckle.AspNetCore.Swagger;

namespace CalculadorDeJurosApiTests.Helpers
{
    public static class TestHelper
    {
        public static ParameterModel GetParameterModelParaTestes() 
        {
            var metodo = GetMethodInfoParaTestes();
            var parametroInfo = metodo.GetParameters()[0];            
            var lista = new List<Object>();                                    
            var parametro = new ParameterModel(parametroInfo, lista);
            return parametro;
        }

        public static ActionModel GetActionModelParaTestes() 
        {
            var metodo = GetMethodInfoParaTestes();
            var lista = new List<Object>();             
            var acao = new ActionModel(metodo, lista);
            var selector = new SelectorModel();
            acao.Selectors.Add(selector);
            return acao;
        }

        public static MethodInfo GetMethodInfoParaTestes() 
        {
            var tipo = typeof(ClassTeste);
            var metodo = tipo.GetMethod("MetodoTeste");
            return metodo;
        }

        public static ActionConstraintContext GetActionConstraintContextParaTestes()
        {
            var httpContext = new DefaultHttpContext();
            var routeContext= new RouteContext(httpContext);
            var contexto = new ActionConstraintContext()
            {
                RouteContext = routeContext
            };            
            
            return contexto;
        }

        public static void SetQueryToActionConstraintContext(ActionConstraintContext contexto, Dictionary<string,StringValues> dicionario)
        {
            var query = new QueryCollection(dicionario);
            contexto.RouteContext.HttpContext.Request.Query = query;
        }

        public static Operation GetNewOperation(string resumo, string descricao)
        {
            return new Operation 
            {
                Summary = resumo,
                Description = descricao,
                Parameters = new List<IParameter>()
                {
                    new BodyParameter(){
                        Description = "[Required]Este é obrigatório"
                    },
                    new BodyParameter(){
                        Description = "Este não é obrigatório"
                    }
                }
            };
        }        


        private class ClassTeste 
        {
            public decimal MetodoTeste(decimal parametroTeste) {
                return 200;
            }
        }


        public interface DelegateMock
        {
            Task RequestDelegate(HttpContext context);
        }
    }    
}