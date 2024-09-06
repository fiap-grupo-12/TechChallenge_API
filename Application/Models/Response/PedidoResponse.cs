namespace FIAP.TechChallenge.ByteMeBurguer.Application.Models.Response
{
    public class PedidoResponse
    {
        public int Id { get; set; }
        public Guid IdGuid { get; set; }
        public DateTime? DataCriacao { get; set; }
        public string StatusPedido { get; set; }
        public string StatusPagamento { get; set; }
        public decimal ValorTotal
        {
            get { return ItensDePedido?.Sum(item => item.Subtotal) ?? 0; }
        }
        public ClienteResponse Cliente { get; set; }
        public FormaPagamentoResponse FormaPagamento { get; set; }
        public IList<ItensDePedidoResponse> ItensDePedido { get; set; }
    }
}
