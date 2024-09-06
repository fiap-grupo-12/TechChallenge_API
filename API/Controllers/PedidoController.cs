using FIAP.TechChallenge.ByteMeBurguer.Application.Models.Request;
using FIAP.TechChallenge.ByteMeBurguer.Application.UseCases.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.TechChallenge.ByteMeBurguer.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : Controller
    {
        private readonly IObterPedidosUseCase _obterPedidos;
        private readonly IObterPedidosFiltradosUseCase _obterPedidosFiltrados;
        private readonly IObterPedidoPorIdUseCase _obterPedidoPorId;
        private readonly IObterStatusPagamentoPorIdUseCase _obterStatusPagamentoPorId;
        private readonly ICriarPedidoUseCase _criarPedido;
        private readonly IAtualizarStatusPedidoUseCase _atualizarStatusPedido;
        private readonly IAtualizarStatusPagamentoUseCase _atualizarStatusPagamento;

        public PedidoController(
            IObterPedidosUseCase obterPedidos,
            IObterPedidosFiltradosUseCase obterPedidosFiltrados,
            IObterPedidoPorIdUseCase obterPedidoPorId,
            IObterStatusPagamentoPorIdUseCase obterStatusPagamentoPorIdUseCase,
            ICriarPedidoUseCase criarPedido,
            IAtualizarStatusPedidoUseCase atualizarStatusPedido,
            IAtualizarStatusPagamentoUseCase atualizarStatusPagamento)
        {
            _obterPedidos = obterPedidos;
            _obterPedidosFiltrados = obterPedidosFiltrados;
            _obterPedidoPorId = obterPedidoPorId;
            _obterStatusPagamentoPorId = obterStatusPagamentoPorIdUseCase;
            _criarPedido = criarPedido;
            _atualizarStatusPedido = atualizarStatusPedido;
            _atualizarStatusPagamento = atualizarStatusPagamento;
        }

        [HttpPost]
        public async Task<IActionResult> CriarPedido(CriarPedidoRequest request)
        {
            try
            {
                var result = await _criarPedido.Execute(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{Id}")]
        public IActionResult ObterPedidoPorId(int Id)
        {
            try
            {
                var result = _obterPedidoPorId.Execute(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        public IActionResult ObterPedidos()
        {
            try
            {
                var result = _obterPedidos.Execute();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("Filtrados")]
        public IActionResult ObterPedidosFiltrados()
        {
            try
            {
                var result = _obterPedidosFiltrados.Execute();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("StatusPagamento/{Id}")]
        public IActionResult ObterStatusPedidoPorId(int Id)
        {
            try
            {
                var result = _obterStatusPagamentoPorId.Execute(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("StatusPedido")]
        public IActionResult AtualizarStatusPedido([FromBody] AtualizarStatusPedidoRequest request)
        {
            try
            {
                _atualizarStatusPedido.Execute(request);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        [Route("StatusPagamento")]
        public async Task<IActionResult> AtualizarStatusPagamento([FromBody] AtualizarStatusPagamentoRequest request)
        {
            try
            {
                await _atualizarStatusPagamento.Execute(request);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
