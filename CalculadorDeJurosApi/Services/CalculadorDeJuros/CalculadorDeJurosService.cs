using System;
using CalculadorDeJurosApi.Extensions;

namespace CalculadorDeJurosApi.Services
{
    public class CalculadorDeJurosService : ICalculadorDeJuros
    {
        const double juros = 1.01;
        const int precisao = 2;
        
        public decimal CalculeJuros(decimal valorInicial, int meses)
        {            
            var jurosAoLongoDoPeriodo = (decimal) Math.Pow(juros, meses);
            var valorFinal = valorInicial * jurosAoLongoDoPeriodo;
            valorFinal = valorFinal.Trunque(precisao);
            return valorFinal;
        }        
    }
}