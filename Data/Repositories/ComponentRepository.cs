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
            try
            {
                if (id > 0)
                {
                    var component = _context.Componentes.Find(id);
                    if (component != null)
                    {
                        component.FechaBaja = DateOnly.FromDateTime(DateTime.Now);
                        component.MotivoBaja = motivoBaja;
                        _context.Componentes.Update(component);
                        return await _context.SaveChangesAsync() > 0;

                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Error al eliminar el componente" + ex.Message);
            }
            
            

        }

        public async Task<List<Componente>> GetComponentsAsyinc()
        {

            try
            {
               var lst = await _context.Componentes.ToListAsync();
                if (lst.Count >0)
                {
                    return lst;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Error al obtener los componentes", ex);
            }
        }

        public async Task<bool> SaveComponentAsync(Componente component)
        {
            try
            {
                if (component.Codigo == 0)
                {   
                    component.FechaBaja = null;
                    component.MotivoBaja = null;
                    _context.Componentes.Add(component);
                }
                else
                {
                    component.FechaBaja = null;
                    component.MotivoBaja = null;
                    _context.Componentes.Update(component);
                }
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {

                throw new Exception("Error al guardar el componente"+ex.Message);
            }
            
        }
    }
}
