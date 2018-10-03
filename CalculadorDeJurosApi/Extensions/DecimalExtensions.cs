using System;

namespace CalculadorDeJurosApi.Extensions
{
    public static class DecimalExtensions
    {        
        public static decimal Trunque(this decimal valor, int precisao)
        {
            const int baseCalc = 10;
            var constante = (decimal) Math.Pow(baseCalc, precisao);
            decimal tmp = Math.Truncate(constante * valor);
            return tmp / constante;
        }
    }
}