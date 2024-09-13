using FIAP.TechChallenge.ByteMeBurguer.Application.Models.Request;
using FIAP.TechChallenge.ByteMeBurguer.Application.Models.Response;

namespace FIAP.TechChallenge.ByteMeBurguer.Application.UseCases.Interfaces
{
    public interface ICriarPedidoUseCase : IUseCaseAsync<CriarPedidoRequest, PedidoResponse>
    {
    }
}
