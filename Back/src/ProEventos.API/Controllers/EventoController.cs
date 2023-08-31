using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.API.Models;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {
        public IEnumerable<Evento> _evento = new Evento[]{
            new Evento(){
                EventoId = 1,
                Tema = "Curso de Angular",
                Local = "Cascavel",
                QtdPessoas = 250,
                DataEvento = DateTime.Now.AddDays(2).ToString("dd/MM/yyyy"),
                Lote = "Lote 10",
                ImagemURL = "angular.jpg"
            },
            new Evento(){
                EventoId = 2,
                Tema = "Curso de Java",
                Local = "Curitiba",
                QtdPessoas = 100,
                DataEvento = DateTime.Now.AddDays(2).ToString("dd/MM/yyyy"),
                Lote = "Lote 20",
                ImagemURL = "java.jpg"
            }
        };
        public EventoController()
        {
        }

        [HttpGet]
        public IEnumerable<Evento> Get()
        {
            return _evento;
        }

        [HttpGet("{id}")]
        public IEnumerable<Evento> GetById(int id)
        {
            return _evento.Where(evento => evento.EventoId == id);
        }

        [HttpPost]
        public string Post()
        {
            return "teste POST";
        }

        [HttpPut("{id}")]
        public string Put(int id)
        {
            return $"teste PUT com id= {id}";
        }

        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            return $"teste DELETE com id= {id}";
        }
    }
}
