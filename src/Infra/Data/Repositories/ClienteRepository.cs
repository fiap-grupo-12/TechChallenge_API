using FIAP.TechChallenge.ByteMeBurguer.Domain.Entities;
using FIAP.TechChallenge.ByteMeBurguer.Domain.Repositories;
using FIAP.TechChallenge.ByteMeBurguer.Infra.Data.Configurations;

namespace FIAP.TechChallenge.ByteMeBurguer.Infra.Data.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ApplicationDbContext _context;

        public ClienteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Cliente> GetAll()
        {
            try
            {
                return _context.Clientes.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao consultar clientes. {ex}");
            }
        }

        public Cliente GetByCpf(string cpf)
        {
            try
            {
                return _context.Clientes.FirstOrDefault(x => x.CPF == cpf);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao consultar cliente. {ex}");
            }
        }

        public Cliente GetById(int id)
        {
            try
            {
                return _context.Clientes.FirstOrDefault(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao consultar cliente. {ex}");
            }
        }

        public async Task<Cliente> Post(Cliente cliente)
        {
            try
            {
                _context.Clientes.Add(cliente);
                await _context.SaveChangesAsync();
                return cliente;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao cadastrar cliente. {ex}");
            }
        }
    }
}
