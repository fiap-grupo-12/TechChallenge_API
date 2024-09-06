namespace FIAP.TechChallenge.ByteMeBurguer.Application.Models.Response
{
    public class ItensDePedidoResponse
    {
        private double _valor;
        public int ProdutoId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Categoria { get; set; } 
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }

        public decimal Subtotal
        {
            get { return Quantidade * ValorUnitario; }
        }
    }
}
