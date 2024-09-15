using FIAP.TechChallenge.ByteMeBurguer.Domain.Entities;
using FIAP.TechChallenge.ByteMeBurguer.Domain.Repositories;
using FIAP.TechChallenge.ByteMeBurguer.Infra.Data.Configurations;

namespace FIAP.TechChallenge.ByteMeBurguer.Infra.Data.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoriaRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IList<Categoria> GetAll()
        {
            try
            {
                return _context.Categorias.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao consultar categorias. {ex}");
            }
        }

        public Categoria GetByName(string nome)
        {
            try
            {
                return _context.Categorias.FirstOrDefault(x => x.Nome == nome);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao consultar categoria. {ex}");
            }
        }
    }
}
