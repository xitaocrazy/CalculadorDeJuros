using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace CalculadorDeJurosApiIntegrationTests.Tests
{
    public class CalculadorDeJurosControllerTests : IClassFixture<WebApplicationFactory<CalculadorDeJurosApi.Startup>>
    {
        private const string _calculaJuros = "api/calculadordejuros/calculajuros";
        private const string _showMeTheCode = "api/calculadordejuros/showmethecode";
        private readonly WebApplicationFactory<CalculadorDeJurosApi.Startup> _factory;


        public CalculadorDeJurosControllerTests(WebApplicationFactory<CalculadorDeJurosApi.Startup> factory)
        {
            _factory = factory;
        }


        [Theory]
        [InlineData("10", "2", HttpStatusCode.OK)]
        [InlineData("a", "b", HttpStatusCode.BadRequest)]
        [InlineData("10", "99999", HttpStatusCode.InternalServerError)]        
        public async Task CalculaJuros_deve_retornar_o_status_esperado(string valorInicial, string meses, HttpStatusCode statusEsperado)
        {            
            var client = _factory.CreateClient();
            var serviceUrl = $"{_calculaJuros}?valorInicial={valorInicial}&meses={meses}";            

            var response = await client.PostAsync(serviceUrl, null);
            
            Assert.Equal(statusEsperado, response.StatusCode);
        }

        [Theory]
        [InlineData("10", "2", "10.2")]
        [InlineData("a", "b", "{\"meses\":[\"The value 'b' is not valid.\"],\"valorInicial\":[\"The value 'a' is not valid.\"]}")]
        [InlineData("10", "99999", "{\"StatusCode\":500,\"Mensagem\":\"Value was either too large or too small for a Decimal.\",\"StackTrace\":\"")]        
        public async Task CalculaJuros_deve_retornar_o_valor_esperado(string valorInicial, string meses, string valorEsperado)
        {            
            var client = _factory.CreateClient();
            var serviceUrl = $"{_calculaJuros}?valorInicial={valorInicial}&meses={meses}";            

            var response = await client.PostAsync(serviceUrl, null);

            var valorRecebido = await response.Content.ReadAsStringAsync();
            Assert.Contains(valorEsperado, valorRecebido);
        }

        [Fact]       
        public async Task ShowMeTheCode_deve_retornar_o_status_esperado()
        {            
            var client = _factory.CreateClient();

            var response = await client.GetAsync(_showMeTheCode);
            
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]       
        public async Task ShowMeTheCode_deve_retornar_o_valor_esperado()
        {            
            var client = _factory.CreateClient();            
            const string valorEsperado = "\"https://github.com/xitaocrazy/CalculadorDeJuros\"";

            var response = await client.GetAsync(_showMeTheCode);
            
            var valorRecebido = await response.Content.ReadAsStringAsync();
            Assert.Equal(valorEsperado, valorRecebido);
        }
    }
}