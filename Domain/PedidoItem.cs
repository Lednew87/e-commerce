namespace Commerce.Domain
{
    public class PedidoItem
    {
        public int Id { get; set; }
        public int PedidosId { get; set; }
        public Pedidos Pedidos { get; set; }
        public int ProdutosId { get; set; }
        public Produtos Produto { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
        public decimal Desconto { get; set; }
    }
}
