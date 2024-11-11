using Desafio.Domain.Entity;
using Desafio.Domain.Utils.Hash;
using Microsoft.EntityFrameworkCore;

namespace Desafio.Infra.Context
{
    public class DesafioContext : DbContext
    {
        public DesafioContext(DbContextOptions<DesafioContext> dbContextOptions) : base(dbContextOptions)
        {
            #region Migration Exist?
            try
            {
                if (Database?.GetAppliedMigrations()?.ToList()?.Count == 0)
                    Database?.Migrate();
            }
            catch (Exception) { }
            #endregion

            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            builder.Entity<User>().HasData(
                new User { Id = 1, Name = "Admin", Surname = "Admin", Email = "admin", Password = HashService.HashPassword("admin"), AcessLevel = "Admin", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new User { Id = 2, Name = "Eduardo", Surname = "Sales", Email = "eduardo.sales@gmail.com", Password = HashService.HashPassword("1234"), AcessLevel = "Comum", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new User { Id = 3, Name = "Ana", Surname = "Pereira", Email = "ana.pereira@yahoo.com", Password = HashService.HashPassword("abcd"), AcessLevel = "Admin", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new User { Id = 4, Name = "Carlos", Surname = "Oliveira", Email = "carlos.oliveira@gmail.com", Password = HashService.HashPassword("5678"), AcessLevel = "Comum", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new User { Id = 5, Name = "Felipe", Surname = "Mendes", Email = "felipe.mendes@hotmail.com", Password = HashService.HashPassword("senha123"), AcessLevel = "Comum", IsActive = false, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new User { Id = 6, Name = "Juliana", Surname = "Almeida", Email = "juliana.almeida@outlook.com", Password = HashService.HashPassword("pass1234"), AcessLevel = "Admin", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new User { Id = 7, Name = "Ricardo", Surname = "Costa", Email = "ricardo.costa@gmail.com", Password = HashService.HashPassword("1234abcd"), AcessLevel = "Comum", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new User { Id = 8, Name = "Mariana", Surname = "Lima", Email = "mariana.lima@yahoo.com", Password = HashService.HashPassword("senha567"), AcessLevel = "Admin", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new User { Id = 9, Name = "Lucas", Surname = "Pereira", Email = "lucas.pereira@hotmail.com", Password = HashService.HashPassword("senha876"), AcessLevel = "Comum", IsActive = false, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new User { Id = 10, Name = "Giovana", Surname = "Rocha", Email = "giovana.rocha@outlook.com", Password = HashService.HashPassword("senha1234"), AcessLevel = "Comum", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new User { Id = 11, Name = "João", Surname = "Silva", Email = "joao.silva@gmail.com", Password = HashService.HashPassword("1234joao"), AcessLevel = "Comum", IsActive = false, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new User { Id = 12, Name = "Raquel", Surname = "Alves", Email = "raquel.alves@gmail.com", Password = HashService.HashPassword("senha890"), AcessLevel = "Admin", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new User { Id = 13, Name = "Roberta", Surname = "Martins", Email = "roberta.martins@outlook.com", Password = HashService.HashPassword("senha456"), AcessLevel = "Comum", IsActive = false, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new User { Id = 14, Name = "Paulo", Surname = "Santos", Email = "paulo.santos@yahoo.com", Password = HashService.HashPassword("pass876"), AcessLevel = "Comum", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new User { Id = 15, Name = "Marcos", Surname = "Souza", Email = "marcos.souza@gmail.com", Password = HashService.HashPassword("senha12345"), AcessLevel = "Admin", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new User { Id = 16, Name = "Luciana", Surname = "Ferreira", Email = "luciana.ferreira@hotmail.com", Password = HashService.HashPassword("12345luciana"), AcessLevel = "Comum", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new User { Id = 17, Name = "Vinícius", Surname = "Barros", Email = "vinicius.barros@outlook.com", Password = HashService.HashPassword("senha321"), AcessLevel = "Admin", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new User { Id = 18, Name = "Patrícia", Surname = "Dias", Email = "patricia.dias@gmail.com", Password = HashService.HashPassword("senha987"), AcessLevel = "Comum", IsActive = false, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new User { Id = 19, Name = "André", Surname = "Nascimento", Email = "andre.nascimento@gmail.com", Password = HashService.HashPassword("senha54321"), AcessLevel = "Admin", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new User { Id = 20, Name = "Tatiane", Surname = "Carvalho", Email = "tatiane.carvalho@yahoo.com", Password = HashService.HashPassword("1234senha"), AcessLevel = "Comum", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new User { Id = 21, Name = "Marcela", Surname = "Costa", Email = "marcela.costa@gmail.com", Password = HashService.HashPassword("senha789"), AcessLevel = "Comum", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new User { Id = 22, Name = "José", Surname = "Lopes", Email = "jose.lopes@outlook.com", Password = HashService.HashPassword("senha432"), AcessLevel = "Admin", IsActive = false, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new User { Id = 23, Name = "Carla", Surname = "Melo", Email = "carla.melo@hotmail.com", Password = HashService.HashPassword("senha654"), AcessLevel = "Comum", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
            );

            base.OnModelCreating(builder);
        }

    }
}
