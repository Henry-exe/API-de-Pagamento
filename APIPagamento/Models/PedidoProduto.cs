namespace APIPagamento.Models
{
    public class PedidoProduto
    {
        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }

        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
        public PedidoProduto()
        {
            Pedido = new Pedido();
            Produto = new Produto();
        }

    }
}
