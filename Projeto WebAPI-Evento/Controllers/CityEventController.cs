using APIEvent.Core.Interface;
using APIEvent.Core.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projeto_WebAPI_Evento.Filters;

namespace Projeto_WebAPI_Evento.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]

    public class CityEventController : ControllerBase
    {
        private readonly ICityEventService _cityService;

        public CityEventController(ICityEventService cityService)
        {
            _cityService = cityService;
        }
        


        [HttpGet("/evento/consulta/{titulo}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<CityEvent>>> GetEventsByTitleAsync(string titulo)
        {
            var reservation = await _cityService.GetEventsByTitleAsync(titulo);

            if (reservation.Count == 0)
            {
                return NotFound();
            }
            return Ok(reservation);
        }

        [HttpGet("/evento/consulta/{local}/{data}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<CityEvent>>> GetEventsByLocalAndDateAsync(string local, DateTime data)
        {
            var reservation = await _cityService.GetEventsByLocalAndDateAsync(local, data);

            if (reservation.Count == 0)
            {
                return NotFound();
            }
            return Ok(reservation);
        }

        [HttpGet("/evento/consulta/{preco_min}/{preco_max}/{data}")]
        [ServiceFilter(typeof(ValidadePositiveAmountActionFilter))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<CityEvent>>> GetEventsByPriceAndDataAsync(decimal preco_min,decimal preco_max, DateTime data)
        {

            var reservation = await _cityService.GetEventsByPriceAndDataAsync(preco_min, preco_max, data);


            if (reservation.Count == 0)
            {
                return NotFound();
            }
            return Ok(reservation);

        }

        [HttpPost("/evento/cadastrar")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult> InsertEventAsync(CityEvent e) // VERIFICAR ERRO
        {
            var reservation = await _cityService.InsertEventAsync(e);
            if (!reservation)
            {
                return BadRequest();
            }
            return  CreatedAtAction(nameof(InsertEventAsync), e);
        }

        [HttpPut("/evento/atualizar")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateEventAsync(int id_evento, CityEvent e)
        {
            var reservation = await _cityService.UpdateEventAsync(id_evento, e);

            if (!reservation)
            {
                return NotFound();
            }
            return NoContent();
        }


        [HttpDelete("/evento/deletar")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteEventAsync(int id_evento)
        {
            var reservation = await _cityService.DeleteEventAsync(id_evento);

            if (!reservation)
            {
                return NotFound();
            }
            return Ok();

        }
    }
}
