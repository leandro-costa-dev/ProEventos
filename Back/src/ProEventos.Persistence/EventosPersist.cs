using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Context;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Persistence
{
    public class EventosPersist : IEventoPersist
    {
        private readonly ProEventosContext _context;
        public EventosPersist(ProEventosContext context)
        {
            this._context = context;
        }

        public async Task<Evento[]> GetAllEventosAsync(bool incluirPalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(e => e.Lote)
                .Include(e => e.RedesSociais);

            if(incluirPalestrantes)
            {
                query = query
                .Include(e => e.PalestranteEventos)
                .ThenInclude(p => p.Palestrante);
            }

            query = query.OrderBy(e => e.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool incluirPalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(e => e.Lote)
                .Include(e => e.RedesSociais);

            if(incluirPalestrantes)
            {
                query = query
                .Include(e => e.PalestranteEventos)
                .ThenInclude(p => p.Palestrante);
            }

            query = query.OrderBy(e => e.Id)
                .Where(e => e.Tema.ToLower().Contains(tema.ToLower()));

            return await query.ToArrayAsync();            
        }

        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool incluirPalestrantes = false)
        {
                IQueryable<Evento> query = _context.Eventos
                .Include(e => e.Lote)
                .Include(e => e.RedesSociais);

            if(incluirPalestrantes)
            {
                query = query
                .Include(e => e.PalestranteEventos)
                .ThenInclude(p => p.Palestrante);
            }

            query = query.OrderBy(e => e.Id)
                .Where(e => e.Id == eventoId);

            return await query.FirstOrDefaultAsync();
        }
    }
}