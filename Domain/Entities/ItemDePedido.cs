namespace FIAP.TechChallenge.ByteMeBurguer.Domain.Entities
{
    public class ItemDePedido
    {
        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
    }

}
