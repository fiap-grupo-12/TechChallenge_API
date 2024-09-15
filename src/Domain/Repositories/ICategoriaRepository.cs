using FIAP.TechChallenge.ByteMeBurguer.Domain.Entities;

namespace FIAP.TechChallenge.ByteMeBurguer.Domain.Repositories
{
    public interface ICategoriaRepository
    {
        IList<Categoria> GetAll();
        Categoria GetByName(string nome);
    }
}
