using FIAP.TechChallenge.ByteMeBurguer.Application.Models.Response;

namespace FIAP.TechChallenge.ByteMeBurguer.Application.UseCases.Interfaces
{
    public interface IObterProdutoPorCategoriaUseCase : IUseCase<string, IList<ProdutoResponse>>
    {
    }
}
