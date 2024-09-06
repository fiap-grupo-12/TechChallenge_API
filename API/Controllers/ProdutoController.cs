using FIAP.TechChallenge.ByteMeBurguer.Application.Models.Request;
using FIAP.TechChallenge.ByteMeBurguer.Application.UseCases.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.TechChallenge.ByteMeBurguer.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : Controller
    {
        private readonly IObterProdutoPorCategoriaUseCase _obterProdutoPorCategoria;
        private readonly ICriarProdutoUseCase _criarProduto;
        private readonly IAtualizarProdutoUseCase _atualizarProduto;
        private readonly IRemoverProdutoUseCase _removerProduto;

        public ProdutoController(
            IObterProdutoPorCategoriaUseCase obterProdutoPorCategoria,
            ICriarProdutoUseCase criarProduto,
            IAtualizarProdutoUseCase atualizarProduto,
            IRemoverProdutoUseCase removerProduto
            )
        {
            _obterProdutoPorCategoria = obterProdutoPorCategoria;
            _criarProduto = criarProduto;
            _atualizarProduto = atualizarProduto;
            _removerProduto = removerProduto;
        }

        [HttpGet("{categoria}")]
        public IActionResult ObterPorCategoria(string categoria)
        {
            try
            {
                var result = _obterProdutoPorCategoria.Execute(categoria);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Criar(CriarProdutoRequest request)
        {
            try
            {
                var result = await _criarProduto.Execute(request);

                if(!result)
                    return BadRequest("Categoria informada inválida!");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Atualizar(AtualizarProdutoRequest request)
        {
            try
            {
                var result = await _atualizarProduto.Execute(request);

                if (!result)
                    return BadRequest("Categoria informada inválida!");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Remover(int id)
        {
            try
            {
                var result = await _removerProduto.Execute(id);

                if(!result)
                    return BadRequest("Produto não encontrado.");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
