using FIAP.TechChallenge.ByteMeBurguer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FIAP.TechChallenge.ByteMeBurguer.Infra.Data.Configurations
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<FormaPagamento> FormasPagamento { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ItemDePedido> ItensDePedido { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>()
                .Property(x => x.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<FormaPagamento>()
                .Property(x => x.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Cliente>()
                .Property(x => x.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Produto>()
                .Property(x => x.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<ItemDePedido>()
                .HasKey(ip => new { ip.PedidoId, ip.ProdutoId });

            modelBuilder.Entity<ItemDePedido>()
                .HasOne(ip => ip.Pedido)
                .WithMany(p => p.ItensDePedido)
                .HasForeignKey(ip => ip.PedidoId);

            modelBuilder.Entity<ItemDePedido>()
                .HasOne(ip => ip.Produto)
                .WithMany()
                .HasForeignKey(ip => ip.ProdutoId);

            base.OnModelCreating(modelBuilder);
        }


    }


}
