using ProyectoPractica.Models;
using System.ComponentModel;

namespace ProyectoPractica.Services
{
    public interface IComponentService
    {
        Task<List<Componente>> GetAllComponentsAsync();
        Task<bool> SaveComponentAsync(Componente componente);
        Task<bool> DeleteComponentAsync(int id, string motivoBaja);
    }
}
