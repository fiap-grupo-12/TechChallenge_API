using FIAP.TechChallenge.ByteMeBurguer.Domain.Entities;
using FIAP.TechChallenge.ByteMeBurguer.Domain.Entities.Enum;

namespace FIAP.TechChallenge.ByteMeBurguer.Domain.Repositories
{
    public interface IPedidoRepository
    {
        Task<Pedido> Post(Pedido pedido);
        Task Update(Pedido pedido, int Id);
        Pedido GetById(int Id);
        IList<Pedido> GetAll();
        IList<Pedido> GetFiltrados();
        IList<Pedido> GetByStatus(StatusPedido status);
    }
}
