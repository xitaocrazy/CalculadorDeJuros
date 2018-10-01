using Xunit;
using CalculadorDeJurosApi.Attributes;
using CalculadorDeJurosApi.Constraints;
using CalculadorDeJurosApiTests.Helpers;

namespace CalculadorDeJurosApiTests.Attributes
{
    public class RequiredFromQueryAttributeTests
    {
        [Fact]
        public void Apply_deve_adicionar_RequiredFromQueryActionConstraint_quando_selectors_nao_eh_null()
        {
            var tipoEsperado = typeof(RequiredFromQueryActionConstraint);
            var parametro = TestHelper.GetParameterModelParaTestes();
            var acao = TestHelper.GetActionModelParaTestes();
            parametro.Action = acao;
            var atributo = new RequiredFromQueryAttribute();

            atributo.Apply(parametro);

            var restricoes = acao.Selectors[0].ActionConstraints;
            var tipoRestricao = restricoes[0].GetType();            
            Assert.Equal(tipoEsperado, tipoRestricao);
        }
    }    
}
