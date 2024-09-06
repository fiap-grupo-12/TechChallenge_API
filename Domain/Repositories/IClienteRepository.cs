using FIAP.TechChallenge.ByteMeBurguer.Domain.Entities;

namespace FIAP.TechChallenge.ByteMeBurguer.Domain.Repositories
{
    public interface IClienteRepository
    {
        IList<Cliente> GetAll();
        Cliente GetByCpf(string cpf);
        Cliente GetById(int id);
        Task<Cliente> Post(Cliente cliente);
    }
}
