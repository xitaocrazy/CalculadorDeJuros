using CalculadorDeJurosApi.Models;
using Xunit;

namespace CalculadorDeJurosApiTests.Models
{
    public class ExcecaoPersonalizadaTests
    {
        [Fact]
        public void ToString_deve_retornar_o_valor_esperado()
        {
            const string descricaoEsperada = "{\"StatusCode\":500,\"Mensagem\":\"Mensagem\",\"StackTrace\":\"StackTrace\"}";
            var excecao = new ExcecaoPersonalizada
            {
                StatusCode = 500,
                Mensagem = "Mensagem",
                StackTrace = "StackTrace"
            };

            var descricao = excecao.ToString();

            Assert.Equal(descricaoEsperada, descricao);
        }
    }
}