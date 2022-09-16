using APIEvent.Core.Interface;
using APIEvent.Core.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projeto_WebAPI_Evento.Filters;

namespace Projeto_WebAPI_Evento.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Authorize]

    public class EventReservationController : ControllerBase
    {
        private readonly IEventReservationService _reservationService;

        public EventReservationController(IEventReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet("/reserva")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<ActionResult<List<EventReservation>>> GetAllReservationsAsync()
        {
            return Ok(await _reservationService.GetAllReservationsAsync());
        }


        [HttpGet("/reserva/consulta/{titulo}/{nome}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "admin, cliente")]
        public async Task<ActionResult<List<Object>>> GetReservationsByTitleAndNameAsync(string titulo, string nome)
        {
            var reservation = await _reservationService.GetReservationsByTitleAndNameAsync(titulo, nome);

            if (reservation.Count == 0)
            {
                return NotFound();
            }
            return Ok(reservation);
        }

        [HttpPost("/reserva/cadastrar")] 
        [ActionName("InsertReservationAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<ActionResult<EventReservation>> InsertReservationAsync(EventReservation e)
        {
            var reservation = await _reservationService.InsertReservationAsync(e);
            if (!reservation)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(InsertReservationAsync), e);
        }

        [HttpPut("/reserva/atualizar/{id_reserva}/{quantidade}")]
        [ServiceFilter(typeof(ValidatePostiveInputActionFilter))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateReservationQuantityAsync(int id_reserva, int quantidade)
        {
            var reservation = await _reservationService.UpdateReservationQuantityAsync(id_reserva, quantidade);

            if (!reservation)
            {
                return NotFound();
            }
            return NoContent();
        }


        [HttpDelete("/reserva/deletar")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteReservationAsync(int id_reserva)
        {
            var reservation = await _reservationService.DeleteReservationAsync(id_reserva);

            if (!reservation)
            {
                return NotFound();
            }
            return Ok();
           
        }
    }
}
