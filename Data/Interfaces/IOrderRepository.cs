using ProyectoPractica.Models;

namespace ProyectoPractica.Data.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<OrdenesProduccion>> GetWihtFiltersAsync(DateOnly? fecha, string? estado);

        Task<bool> SaveOrderAsync(OrdenesProduccion orden);

        Task<bool> DeleteOrderAsync(int id);
    }
}
