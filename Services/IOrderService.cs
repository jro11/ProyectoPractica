using ProyectoPractica.Models;

namespace ProyectoPractica.Services
{
    public interface IOrderService
    {
        Task<List<OrdenesProduccion>> GetWihtFiltersAsync(DateOnly? fecha, string? estado);
        Task<bool> SaveOrderAsync(OrdenesProduccion orden);
        Task<bool> DeleteOrderAsync(int id);
    }
}
