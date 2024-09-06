using FIAP.TechChallenge.ByteMeBurguer.Domain.Entities;
using FIAP.TechChallenge.ByteMeBurguer.Application.Models.Request;
using FIAP.TechChallenge.ByteMeBurguer.Application.UseCases.Interfaces;
using FIAP.TechChallenge.ByteMeBurguer.Domain.Entities.Enum;
using FIAP.TechChallenge.ByteMeBurguer.Domain.Repositories;
using FIAP.TechChallenge.ByteMeBurguer.Application.Models.Response;
using AutoMapper;

namespace FIAP.TechChallenge.ByteMeBurguer.Application.UseCases
{
    public class CriarPedidoUseCase : ICriarPedidoUseCase
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IFormaPagamentoRepository _formaPagamentoRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;

        public CriarPedidoUseCase(
            IPedidoRepository petoRepository,
            IClienteRepository clienteRepository,
            IFormaPagamentoRepository formaPagamentoRepository,
            IProdutoRepository produtoRepository,
            IMapper mapper)
        {
            _pedidoRepository = petoRepository;
            _clienteRepository = clienteRepository;
            _formaPagamentoRepository = formaPagamentoRepository;
            _produtoRepository = produtoRepository;
            _mapper = mapper;   
        }

        public async Task<PedidoResponse> Execute(CriarPedidoRequest request)
        {
            Cliente cliente = null;

            if (!string.IsNullOrEmpty(request.CpfCliente))
                cliente = _clienteRepository.GetByCpf(request.CpfCliente);
            if (cliente == null)
            {
                throw new Exception("Cliente não encontrado.");
            }
                
            var formaPagamento = _formaPagamentoRepository.GetById(request.IdFormaPagamento) ?? throw new Exception("Forma de pagamento não encontrada.");
            var itensDePedido = new List<ItemDePedido>();

            foreach (var item in request.ProdutosQuantidade)    
            {
                var produto = _produtoRepository.GetById(item.IdProduto) ?? throw new Exception($"Produto com ID {item.IdProduto} não encontrado.");
                itensDePedido.Add(
                    new ItemDePedido { 
                        ProdutoId = produto.Id,
                        Quantidade = item.Quantidade 
                    });
            }

            var pedido = new Pedido()
            {
                Cliente = cliente,
                FormaPagamento = formaPagamento,
                ItensDePedido = itensDePedido,
                DataCriacao = DateTime.Now,
                StatusPedido = StatusPedido.Recebido,
                StatusPagamento = StatusPagamento.Pendente
            };

            var result = await _pedidoRepository.Post(pedido);

            return _mapper.Map<PedidoResponse>(result); 

        }
    }
}
