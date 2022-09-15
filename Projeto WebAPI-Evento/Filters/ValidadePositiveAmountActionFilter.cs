
using APIEvent.Core.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Projeto_WebAPI_Evento.Filters
{
    public class ValidadePositiveAmountActionFilter : ActionFilterAttribute
    {
        public ICityEventService _cityService;

        public ValidadePositiveAmountActionFilter(ICityEventService cityService)
        {
            _cityService = cityService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
          decimal quant1 = (decimal)context.ActionArguments["preco_min"];
          decimal quant2 = (decimal)context.ActionArguments["preco_max"];

            if (quant1 <0 || quant1 <0)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
            }

            if (quant1 > quant2)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status406NotAcceptable);
            }


        }
    }
}

