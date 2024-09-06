using FIAP.TechChallenge.ByteMeBurguer.Domain.Entities;

namespace FIAP.TechChallenge.ByteMeBurguer.Domain.Repositories
{
    public interface IProdutoRepository
    {
        Produto GetById(int Id);
        Task Delete(Produto produto);
        Task<Produto> Post(Produto produto);
        Task<Produto> Update(Produto produto);
        IList<Produto> GetAll();
        IList<Produto> GetByCategoria(int IdCategoria);
    }
}
