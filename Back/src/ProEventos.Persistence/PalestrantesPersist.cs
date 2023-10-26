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
    public class PalestrantesPersist : IPalestrantePersist
    {
        private readonly ProEventosContext _context;
        public PalestrantesPersist(ProEventosContext context)
        {
            this._context = context;
        }
        
        public async Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool incluirEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(p => p.RedesSociais);

            if(incluirEventos)
            {
                query = query
                .Include(p => p.PalestranteEventos)
                .ThenInclude(p => p.Evento);
            }

            query = query.AsNoTracking().OrderBy(p => p.Id)
                .Where(p => p.Nome.ToLower().Contains(nome.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesAsync(bool incluirEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(p => p.RedesSociais);

            if(incluirEventos)
            {
                query = query
                .Include(p => p.PalestranteEventos)
                .ThenInclude(p => p.Evento);
            }

            query = query.AsNoTracking().OrderBy(p => p.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, bool incluirEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(p => p.RedesSociais);

            if(incluirEventos)
            {
                query = query
                .Include(p => p.PalestranteEventos)
                .ThenInclude(p => p.Evento);
            }

            query = query.AsNoTracking().OrderBy(p => p.Id)
                .Where(p => p.Id == palestranteId);

            return await query.FirstOrDefaultAsync();
        }
    }
}