using FIAP.TechChallenge.ByteMeBurguer.Application.Models.Request;
using FIAP.TechChallenge.ByteMeBurguer.Application.UseCases.Interfaces;
using FIAP.TechChallenge.ByteMeBurguer.Domain.Entities;
using FIAP.TechChallenge.ByteMeBurguer.Domain.Repositories;

namespace FIAP.TechChallenge.ByteMeBurguer.Application.UseCases
{
    public class AtualizarStatusPagamentoUseCase(IPedidoRepository pedidoRepository) : IAtualizarStatusPagamentoUseCase
    {
        async Task<bool> IUseCaseAsync<AtualizarStatusPagamentoRequest, bool>.Execute(AtualizarStatusPagamentoRequest request)
        {
            Pedido pedido = pedidoRepository.GetById(request.PedidoId);

            if (pedido != null)
            {
                if (pedido.StatusPagamento == request.StatusPagamento) 
                    return true;

                pedido.StatusPagamento = request.StatusPagamento;
                await pedidoRepository.Update(pedido, pedido.Id);

                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
