using AutoMapper;
using FIAP.TechChallenge.ByteMeBurguer.Application.Models.Response;
using FIAP.TechChallenge.ByteMeBurguer.Application.UseCases.Interfaces;
using FIAP.TechChallenge.ByteMeBurguer.Domain.Repositories;

namespace FIAP.TechChallenge.ByteMeBurguer.Application.UseCases
{
    public class ObterPedidoPorIdUseCase : IObterPedidoPorIdUseCase
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IMapper _mapper;

        public ObterPedidoPorIdUseCase(
            IPedidoRepository pedidoRepository,
            IMapper mapper)
        {
            _pedidoRepository = pedidoRepository;
            _mapper = mapper;
        }

        public PedidoResponse Execute(int Id)
        {
            var result = _pedidoRepository.GetById(Id);

            return _mapper.Map<PedidoResponse>(result);
        }
    }
}