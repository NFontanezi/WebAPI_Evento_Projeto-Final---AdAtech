﻿using APIEvent.Core.Interface;
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

        [HttpGet("/events/consult")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<CityEvent>> GetAllEvents()
        {
            return Ok(_cityService.GetAllEvents());
        }

        
        [HttpGet("/events/consult1/{title}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<CityEvent>> GetEventsByTitle(string title)
        {
            var reservation = _cityService.GetEventsByTitle(title);
            if (title == null)
            {
                return BadRequest();
            }
            if (reservation == null)
            {
                return NotFound();
            }
            return Ok(_cityService.GetEventsByTitle(title));
        }

        [HttpGet("/events/consult2/{local}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<CityEvent>> GetEventsByLocal(string local)
        {
            var reservation = _cityService.GetEventsByLocal(local);
            if (local == null)
            {
                return BadRequest();
            }
            if (reservation == null)
            {
                return NotFound();
            }
            return Ok(_cityService.GetEventsByLocal(local));
        }

        [HttpPost("/events/insert")]
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

        [HttpPut("/events/update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateEvent(int id, CityEvent e)
        {
            var reservation = _cityService.UpdateEvent(id, e);

            if (!reservation)
            {
                return NotFound();
            }
            return NoContent();
        }


        [HttpDelete("/events/delete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteEvent(int id)
        {
            var reservation = _cityService.DeleteEvent(id);

            if (!reservation)
            {
                return NotFound();
            }
            return Ok();

        }
    }
}