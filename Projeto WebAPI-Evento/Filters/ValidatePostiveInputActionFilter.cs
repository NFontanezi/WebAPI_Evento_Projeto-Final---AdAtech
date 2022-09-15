
using APIEvent.Core.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Projeto_WebAPI_Evento.Filters
{
    public class ValidatePostiveInputActionFilter : ActionFilterAttribute
    {

        public IEventReservationService _reservationService;

        public ValidatePostiveInputActionFilter (IEventReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            int quant = (int)context.ActionArguments["quantidade"];

            if(quant==0)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
            }

            if (quant < 0)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status406NotAcceptable);
            }

        }
    }
}
