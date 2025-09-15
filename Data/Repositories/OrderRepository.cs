using Microsoft.EntityFrameworkCore;
using ProyectoPractica.Data.Interfaces;
using ProyectoPractica.Models;

namespace ProyectoPractica.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        DbProduccionContext _context;
        public OrderRepository(DbProduccionContext context)
        {
            _context = context;
        }
        public async Task<bool> DeleteOrdenAsync(int id)
        {
            try
            {
                if (id > 0)
                {
                    var orden = await _context.OrdenesProduccions.FindAsync(id);
                    if (orden != null)
                    {
                        if (orden.Estado != "Finalizada" && orden.Estado != "Cancelada")
                        {
                            orden.Estado = "Cancelada";
                            _context.OrdenesProduccions.Update(orden);
                            return await _context.SaveChangesAsync() > 0;
                        }
                        return false;

                    }
                    else
                        return false;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {

                throw new Exception("Error conectando con el servidor" + ex.Message);
            }
            
          
        }

        public async Task<List<OrdenesProduccion>> GetOrdenesAsync(DateOnly? fecha, string? estado)
        {
            var lst =  await _context.OrdenesProduccions.Include(o => o.Detalles).ToListAsync();
            var lstFiltrada = lst.Where(l => l.Fecha>=fecha && l.Estado ==estado  || fecha ==null && estado==null
            || l.Fecha >= fecha && estado ==null || l.Estado == estado && fecha ==null);
            return lstFiltrada.ToList();
            
        }

        public async Task<bool> SaveOrdenAsync(OrdenesProduccion orden)
        {
            if (orden.NroOrden == 0)
            {
                orden.Estado = "Creada";
                _context.OrdenesProduccions.Add(orden);
            }
            else
            {
                _context.OrdenesProduccions.Update(orden);
            }
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
