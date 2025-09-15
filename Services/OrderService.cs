using ProyectoPractica.Data.Interfaces;
using ProyectoPractica.Models;

namespace ProyectoPractica.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repo;

        public OrderService(IOrderRepository repo)
        {
            _repo = repo;
        }
        public async Task<bool> DeleteOrderAsync(int id)
        {
            return await _repo.DeleteOrderAsync(id);
        }

        public async Task<List<OrdenesProduccion>> GetWihtFiltersAsync(DateOnly? fecha, string? estado)
        {
            return await _repo.GetWihtFiltersAsync(fecha, estado);
        }

        public async Task<bool> SaveOrderAsync(OrdenesProduccion orden)
        {
            return await _repo.SaveOrderAsync(orden);
        }
    }
}
