using APIEvent.Core.Interface;
using APIEvent.Core.Model;
using Microsoft.AspNetCore.Mvc;

namespace Projeto_WebAPI_Evento.Controllers
{
    public class CityEventController : Controller
    {
        private readonly ICityEventService _cityService;

        public CityEventController(ICityEventService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet("/event/consultar")]
        public ActionResult<CityEvent> GetAllEvents()
        {
            return Ok(_cityService.GetAllEvents());
        }
    }
}
