namespace APIPagamento.Models
{
    public class Pagamento
    {
        public int PagamentoId { get; set; }
        public string? NumCartao { get; set; }
        public string? DataExpiracao { get; set; }
        public string? CVV { get; set; }
        public Pedido? Pedido { get; set; }
        public Guid PedidoId { get; set; }
    }
}
