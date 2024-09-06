using FIAP.TechChallenge.ByteMeBurguer.Application.Models.Request;

namespace FIAP.TechChallenge.ByteMeBurguer.Application.UseCases.Interfaces
{
    public interface ICriarProdutoUseCase : IUseCaseAsync<CriarProdutoRequest, bool>
    {
    }
}
