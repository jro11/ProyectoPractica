using ProyectoPractica.Models;

namespace ProyectoPractica.Data.Interfaces
{
    public interface IComponentRepository
    {
        Task<List<Componente>> GetAllComponentsAsync();

        Task<bool> SaveComponentAsync(Componente componente);

        Task<bool> DeleteComponentAsync(int id, string motivoBaja);


    }
}
