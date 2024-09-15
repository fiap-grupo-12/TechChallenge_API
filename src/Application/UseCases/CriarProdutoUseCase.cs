using FIAP.TechChallenge.ByteMeBurguer.Domain.Entities;
using FIAP.TechChallenge.ByteMeBurguer.Application.Models.Request;
using FIAP.TechChallenge.ByteMeBurguer.Application.UseCases.Interfaces;
using FIAP.TechChallenge.ByteMeBurguer.Domain.Repositories;

namespace FIAP.TechChallenge.ByteMeBurguer.Application.UseCases
{
    public class CriarProdutoUseCase : ICriarProdutoUseCase
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly ICategoriaRepository _categoriaRepository;

        public CriarProdutoUseCase(IProdutoRepository repository, ICategoriaRepository categoriaRepository)
        {
            _produtoRepository = repository;
            _categoriaRepository = categoriaRepository;
        }

        public async Task<bool> Execute(CriarProdutoRequest request)
        {
            var categoria = _categoriaRepository.GetByName(request.NomeCategoria);
            
            if(categoria is null)
                return false;
            
            var produto = new Produto()
            {
                Nome = request.Nome,
                Descricao = request.Descricao,
                Valor = request.Valor,
                CategoriaProduto = categoria
            };

            await _produtoRepository.Post(produto);

            return true;
        }
    }
}
