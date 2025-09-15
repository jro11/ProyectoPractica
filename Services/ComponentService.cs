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

        public async Task<List<Componente>> GetComponentsAsyinc()
        {
            return await _repo.GetComponentsAsyinc();
        }

        public async Task<bool> SaveComponentAsync(Componente component)
        {
            return await _repo.SaveComponentAsync(component);
        }
    }
}
