using Fiap.Web.Alunos.Models;
using Microsoft.EntityFrameworkCore;
namespace Fiap.Web.Alunos.Data.Contexts
{
    public class DatabaseContext : DbContext
    {
        public DbSet<RepresentanteModel> RepresentanteModels { get; set; }
        public DbSet<ClienteModel> Clientes { get; set; }
        public DbSet<LojaModel> Lojas { get; set; }
        public DbSet<PedidoModel> Pedidos { get; set; }
        public DbSet<FornecedorModel> Fornecedores { get; set; }
        public DbSet<ProdutoModel> Produtos{ get; set; }
        public DbSet<PedidoProdutoModel> PedidoProdutos { get; set; }


        public class LojaModel
        {
            public int LojaId { get; set; }
            public string Nome { get; set; }
            public string Endereco { get; set; }
            // Relacionamento com Pedido
            public List<PedidoModel> Pedidos { get; set; }
        }


        public class PedidoModel
        {
            public int PedidoId { get; set; }
            public DateTime DataPedido { get; set; }
            // Relacionamento com Cliente
            public int ClienteId { get; set; }
            public ClienteModel Cliente { get; set; }
            // Relacionamento com Loja
            public int LojaId { get; set; }
            public LojaModel Loja { get; set; }
            // Relacionamento com Produto
            public List<PedidoProdutoModel> PedidoProdutos { get; set; }
        }

        public class FornecedorModel
        {
            public int FornecedorId { get; set; }
            public string Nome { get; set; }
            // Relacionamento com Produto
            public List<ProdutoModel> Produtos { get; set; }
        }

        public class ProdutoModel
        {
            public int ProdutoId { get; set; }
            public string Nome { get; set; }
            public decimal Preco { get; set; }
            public string Descricao { get; set; }
            // Relacionamento com Fornecedor
            public int FornecedorId { get; set; }
            public FornecedorModel Fornecedor { get; set; }
            // Relacionamento com Pedido
            public List<PedidoProdutoModel> PedidoProdutos { get; set; }
        }

        public class PedidoProdutoModel
        {
            public int PedidoId { get; set; }
            public PedidoModel Pedido { get; set; }
            public int ProdutoId { get; set; }
            public ProdutoModel Produto { get; set; }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RepresentanteModel>(entity =>
            {
                // Definindo um nome para tabela
                entity.ToTable("Representantes");
                // Definindo chave primária
                entity.HasKey(e => e.RepresentanteId);
                // Tornando o nome obrigatório
                entity.Property(e => e.NomeRepresentante).IsRequired();
                // Adicionando índice único para CPF
                entity.HasIndex(e => e.CPF).IsUnique();
            });

            modelBuilder.Entity<ClienteModel>(entity =>
            {
                entity.ToTable("Clientes");
                entity.HasKey(e => e.ClienteId);
                entity.Property(e => e.Nome).IsRequired();
                entity.Property(e => e.Email).IsRequired();
                entity.Property(e => e.DataNascimento).HasColumnType("date");
                entity.Property(e => e.Observacao).HasMaxLength(500);
                entity.HasOne(e => e.Representante).WithMany()
                .HasForeignKey(e => e.RepresentanteId).IsRequired();
            });

            modelBuilder.Entity<ProdutoModel>(entity =>
            {
                entity.ToTable("Produtos");
                entity.HasKey(p => p.ProdutoId);
                entity.Property(p => p.Nome).IsRequired();
                entity.Property(p => p.Descricao);
                entity.Property(p => p.Preco).HasColumnType("NUMBER(18,2)");
                
                entity.HasOne(p => p.Fornecedor)
                      .WithMany(f => f.Produtos)
                      .HasForeignKey(p => p.FornecedorId);
            });
            // Configuração para LojaModel
            modelBuilder.Entity<LojaModel>(entity =>
            {
                entity.ToTable("Lojas");
                entity.HasKey(l => l.LojaId);
                entity.Property(l => l.Nome).IsRequired();
                entity.Property(l => l.Endereco);
                // Relacionamento com PedidoModel
                entity.HasMany(l => l.Pedidos)
                      .WithOne(p => p.Loja)
                      .HasForeignKey(p => p.LojaId);
            });
            // Configuração para PedidoModel
            modelBuilder.Entity<PedidoModel>(entity =>
            {
                entity.ToTable("Pedidos");
                entity.HasKey(p => p.PedidoId);
                entity.Property(p => p.DataPedido).HasColumnType("DATE");
                // Relacionamento com ClienteModel
                entity.HasOne(p => p.Cliente)
                      .WithMany()
                      .HasForeignKey(p => p.ClienteId);
                // Configuração de muitos para muitos: PedidoModel e ProdutoModel
                entity.HasMany(p => p.PedidoProdutos)
                      .WithOne(pp => pp.Pedido)
                      .HasForeignKey(pp => pp.PedidoId);
            });
            // Configuração para FornecedorModel
            modelBuilder.Entity<FornecedorModel>(entity =>
            {
                entity.ToTable("Fornecedores");
                entity.HasKey(f => f.FornecedorId);
                entity.Property(f => f.Nome).IsRequired();
            });
            // Configuração para PedidoProdutoModel (relacionamento muitos-para-muitos)
            modelBuilder.Entity<PedidoProdutoModel>(entity =>
            {
                entity.HasKey(pp => new { pp.PedidoId, pp.ProdutoId });
                entity.HasOne(pp => pp.Pedido)
                      .WithMany(p => p.PedidoProdutos)
                      .HasForeignKey(pp => pp.PedidoId);
                entity.HasOne(pp => pp.Produto)
                      .WithMany(p => p.PedidoProdutos)
                      .HasForeignKey(pp => pp.ProdutoId);
            });

            modelBuilder.Entity<RepresentanteModel>().HasData(
                new RepresentanteModel { RepresentanteId = 1, NomeRepresentante = "João da Silva", CPF = "26924456101" },
                new RepresentanteModel { RepresentanteId = 2, NomeRepresentante = "Maria Alencar", CPF = "26924456102" },
                new RepresentanteModel { RepresentanteId = 3, NomeRepresentante = "Alcides Souza", CPF = "26924456103" }
    );

        }

        


        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }
        protected DatabaseContext()
        {
        }
    }
}

