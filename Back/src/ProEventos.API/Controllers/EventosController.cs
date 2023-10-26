using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Domain;
using ProEventos.Application.Contratos;
using Microsoft.AspNetCore.Http;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        private readonly IEventoService _eventoService;
        public EventosController(IEventoService eventoService)
        {
            this._eventoService = eventoService;
        }

        [HttpGet]
        public async Task <ActionResult<Evento>> ObterEventos()
        {
            try
            {
                var eventos = await _eventoService.GetAllEventosAsync(true);
                if(eventos == null) return NotFound("Nenhum evento foi encontrado.");

                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao recuperar eventos. Erro: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task <ActionResult<Evento>> ObterEventoId(int id)
        {
            try
            {
                var evento = await _eventoService.GetEventoByIdAsync(id);
                if(evento == null) return NotFound("Evento não foi encontrado.");

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao recuperar evento. Erro: {ex.Message}");
            }
        }

        [HttpGet("tema/{tema}")]
        public async Task <ActionResult<Evento>> ObterTema(string tema)
        {
            try
            {
                var evento = await _eventoService.GetAllEventosByTemaAsync(tema);
                if(evento == null) return NotFound("Nenhum tema foi encontrado.");

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao recuperar tema. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Evento>> SalvarEvento(Evento model)
        {
            try
            {
                var evento = await _eventoService.AddEvento(model);
                if(evento == null) return BadRequest("Erro ao adicionar evento.");

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao adicionar evento. Erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Evento>> AlterarEvento(int id, Evento model)
        {
            try
            {
                var evento = await _eventoService.UpdateEvento(id,model);
                if(evento == null) return BadRequest("Erro ao alterar evento.");

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao alterar evento. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletarEvento(int id)
        {
            try
            {
               return await _eventoService.DeleteEvento(id) ?             
                    Ok("Evento removido com sucesso.") :                
                    BadRequest("Erro ao remover evento.");                              
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao remover evento. Erro: {ex.Message}");
            }
        }
    }
}
