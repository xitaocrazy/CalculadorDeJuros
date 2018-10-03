using CalculadorDeJurosApi.Services;
using Xunit;

namespace CalculadorDeJurosApiTests.Services
{
    public class CalculadorDeJurosServiceTests
    {
        [Theory]
        [InlineData(200, 8, 216.57)]
        [InlineData(1500.50, 2, 1530.66)]
        [InlineData(100, 5, 105.10)]
        public void CalculeJuros_deve_calcular_o_valor_esperado(decimal valorInicial, int meses, decimal resultadoEsperado)
        {
            var service = new CalculadorDeJurosService();

            var resultado = service.CalculeJuros(valorInicial, meses);

            Assert.Equal(resultadoEsperado, resultado);
        }
    }
}