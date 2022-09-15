﻿using APIEvent.Core.Interface;
using APIEvent.Core.Model;
using Microsoft.AspNetCore.Mvc;

namespace Projeto_WebAPI_Evento.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class EventReservationController : Controller
    {
        private readonly IEventReservationService _reservationService;

        public EventReservationController(IEventReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet("/reserva")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<EventReservation>> GetAllEvents()
        {
            return Ok(_reservationService.GetAllReservations());
        }


        [HttpGet("/reserva/consulta/{titulo}/{nome}")]
        [Consumes("text/plain")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<EventReservation> GetReservationsByTitleAndName(string titulo, string nome)
        {
            var reservation = _reservationService.GetReservationsByTitleAndName(titulo, nome);

            if (reservation == null)
            {
                return NotFound();
            }
            return Ok(_reservationService.GetReservationsByTitleAndName(titulo, nome));
        }

        [HttpPost("/reserva/cadastrar")]
        [Consumes("application/json")]
        [Produces("application/json")]
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

        [HttpPut("/reserva/atualizar/{id_reserva}/{quantidade}")]
        [Consumes("text/plain")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateReservationQuantity(int id_reserva, int quantidade)
        {
            var reservation = _reservationService.UpdateReservationQuantity(id_reserva, quantidade);

            if (!reservation)
            {
                return NotFound();
            }
            return NoContent();
        }


        [HttpDelete("/reserva/deletar")]
        [Consumes("text/plain")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteReservation(int id_reserva)
        {
            var reservation = _reservationService.DeleteReservation(id_reserva);

            if (!reservation)
            {
                return NotFound();
            }
            return Ok();
           
        }
    }
}
