using System;
using CalculadorDeJurosApi.Attributes;
using CalculadorDeJurosApi.Services;
using CalculadorDeJurosApi.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CalculadorDeJurosApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CalculadorDeJurosController : ControllerBase
    {
        private ICalculadorDeJuros _calculadorDeJuros;
        private ILoggerManager _logger;


        public CalculadorDeJurosController(ICalculadorDeJuros calculadorDeJuros, ILoggerManager logger){
            _calculadorDeJuros = calculadorDeJuros;
            _logger = logger;
        }

        /// <summary>
        /// Calcula o valor final após aplicar a taxa de juros ao longo do tempo.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        /// <!-- 
        /// <pre>
        ///     POST /CalculadorDeJuros/calculajuros?<paramref name="valorInicial"/>=100&<paramref name="meses"/>=5
        /// </pre>
        /// -->
        /// </remarks>
        /// <param name="valorInicial">[Required]Valor base ao qual será aplicado o juros ao longo do tempo.</param>
        /// <param name="meses">[Required]Tempo em mêses para o cálculo do juros.</param>
        /// <returns>Valor final calculado.</returns>  
        /// <response code="200">Retorna quando o valor é calculado com sucesso.</response> 
        /// <response code="400">Retorna quando o algum valor inválido é informado como parâmetro.</response> 
        /// <response code="500">Retorna quando ocorre algum erro durante o cálculo.</response>          
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public ActionResult<decimal> CalculaJuros([RequiredFromQueryAttribute] decimal valorInicial, [RequiredFromQueryAttribute] int meses)
        {
            var mensagem = $"Recebido pedido de cálculo de juros com valor inicial = {valorInicial} e quantidade de meses = {meses}.";
            _logger.LogInfo(mensagem);

            var valorFinal = _calculadorDeJuros.CalculeJuros(valorInicial, meses);
            
            mensagem = $"Valor final após aplicação dos juros = {valorFinal}.";
            _logger.LogInfo(mensagem);
            
            return Ok(valorFinal);
        }

        /// <summary>
        /// Retorna o endereço do projeto no GitHub.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        /// <!-- 
        /// <pre>
        ///     GET /CalculadorDeJuros/showmethecode
        /// </pre>
        /// -->
        /// </remarks>
        /// <returns>Endereço do projeto no GitHub.</returns>  
        /// <response code="200">Retorna quando o endereço é retornado com sucesso.</response> 
        [HttpGet]
        [ProducesResponseType(200)]
        public ActionResult<string> ShowMeTheCode() => "https://github.com/xitaocrazy/CalculadorDeJuros";
    }
}