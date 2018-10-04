using System;
using CalculadorDeJurosApi.Controllers;
using CalculadorDeJurosApi.Services;
using CalculadorDeJurosApi.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CalculadorDeJurosApiTests.Controllers
{
    public class CalculadorDeJurosControllerTests
    {
        private const decimal _valor = (decimal) 105.10;
        private const string _erroGenerico = "Erro genérico";
        private const string _caminhoGitHub = "https://github.com/xitaocrazy/CalculadorDeJuros";
        private Mock<ICalculadorDeJuros> _calculadorDeJurosMock;
        private Mock<ILoggerManager> _loggerMock;


        [Fact]
        public void CalculaJuros_deve_retornar_o_tipo_esperado_em_caso_de_sucesso()
        {
            var controller = CrieController();
            ConfigureCalculadorDeJurosCalculeJurosParaSucesso();

            var retorno = controller.CalculaJuros(100, 5);

            Assert.IsType<OkObjectResult>(retorno.Result);
        }

        [Fact]
        public void CalculaJuros_deve_retornar_o_valor_esperado_em_caso_de_sucesso()
        {
            var controller = CrieController();
            ConfigureCalculadorDeJurosCalculeJurosParaSucesso();

            var retorno = controller.CalculaJuros(100, 5);

            var resultado = (ObjectResult) retorno.Result;
            var valorRetornado = resultado.Value;
            Assert.Equal(_valor, valorRetornado);
        }         
        
        [Fact]
        public void CalculaJuros_deve_retornar_a_mensagem_esperada_em_caso_de_erro()
        {
            var controller = CrieController();
            ConfigureCalculadorDeJurosCalculeJurosParaErro();

            var ex = Assert.Throws<Exception>(() => controller.CalculaJuros(100, 5));

            Assert.Equal(_erroGenerico, ex.Message);
        }                                

        [Fact]
        public void CalculaJuros_deve_chamar_CalculadorDeJuros_CalculeJuros()
        {
            var controller = CrieController();
            ConfigureCalculadorDeJurosCalculeJurosParaSucesso();

            controller.CalculaJuros(200, 2);

            _calculadorDeJurosMock.Verify(s => s.CalculeJuros(It.IsAny<decimal>(), It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void CalculaJuros_deve_chamar_Logger_LogInfo()
        {
            var controller = CrieController();
            ConfigureCalculadorDeJurosCalculeJurosParaSucesso();

            controller.CalculaJuros(200, 2);

            var mensagem = "Recebido pedido de cálculo de juros com valor inicial = 200 e quantidade de meses = 2.";
            _loggerMock.Verify(l => l.LogInfo(mensagem), Times.Once);
            mensagem = $"Valor final após aplicação dos juros = {_valor}.";
            _loggerMock.Verify(l => l.LogInfo(mensagem), Times.Once);
        }


        [Fact]
        public void ShowMeTheCode_deve_retornar_o_valor_esperado()
        {
            var controller = CrieController();

            var retorno = controller.ShowMeTheCode();

            var valorRetornado = retorno.Value;
            Assert.Equal(_caminhoGitHub, valorRetornado);
        }        


        private CalculadorDeJurosController CrieController()
        {
            _calculadorDeJurosMock = new Mock<ICalculadorDeJuros>(MockBehavior.Strict);
            _loggerMock = new Mock<ILoggerManager>(MockBehavior.Strict);    
            ConfigureLoggerMock();        
            return new CalculadorDeJurosController(_calculadorDeJurosMock.Object, _loggerMock.Object);
        }
        
        private void ConfigureCalculadorDeJurosCalculeJurosParaSucesso()
        {
            _calculadorDeJurosMock.Setup(s => s.CalculeJuros(It.IsAny<decimal>(), It.IsAny<int>()))
                                  .Returns(_valor);
        }
        
        private void ConfigureCalculadorDeJurosCalculeJurosParaErro()
        {
            _calculadorDeJurosMock.Setup(s => s.CalculeJuros(It.IsAny<decimal>(), It.IsAny<int>()))
                                  .Throws(new Exception(_erroGenerico));
        }

        private void ConfigureLoggerMock()
        {
            _loggerMock.Setup(l => l.LogInfo(It.IsAny<string>()));
        }
    }
}