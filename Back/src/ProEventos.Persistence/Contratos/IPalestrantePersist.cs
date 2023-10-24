using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistence.Contratos
{
    public interface IPalestrantePersist
    {
        //Palestrantes
        Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool incluirEventos = false);
        Task<Palestrante[]> GetAllPalestrantesAsync(bool incluirEventos = false);
        Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, bool incluirEventos = false);
    }
}