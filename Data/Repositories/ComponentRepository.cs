using Microsoft.EntityFrameworkCore;
using ProyectoPractica.Data.Interfaces;
using ProyectoPractica.Models;

namespace ProyectoPractica.Data.Repositories
{
    public class ComponentRepository : IComponentRepository
    {
        DbProduccionContext _context;

        public ComponentRepository(DbProduccionContext context)
        {
            _context = context;
        }
        public async Task<bool> DeleteComponentAsync(int id, string motivoBaja)
        {
            var component = await _context.Componentes.FindAsync(id);
            if(component!=null)
            {
                component.FechaBaja = DateOnly.FromDateTime(DateTime.Now);
                component.MotivoBaja = motivoBaja;
                 _context.Update(component);
                return await _context.SaveChangesAsync() > 0;

            }
            else { return false; }
        }

        public async Task<List<Componente>> GetAllComponentsAsync()
        {
            return await _context.Componentes.ToListAsync();
        }

        public async Task<bool> SaveComponentAsync(Componente componente)
        {
            if(componente.Codigo == 0)
            {
                componente.FechaBaja = null;
                componente.MotivoBaja = null;
                _context.Add(componente);
            }
            else
            {
                componente.FechaBaja = null;
                componente.MotivoBaja = null;
                _context.Update(componente);
            }
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
