using System;
using System.Threading.Tasks;
using CalculadorDeJurosApi.Middlewares;
using CalculadorDeJurosApi.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using static CalculadorDeJurosApiTests.Helpers.TestHelper;

namespace CalculadorDeJurosApiTEsts.Middlewares
{
    public class ExceptionMiddlewareTests
    {
        private const string _erroGenerico = "Erro genérico";
        private const decimal _valor = (decimal) 105.10;
        private const string _url = "/api/calculadordejuros/calculajuros?valorInicial=1&meses=1";
        private Mock<DelegateMock> _requestDelegateMock;
        private Mock<ILoggerManager> _loggerMock;        
        private ExceptionMiddleware _exceptionMiddleware;
        private HttpContext _httpContext;


        [Fact]
        public async Task InvokeAsync_deve_executar_o_delegate()
        {
            Setup();
            ConfigureRequestDelegateParaSucesso();

            await _exceptionMiddleware.InvokeAsync(_httpContext);

            _requestDelegateMock.Verify(x => x.RequestDelegate(It.IsAny<HttpContext>()), Times.Once);
        }

        [Fact]
        public async Task InvokeAsync_nao_deve_registrar_log_se_nao_der_erro()
        {
            Setup();
            ConfigureRequestDelegateParaSucesso();

            await _exceptionMiddleware.InvokeAsync(_httpContext);

            _loggerMock.Verify(x => x.LogError(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task InvokeAsync_nao_deve_alterar_o_context_se_nao_der_erro()
        {
            Setup();
            ConfigureRequestDelegateParaSucesso();

            await _exceptionMiddleware.InvokeAsync(_httpContext);

            Assert.Equal(200, _httpContext.Response.StatusCode);
        }

        [Fact]
        public async Task InvokeAsync_deve_registrar_log_se_der_erro()
        {
            Setup();
            ConfigureRequestDelegateParaErro();

            await _exceptionMiddleware.InvokeAsync(_httpContext);

            _loggerMock.Verify(x => x.LogError(It.Is<string>(v => v.StartsWith("Ocorreu um erro: System.Exception: Erro genérico"))), Times.Once);
        }

        [Fact]
        public async Task InvokeAsync_deve_alterar_o_context_se_der_erro()
        {
            Setup();
            ConfigureRequestDelegateParaErro();

            await _exceptionMiddleware.InvokeAsync(_httpContext);

            Assert.Equal(500, _httpContext.Response.StatusCode);
            Assert.Equal("application/json", _httpContext.Response.ContentType);            
        }


        private void Setup()
        {
            _requestDelegateMock = new Mock<DelegateMock>();
            _loggerMock = new Mock<ILoggerManager>(MockBehavior.Strict);                    
            _exceptionMiddleware =  new ExceptionMiddleware(_requestDelegateMock.Object.RequestDelegate, _loggerMock.Object);
            _httpContext = new DefaultHttpContext();
            _httpContext.Request.Path = _url;
            ConfigureLoggerMock();
        }

        private void ConfigureRequestDelegateParaSucesso()
        {
            _requestDelegateMock
                .Setup(x => x.RequestDelegate(It.IsAny<HttpContext>()))
                .Returns(Task.FromResult(true));
        }

        private void ConfigureRequestDelegateParaErro()
        {
            _requestDelegateMock
                .Setup(x => x.RequestDelegate(It.IsAny<HttpContext>()))
                .Throws(new Exception(_erroGenerico));
        }   

        private void ConfigureLoggerMock()
        {
            _loggerMock.Setup(l => l.LogError(It.IsAny<string>()));
        }     
    }
}