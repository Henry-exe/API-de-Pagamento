namespace APIPagamento.Models
{
    public class Pedido
    {
        public int IdPedido { get; set; }
        public string? CustomerName { get; set; }
        public List<PedidoProduto>? PedidoProdutos { get; set; }
        public Pagamento? Pagamento { get; set; }
    }
}
