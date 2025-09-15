using ProyectoPractica.Data.Interfaces;
using ProyectoPractica.Data.Repositories;
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
        public async Task<bool> DeleteAsync(int id)
        {
            return await _repo.DeleteOrdenAsync(id);
        }

        public async Task<List<OrdenesProduccion>> GetAllAsync(DateOnly? fecha, string? estado)
        {
            return await _repo.GetOrdenesAsync(fecha, estado);
        }

        public async Task<bool> SaveAsync(OrdenesProduccion orden)
        {
            return await _repo.SaveOrdenAsync(orden);
        }
    }
}
