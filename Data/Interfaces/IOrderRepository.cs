using ProyectoPractica.Models;

namespace ProyectoPractica.Data.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<OrdenesProduccion>> GetOrdenesAsync(DateOnly? fecha, string? estado);
        Task<bool> SaveOrdenAsync(OrdenesProduccion orden);
        Task<bool> DeleteOrdenAsync(int id);
    }
}
