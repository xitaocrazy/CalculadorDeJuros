using System;
using CalculadorDeJurosApi.Controllers;
using CalculadorDeJurosApi.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CalculadorDeJurosApiTests.Controllers
{
    public class CalculadorDeJurosControllerTests
    {
        const decimal _valor = (decimal) 105.10;
        const string _erroGenerico = "Erro genérico";
        const string _caminhoGitHub = "https://github.com/xitaocrazy/CalculadorDeJuros";
        private Mock<ICalculadorDeJuros> _calculadorDeJurosMock;


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
        public void CalculaJuros_deve_retornar_o_tipo_esperado_em_caso_de_erro()
        {
            var controller = CrieController();
            ConfigureCalculadorDeJurosCalculeJurosParaErro();

            var retorno = controller.CalculaJuros(100, 5);

            Assert.IsType<ObjectResult>(retorno.Result);
        }

        [Fact]
        public void CalculaJuros_deve_retornar_a_mensagem_esperada_em_caso_de_erro()
        {
            var controller = CrieController();
            ConfigureCalculadorDeJurosCalculeJurosParaErro();

            var retorno = controller.CalculaJuros(100, 5);

            var resultado = (ObjectResult) retorno.Result;
            var valorRetornado = resultado.Value;
            Assert.Equal(_erroGenerico, valorRetornado);
        }

        [Theory]
        [InlineData(false, 500)]
        [InlineData(true, 200)]
        public void CalculaJuros_deve_retornar_o_status_http_esperado(bool comSucesso, int statusEsperado)
        {
            var controller = CrieController();
            ConfigureCalculadorDeJurosCalculeJurosComoDesejado(comSucesso);

            var retorno = controller.CalculaJuros(200, 2);

            var resultado = (ObjectResult) retorno.Result;
            var status = resultado.StatusCode;
            Assert.Equal(statusEsperado, status);
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
            return new CalculadorDeJurosController(_calculadorDeJurosMock.Object);
        }

        private void ConfigureCalculadorDeJurosCalculeJurosComoDesejado(bool comSucesso){
            if(comSucesso)
            {
                ConfigureCalculadorDeJurosCalculeJurosParaSucesso();
            } 
            else{
                ConfigureCalculadorDeJurosCalculeJurosParaErro();
            }
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
    }
}