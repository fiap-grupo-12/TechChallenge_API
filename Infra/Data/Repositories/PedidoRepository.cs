using FIAP.TechChallenge.ByteMeBurguer.Domain.Entities;
using FIAP.TechChallenge.ByteMeBurguer.Domain.Entities.Enum;
using FIAP.TechChallenge.ByteMeBurguer.Domain.Repositories;
using FIAP.TechChallenge.ByteMeBurguer.Infra.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace FIAP.TechChallenge.ByteMeBurguer.Infra.Data.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly ApplicationDbContext _context;

        public PedidoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Pedido> GetAll()
        {
            try
            {
                return _context.Pedidos
                    .Include(x => x.Cliente)
                    .Include(x => x.FormaPagamento)
                    .Include(x => x.ItensDePedido)
                        .ThenInclude(y => y.Produto)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao consultar pedidos. {ex}");
            }
        }

        public Pedido GetById(int Id)
        {
            try
            {
                return _context.Pedidos
                    .Include(x => x.Cliente)
                    .Include(x => x.FormaPagamento)
                    .FirstOrDefault(x => x.Id == Id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao consultar pedido {Id}. {ex}");
            }
        }

        public IList<Pedido> GetByStatus(StatusPedido status)
        {
            try
            {
                return _context.Pedidos
                    .Include(x => x.Cliente)
                    .Include(x => x.FormaPagamento)
                    .Where(x => x.StatusPedido == status).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao consultar pedidos. {ex}");
            }
        }

        public IList<Pedido> GetFiltrados()
        {
            try
            {
                return _context.Pedidos
                    .Include(x => x.Cliente)
                    .Include(x => x.FormaPagamento)
                    .Include(x => x.ItensDePedido)
                        .ThenInclude(y => y.Produto)
                    .Where(x => x.StatusPedido != StatusPedido.Finalizado)
                    .OrderBy(x => x.StatusPedido == StatusPedido.Pronto ? 0 :
                                  x.StatusPedido == StatusPedido.EmPreparacao ? 1 :
                                  x.StatusPedido == StatusPedido.Recebido ? 2 : 3)
                    .ThenBy(x => x.DataCriacao)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao consultar pedidos filtrados. {ex}");
            }
        }

        public async Task<Pedido> Post(Pedido pedido)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    pedido.IdGuid = Guid.NewGuid();

                    _context.Pedidos.Add(pedido);
                    await _context.SaveChangesAsync();

                    foreach (var item in pedido.ItensDePedido)
                    {
                        item.PedidoId = pedido.Id;
                    }

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return pedido;
                }
                catch (Exception ex)
                {
                    // Reverter a transação em caso de erro
                    await transaction.RollbackAsync();
                    throw new Exception($"Erro ao cadastrar pedido. {ex.Message}", ex);
                }
            }
        }

        public async Task Update(Pedido pedido, int Id)
        {
            try
            {
                _context.Pedidos.Update(pedido);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar pedido. {ex}");
            }
        }
    }
}
