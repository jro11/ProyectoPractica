using ProyectoPractica.Models;

namespace ProyectoPractica.Services
{
    public interface IComponentService
    {
        Task<List<Componente>> GetComponentsAsyinc();
        Task<bool> SaveComponentAsync(Componente component);
        Task<bool> DeleteComponentAsync(int id, string motivoBaja);
    }
}
