using Audicon.Infra.RelationalData.Entity;
using Microsoft.EntityFrameworkCore;

namespace Audicon.Infra.RelationalData.DbContext
{
    public class AudiconDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public AudiconDbContext(DbContextOptions<AudiconDbContext> options) : base(options) { }

        public DbSet<BaseEntity> BaseEntities { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração para campos automáticos
            modelBuilder.Entity<BaseEntity>()
                .Property(b => b.DataCriacao)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<BaseEntity>()
                .Property(b => b.DataAlteracao)
                .ValueGeneratedOnAddOrUpdate();

            modelBuilder.Entity<BaseEntity>()
                .Property(b => b.UsuarioCriacao)
                .IsRequired();

            modelBuilder.Entity<BaseEntity>()
                .Property(b => b.UsuarioAlteracao)
                .IsRequired(false);
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.DataCriacao = DateTime.Now;
                    entry.Entity.UsuarioCriacao = ObterUsuarioAtual(); 
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.DataAlteracao = DateTime.Now;
                    entry.Entity.UsuarioAlteracao = ObterUsuarioAtual(); 
                }
            }

            return base.SaveChanges();
        }

        
        private Guid ObterUsuarioAtual()
        {
            return Guid.NewGuid(); 
        }
    }
}
