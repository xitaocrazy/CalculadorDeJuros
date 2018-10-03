using CalculadorDeJurosApi.Extensions;
using Xunit;

namespace CalculadorDeJurosApiTests.Extensions
{
    public class DecimalExtensionsTests
    {
        [Theory]        
        [InlineData(1.123456, 1, 1.1)]
        [InlineData(1.123456, 2, 1.12)]
        [InlineData(1.123456, 3, 1.123)]
        [InlineData(1.123456, 4, 1.1234)]
        [InlineData(1.123456, 5, 1.12345)]
        [InlineData(1.101, 2, 1.10)]
        public void Trunque_deve_calcular_o_valor_esperado(decimal valor, int precisao, decimal resultadoEsperado)
        {
            var resultado = valor.Trunque(precisao);

            Assert.Equal(resultadoEsperado, resultado);
        }
    }
}