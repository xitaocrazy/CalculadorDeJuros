using CalculadorDeJurosApi.Attributes;
using CalculadorDeJurosApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CalculadorDeJurosApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CalculadorDeJurosController : ControllerBase
    {
        private ICalculadorDeJuros _calculadorDeJuros;

        public CalculadorDeJurosController(ICalculadorDeJuros calculadorDeJuros){
            _calculadorDeJuros = calculadorDeJuros;
        }

        /// <summary>
        /// Calcula o valor final após aplicar a taxa de juros ao longo do tempo.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///     POST /CalculadorDeJuros/calculajuros?<paramref name="valorInicial"/>=100&amp;<paramref name="meses"/>=5
        /// </remarks>
        /// <param name="valorInicial">[Required]Valor base ao qual será aplicado o juros ao longo do tempo.</param>
        /// <param name="meses">[Required]Tempo em mêses para o cálculo do juros.</param>
        /// <returns>Valor final calculado.</returns>  
        /// <response code="200">Retorna quando o valor é calculado com sucesso.</response>          
        [HttpPost]
        [ProducesResponseType(200)]
        public ActionResult<decimal> CalculaJuros([RequiredFromQueryAttribute] decimal valorInicial, [RequiredFromQueryAttribute] int meses)
        {
            return 200;
        }

        /// <summary>
        /// Retorna o endereço do projeto no GitHub.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///     GET /CalculadorDeJuros/showmethecode
        /// </remarks>
        /// <returns>Endereço do projeto no GitHub.</returns>  
        /// <response code="200">Retorna quando o endereço é retornado com sucesso.</response> 
        [HttpGet]
        [ProducesResponseType(200)]
        public ActionResult<string> ShowMeTheCode() => "em breve";
    }
}