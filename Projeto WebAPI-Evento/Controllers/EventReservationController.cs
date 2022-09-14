using APIEvent.Core.Interface;
using APIEvent.Core.Model;
using Microsoft.AspNetCore.Mvc;

namespace Projeto_WebAPI_Evento.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Consumes("application/json")]
    //[Produces("application/json")]
    public class EventReservationController : Controller
    {
        private readonly IEventReservationService _reservationService;

        public EventReservationController(IEventReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet("/reservations/consult")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<EventReservation>> GetAllEvents()
        {
            return Ok(_reservationService.GetAllReservations());
        }


        [HttpGet("/reservations/consult2/{title}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<EventReservation> GetReservationsByTitleAndName(string Title, string PersonName)
        {
            var reservation = _reservationService.GetReservationsByTitleAndName(Title, PersonName);

            if (reservation == null)
            {
                return NotFound();
            }
            return Ok(_reservationService.GetReservationsByTitleAndName(Title, PersonName));
        }

        [HttpPost("/reservations/insert")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult InsertReservation(EventReservation e)
        {
            var reservation = _reservationService.InsertReservation(e);
            if (!reservation)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(InsertReservation), e);
        }

        [HttpPut("/reservations/update/{id}/{quant}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateReservationQuantity(int id, int quant)
        {
            var reservation = _reservationService.UpdateReservationQuantity(id, quant);

            if (!reservation)
            {
                return NotFound();
            }
            return NoContent();
        }


        [HttpDelete("/reservations/delete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteReservation(int id)
        {
            var reservation = _reservationService.DeleteReservation(id);

            if (!reservation)
            {
                return NotFound();
            }
            return Ok();
           
        }
    }
}
