namespace CalculadorDeJurosApi.Services
{
    public interface ICalculadorDeJuros
    {
        decimal CalculeJuros(decimal valorInicial, int meses);
    }
}