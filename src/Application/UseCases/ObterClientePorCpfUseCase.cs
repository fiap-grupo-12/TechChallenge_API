using AutoMapper;
using FIAP.TechChallenge.ByteMeBurguer.Application.Models.Response;
using FIAP.TechChallenge.ByteMeBurguer.Application.UseCases.Interfaces;
using FIAP.TechChallenge.ByteMeBurguer.Domain.Repositories;

namespace FIAP.TechChallenge.ByteMeBurguer.Application.UseCases
{
    public class ObterClientePorCpfUseCase : IObterClientePorCpfUseCase
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;

        public ObterClientePorCpfUseCase(
            IClienteRepository clienteRepository, IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }

        public ClienteResponse Execute(string cpf)
        {
            var result = _clienteRepository.GetByCpf(cpf);

            return _mapper.Map<ClienteResponse>(result);
        }
    }
}