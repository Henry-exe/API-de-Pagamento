using APIPagamento.Models;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;

namespace APIPagamento.Context
{
    public class SqlContext : DbContext
    {
       public SqlContext(DbContextOptions<SqlContext> options) : base(options) { }
        public DbSet<Pagamento> Pagamentos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Produto> produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pedido>()
            .HasNoKey();
            modelBuilder.Entity<Pedido>()
                .HasOne(pedido => pedido.Pagamento)
                .WithOne(pagamento => pagamento.Pedido)
                .HasForeignKey<Pagamento>(pagamento => pagamento.PedidoId);

            modelBuilder.Entity<PedidoProduto>()
                .HasKey(pp => new { pp.PedidoId, pp.ProdutoId }); // Chave primária composta

            modelBuilder.Entity<PedidoProduto>()
                .HasOne(pp => pp.Pedido)
                .WithMany(pedido => pedido.PedidoProdutos)
                .HasForeignKey(pp => pp.PedidoId);

            modelBuilder.Entity<PedidoProduto>()
                .HasOne(pp => pp.Produto)
                .WithMany()
                .HasForeignKey(pp => pp.ProdutoId);

            modelBuilder.Entity<Produto>().ToTable("Produtos");
            modelBuilder.Entity<Pedido>().ToTable("Pedidos");
            modelBuilder.Entity<Pagamento>().ToTable("Pagamentos");
        }

    }
}
