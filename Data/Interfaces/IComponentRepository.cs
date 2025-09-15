using ProyectoPractica.Models;

namespace ProyectoPractica.Data.Interfaces
{
    public interface IComponentRepository
    {
        Task<List<Componente>> GetComponentsAsyinc();

        Task<bool> SaveComponentAsync(Componente component);

        Task<bool> DeleteComponentAsync(int id, string motivoBaja);

    }
}
