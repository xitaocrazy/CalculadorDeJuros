using System.Linq;
using CalculadorDeJurosApi.Constraints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace CalculadorDeJurosApi.Attributes
{
    public class RequiredFromQueryAttribute : FromQueryAttribute, IParameterModelConvention
    {
        public void Apply(ParameterModel parameter)
        {
            if (parameter.Action?.Selectors?.Any() ?? false)
            {
                var restricao = new RequiredFromQueryActionConstraint(parameter.BindingInfo?.BinderModelName ?? parameter.ParameterName);
                parameter.Action.Selectors.Last().ActionConstraints.Add(restricao);
            }
        }
    }
}