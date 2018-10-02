using System.Collections.Generic;
using CalculadorDeJurosApi.Constraints;
using CalculadorDeJurosApiTests.Helpers;
using Microsoft.Extensions.Primitives;
using Xunit;

namespace CalculadorDeJurosApiTests.Constraints
{
    public class RequiredFromQueryActionConstraintTests{
        [Theory]
        [InlineData("parametroCerto", true)]
        [InlineData("parametroErrado", false)]
        public void Accept_deve_retornar_o_resultado_esperado(string parametro, bool resultadoEsperado)
        {
            const string parametroEsperado = "parametroCerto";
            var restricao = new RequiredFromQueryActionConstraint(parametroEsperado);
            var contexto = TestHelper.GetActionConstraintContextParaTestes();
            var dicionario = new Dictionary<string,StringValues>();
            dicionario.Add(parametro, parametro);
            TestHelper.SetQueryToActionConstraintContext(contexto, dicionario);

            var resultado = restricao.Accept(contexto);
            
            Assert.Equal(resultadoEsperado, resultado);
        }
    }
}