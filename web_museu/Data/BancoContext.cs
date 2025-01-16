using Microsoft.EntityFrameworkCore;
using web_museu.Models;

namespace web_museu.Data
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options) { }

        // Mapeamento das entidades
        public DbSet<ContatoModel> Contatos { get; set; }
        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<QuestionarioModel> Questionarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração da tabela Contatos
            modelBuilder.Entity<ContatoModel>(entity =>
            {
                entity.ToTable("Contatos"); // Nome da tabela
                entity.HasKey(c => c.IdContato); // Define a chave primária
                entity.Property(c => c.Nome)
                      .HasMaxLength(200)
                      .IsRequired();
                entity.Property(c => c.Email)
                      .HasMaxLength(200)
                      .IsRequired();
                entity.Property(c => c.Celular)
                      .HasMaxLength(20)
                      .IsRequired();
                entity.Property(c => c.DataCriacao)
                      .HasColumnType("datetime")
                      .IsRequired();
            });

            // Configuração da tabela Usuarios
            modelBuilder.Entity<UsuarioModel>(entity =>
            {
                entity.ToTable("Usuarios"); // Nome da tabela
                entity.HasKey(u => u.Id); // Define a chave primária
                entity.Property(u => u.Nome)
                      .HasMaxLength(200)
                      .IsRequired();
                entity.Property(u => u.Email)
                      .HasMaxLength(200)
                      .IsRequired();
                entity.Property(u => u.Login)
                      .HasMaxLength(100)
                      .IsRequired();
                entity.Property(u => u.Senha)
                      .HasMaxLength(100)
                      .IsRequired();
                entity.Property(u => u.Perfil)
                      .IsRequired();
                entity.Property(u => u.DataCadastro)
                      .HasColumnType("datetime")
                      .IsRequired();
                entity.Property(u => u.DataAtualizacao)
                      .HasColumnType("datetime");
            });

            // Configuração da tabela Questionarios
            modelBuilder.Entity<QuestionarioModel>(entity =>
            {
                entity.ToTable("Questionarios"); // Nome da tabela
                entity.HasKey(q => q.Id); // Define a chave primária
                entity.Property(q => q.AvaliacaoDesign)
                      .IsRequired();
                entity.Property(q => q.AvaliacaoUsabilidade)
                      .IsRequired();
                entity.Property(q => q.AvaliacaoConteudo)
                      .IsRequired();
                entity.Property(q => q.Sugestao)
                      .HasMaxLength(500); // Limita a sugestão a 500 caracteres
                entity.Property(q => q.DataCriacao)
                      .HasColumnType("datetime")
                      .IsRequired();

                // Configura o relacionamento com ContatoModel
                entity.HasOne(q => q.Contato)
                      .WithMany() // Um contato pode ter vários questionários
                      .HasForeignKey(q => q.ContatoId) // Chave estrangeira
                      .OnDelete(DeleteBehavior.Cascade); // Define o comportamento ao excluir o contato
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
