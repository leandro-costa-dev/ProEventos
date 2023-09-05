using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProEventos.API.Data;
using ProEventos.API.Models;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {
        private readonly DataContext _context;
        public EventoController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Evento>> ObterEventos()
        {
            var eventos = _context.Eventos.AsNoTracking().ToList();            
            if (eventos is null)
            {
                return NotFound("Nenhum evento foi encontrado!");
            }
            return eventos;
        }

        [HttpGet("{id:int}", Name = "ObterEventoId")]
        public ActionResult<Evento> ObterEventoId(int id)
        {
            var evento = _context.Eventos.FirstOrDefault(
            evento => evento.EventoId == id);

            if(evento is null)
            {
                return NotFound($"Evento com id {id} não encontrado!");
            }

            return evento;
        }

        [HttpPost]
        public ActionResult SalvarEvento(Evento evento)
        {
            if(evento is null)
            {
                return BadRequest();
            }

            _context.Eventos.Add(evento);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterEventoId",
                new { id = evento.EventoId }, evento);
        }

        [HttpPut("{id:int}")]
        public ActionResult AlterarEvento(int id, Evento evento)
        {
            if (id != evento.EventoId)
            {
                return BadRequest();
            }

            _context.Entry(evento).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(evento);
        }

        [HttpDelete("{id:int}")]
        public ActionResult DeletarEvento(int id)
        {
            var evento = _context.Eventos.FirstOrDefault(evento => evento.EventoId == id);

            if (evento is null)
            {
                return NotFound($"Evento com id {id} não localizado...");
            }

            _context.Eventos.Remove(evento);
            _context.SaveChanges();

            return Ok(evento);
        }
    }
}
