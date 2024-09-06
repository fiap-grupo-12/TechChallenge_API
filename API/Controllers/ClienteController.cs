using FIAP.TechChallenge.ByteMeBurguer.Application.Models.Request;
using FIAP.TechChallenge.ByteMeBurguer.Application.UseCases.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.TechChallenge.ByteMeBurguer.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : Controller
    {
        private readonly ICriarClienteUseCase _criarClientes;
        private readonly IObterClientePorCpfUseCase _obterClientePorCpf;

        public ClienteController(
            ICriarClienteUseCase criarClientes,
            IObterClientePorCpfUseCase obterClientePorCpf)
        {
            _criarClientes = criarClientes;
            _obterClientePorCpf = obterClientePorCpf;
        }

        [HttpGet("{cpf}")]
        public IActionResult ObterClientePorCpf(string cpf)
        {
            try
            {
                var result = _obterClientePorCpf.Execute(cpf);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CriarClientes(CriarClienteRequest request)
        {
            try
            {
                var result = await _criarClientes.Execute(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
