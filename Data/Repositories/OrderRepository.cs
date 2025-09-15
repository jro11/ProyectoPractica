using Microsoft.EntityFrameworkCore;
using ProyectoPractica.Data.Interfaces;
using ProyectoPractica.Models;
using System.Data.Common;

namespace ProyectoPractica.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DbProduccionContext _context;

        public OrderRepository(DbProduccionContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            try
            {
                if (id > 0)
                {
                    var orden = await _context.OrdenesProduccions.FindAsync(id);
                    if (orden != null && orden.Estado != "Cancelada" && orden.Estado != "Finalizada")
                    {
                        orden.Estado = "Cancelada";
                        _context.OrdenesProduccions.Update(orden);
                        return await _context.SaveChangesAsync() > 0;

                    }
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {

                throw new Exception("Error" + ex.Message, ex);
            }
            
        }

        public async Task<List<OrdenesProduccion>> GetWihtFiltersAsync(DateOnly? fecha, string? estado)
        {
            try
            {
                var lst = await _context.OrdenesProduccions.Include(o => o.detalles).ToListAsync();
                if (fecha.HasValue)
                {
                    lst = lst.Where(o => o.Fecha >= fecha.Value).ToList();
                }
                if(!string.IsNullOrEmpty(estado))
                {
                    lst = lst.Where(o => o.Estado==estado).ToList();
                }
                return lst;
            }
            catch (Exception ex)
            {

                throw new Exception("Error trayendo la lista" + ex.Message, ex);
            }
            
        }

        public async Task<bool> SaveOrderAsync(OrdenesProduccion orden)
        {
            try
            {
                if (orden.NroOrden == 0)
                {
                    orden.Estado = "Creada";
                    _context.OrdenesProduccions.Add(orden);
                }
                else
                {
                    return false;
                }
                return await _context.SaveChangesAsync() > 0;

            }
            catch (Exception ex)
            {

                throw new Exception("Error al guardar la orden" + ex.Message, ex);
            }
        }
    }
}
