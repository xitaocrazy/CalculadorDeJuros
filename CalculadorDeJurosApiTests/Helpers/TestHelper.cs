using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

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

        private class ClassTeste 
        {
            public decimal MetodoTeste(decimal parametroTeste) {
                return 200;
            }
        }
    }    
}