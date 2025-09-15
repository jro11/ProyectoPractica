using ProyectoPractica.Data.Interfaces;
using ProyectoPractica.Models;

namespace ProyectoPractica.Services
{
    public class ComponentService : IComponentService
    {
        private readonly IComponentRepository _repo;

        public ComponentService(IComponentRepository repo)
        {
            _repo = repo;
        }
        public async Task<bool> DeleteComponentAsync(int id, string motivoBaja)
        {
            return await _repo.DeleteComponentAsync(id, motivoBaja);
        }

        public async Task<List<Componente>> GetAllComponentsAsync()
        {
            return await _repo.GetAllComponentsAsync();
        }

        public async Task<bool> SaveComponentAsync(Componente componente)
        {
            return await _repo.SaveComponentAsync(componente);
        }
    }
}
