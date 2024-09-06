using FIAP.TechChallenge.ByteMeBurguer.Domain.Entities;

namespace FIAP.TechChallenge.ByteMeBurguer.Domain.Repositories
{
    public interface IFormaPagamentoRepository
    {
        IList<FormaPagamento> GetAll();
        FormaPagamento GetById(int id);
    }
}
