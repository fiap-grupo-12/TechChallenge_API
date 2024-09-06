using FIAP.TechChallenge.ByteMeBurguer.Application.Models.Response;

namespace FIAP.TechChallenge.ByteMeBurguer.Application.UseCases.Interfaces
{
    public interface IObterPedidoPorIdUseCase : IUseCase<int, PedidoResponse>
    {
    }
}
