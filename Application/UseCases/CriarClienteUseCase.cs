using FIAP.TechChallenge.ByteMeBurguer.Domain.Entities;
using FIAP.TechChallenge.ByteMeBurguer.Application.Models.Request;
using FIAP.TechChallenge.ByteMeBurguer.Application.UseCases.Interfaces;
using FIAP.TechChallenge.ByteMeBurguer.Domain.Repositories;

namespace FIAP.TechChallenge.ByteMeBurguer.Application.UseCases
{
    public class CriarClienteUseCase : ICriarClienteUseCase
    {
        private readonly IClienteRepository _clienteRepository;

        public CriarClienteUseCase(
            IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<bool> Execute(CriarClienteRequest request)
        {
            var cliente = new Cliente()
            {
                CPF = request.CPF,
                Nome = request.Nome,
                Email = request.Email
            };
            await _clienteRepository.Post(cliente);

            return true;
        }
    }
}
