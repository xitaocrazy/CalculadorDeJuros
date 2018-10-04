using Newtonsoft.Json;
 
namespace CalculadorDeJurosApi.Models
{
    public class ExcecaoPersonalizada
    {
        public int StatusCode { get; set; }
        public string Mensagem { get; set; }
        public string StackTrace { get; set; }
 
 
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}