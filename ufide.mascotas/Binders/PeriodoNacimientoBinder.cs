using System;
using System.Web.Mvc;
using ufide.mascotas.Models.ViewModels;

namespace ufide.mascotas.Binders
{
    public class PeriodoNacimientoBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext,
            ModelBindingContext bindingContext)
        {
            var mesValor = bindingContext.ValueProvider.GetValue("mes");
            var anioValor = bindingContext.ValueProvider.GetValue("anio");

            var periodo = new PeriodoNacimiento();

            if (mesValor != null && !string.IsNullOrWhiteSpace(mesValor.AttemptedValue))
            {
                int mes;
                if (!int.TryParse(mesValor.AttemptedValue, out mes) || mes < 1 || mes > 12)
                {
                    bindingContext.ModelState.AddModelError("mes", "El mes debe estar entre 1 y 12.");
                }
                else
                {
                    periodo.Mes = mes;
                }
            }

            if (anioValor != null && !string.IsNullOrWhiteSpace(anioValor.AttemptedValue))
            {
                int anio;
                if (!int.TryParse(anioValor.AttemptedValue, out anio) || anio < 1970 || anio > DateTime.Now.Year)
                {
                    bindingContext.ModelState.AddModelError("anio", "El año no es válido.");
                }
                else
                {
                    periodo.Anio = anio;
                }
            }

            if (periodo.Mes.HasValue != periodo.Anio.HasValue)
            {
                bindingContext.ModelState.AddModelError("periodo", "Indique mes y año, o deje ambos vacíos.");
            }

            return periodo;
        }
    }
}
