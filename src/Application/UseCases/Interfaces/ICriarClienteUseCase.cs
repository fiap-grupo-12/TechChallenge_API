using FIAP.TechChallenge.ByteMeBurguer.Application.Models.Request;

namespace FIAP.TechChallenge.ByteMeBurguer.Application.UseCases.Interfaces
{
    public interface ICriarClienteUseCase : IUseCaseAsync<CriarClienteRequest, bool>
    {
    }
}
