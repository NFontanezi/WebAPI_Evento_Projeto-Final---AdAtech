using APIEvent.Core.Interface;
using APIEvent.Core.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projeto_WebAPI_Evento.Filters;

namespace Projeto_WebAPI_Evento.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CityEventController : ControllerBase
    {
        private readonly ICityEventService _cityService;

        public CityEventController(ICityEventService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet("/evento")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<CityEvent>> GetAllEvents()
        {
            var reservation = _cityService.GetAllEvents();

            if (reservation.Count == 0)
            {
                return NotFound();
            }
            return Ok(_cityService.GetAllEvents());
        }


        [HttpGet("/evento/consulta/{titulo}")]
        [Consumes("text/plain")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<CityEvent>> GetEventsByTitle(string titulo)
        {
            var reservation = _cityService.GetEventsByTitle(titulo);

            if (reservation.Count == 0)
            {
                return NotFound();
            }
            return Ok(_cityService.GetEventsByTitle(titulo));
        }

        [HttpGet("/evento/consulta/{local}/{data}")]
        [Consumes("text/plain")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<CityEvent>> GetEventsByLocalAndDate(string local, DateTime data)


        {
            var reservation = _cityService.GetEventsByLocalAndDate(local, data);

            if (reservation.Count == 0)
            {
                return NotFound();
            }
            return Ok(_cityService.GetEventsByLocalAndDate(local, data));
        }

        [HttpGet("/evento/consulta/{preco_min}/{preco_max}/{data}")]
        [Consumes("text/plain")]
        [Produces("application/json")]
        [ServiceFilter(typeof(ValidadePositiveAmountActionFilter))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<CityEvent>> GetEventsByPriceAndData(decimal preco_min,decimal preco_max, DateTime data)
        {

            var reservation = _cityService.GetEventsByPriceAndData(preco_min, preco_max, data);


            if (reservation.Count == 0)
            {
                return NotFound();
            }
            return Ok(reservation);

        }

        [HttpPost("/evento/cadastrar")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult InsertEvent(CityEvent e)
        {
            var reservation = _cityService.InsertEvent(e);
            if (!reservation)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(InsertEvent), e);
        }

        [HttpPut("/evento/atualizar")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public IActionResult UpdateEvent(int id_evento, CityEvent e)
        {
            var reservation = _cityService.UpdateEvent(id_evento, e);

            if (!reservation)
            {
                return NotFound();
            }
            return NoContent();
        }


        [HttpDelete("/events/deletar")]
        [Consumes("text/plain")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteEvent(int id_evento)
        {
            var reservation = _cityService.DeleteEvent(id_evento);

            if (!reservation)
            {
                return NotFound();
            }
            return Ok();

        }
    }
}
