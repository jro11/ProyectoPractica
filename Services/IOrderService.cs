using ProyectoPractica.Models;

namespace ProyectoPractica.Services
{
    public interface IOrderService
    {
        Task<List<OrdenesProduccion>> GetAllAsync(DateOnly? fecha, string? estado);
        Task<bool> SaveAsync(OrdenesProduccion orden);
        Task<bool> DeleteAsync(int id);

    }
}
