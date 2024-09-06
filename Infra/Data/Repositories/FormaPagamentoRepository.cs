using FIAP.TechChallenge.ByteMeBurguer.Domain.Entities;
using FIAP.TechChallenge.ByteMeBurguer.Domain.Repositories;
using FIAP.TechChallenge.ByteMeBurguer.Infra.Data.Configurations;

namespace FIAP.TechChallenge.ByteMeBurguer.Infra.Data.Repositories
{
    public class FormaPagamentoRepository : IFormaPagamentoRepository
    {
        private readonly ApplicationDbContext _context;

        public FormaPagamentoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<FormaPagamento> GetAll()
        {
            try
            {
                return _context.FormasPagamento.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao consultar Formas de pagamentos. {ex}");
            }
        }

        public FormaPagamento GetById(int id)
        {
            try
            {
                return _context.FormasPagamento.FirstOrDefault(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao consultar Forma de pagamento. {ex}");
            }
        }
    }
}
