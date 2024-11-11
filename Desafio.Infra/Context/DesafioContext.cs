﻿using Desafio.Domain.Entity;
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
            builder.Entity<User>().HasData(
                new User { Id = 1, Name = "Admin", Surname = "Admin", Email = "admin", Password = "admin", AcessLevel = "Admin", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new User { Id = 2, Name = "Eduardo", Surname = "Sales", Email = "eduardo.sales@gmail.com", Password = "1234", AcessLevel = "Comum", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new User { Id = 3, Name = "Ana", Surname = "Pereira", Email = "ana.pereira@yahoo.com", Password = "abcd", AcessLevel = "Admin", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new User { Id = 4, Name = "Carlos", Surname = "Oliveira", Email = "carlos.oliveira@gmail.com", Password = "5678", AcessLevel = "Comum", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new User { Id = 5, Name = "Felipe", Surname = "Mendes", Email = "felipe.mendes@hotmail.com", Password = "senha123", AcessLevel = "Comum", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new User { Id = 6, Name = "Juliana", Surname = "Almeida", Email = "juliana.almeida@outlook.com", Password = "pass1234", AcessLevel = "Admin", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new User { Id = 7, Name = "Ricardo", Surname = "Costa", Email = "ricardo.costa@gmail.com", Password = "1234abcd", AcessLevel = "Comum", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new User { Id = 8, Name = "Mariana", Surname = "Lima", Email = "mariana.lima@yahoo.com", Password = "senha567", AcessLevel = "Admin", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new User { Id = 9, Name = "Lucas", Surname = "Pereira", Email = "lucas.pereira@hotmail.com", Password = "senha876", AcessLevel = "Comum", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new User { Id = 10, Name = "Giovana", Surname = "Rocha", Email = "giovana.rocha@outlook.com", Password = "senha1234", AcessLevel = "Comum", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new User { Id = 11, Name = "João", Surname = "Silva", Email = "joao.silva@gmail.com", Password = "1234joao", AcessLevel = "Comum", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new User { Id = 12, Name = "Raquel", Surname = "Alves", Email = "raquel.alves@gmail.com", Password = "senha890", AcessLevel = "Admin", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new User { Id = 13, Name = "Roberta", Surname = "Martins", Email = "roberta.martins@outlook.com", Password = "senha456", AcessLevel = "Comum", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new User { Id = 14, Name = "Paulo", Surname = "Santos", Email = "paulo.santos@yahoo.com", Password = "pass876", AcessLevel = "Comum", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new User { Id = 15, Name = "Marcos", Surname = "Souza", Email = "marcos.souza@gmail.com", Password = "senha12345", AcessLevel = "Admin", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new User { Id = 16, Name = "Luciana", Surname = "Ferreira", Email = "luciana.ferreira@hotmail.com", Password = "12345luciana", AcessLevel = "Comum", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new User { Id = 17, Name = "Vinícius", Surname = "Barros", Email = "vinicius.barros@outlook.com", Password = "senha321", AcessLevel = "Admin", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new User { Id = 18, Name = "Patrícia", Surname = "Dias", Email = "patricia.dias@gmail.com", Password = "senha987", AcessLevel = "Comum", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new User { Id = 19, Name = "André", Surname = "Nascimento", Email = "andre.nascimento@gmail.com", Password = "senha54321", AcessLevel = "Admin", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new User { Id = 20, Name = "Tatiane", Surname = "Carvalho", Email = "tatiane.carvalho@yahoo.com", Password = "1234senha", AcessLevel = "Comum", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new User { Id = 21, Name = "Marcela", Surname = "Costa", Email = "marcela.costa@gmail.com", Password = "senha789", AcessLevel = "Comum", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new User { Id = 22, Name = "José", Surname = "Lopes", Email = "jose.lopes@outlook.com", Password = "senha432", AcessLevel = "Admin", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new User { Id = 23, Name = "Carla", Surname = "Melo", Email = "carla.melo@hotmail.com", Password = "senha654", AcessLevel = "Comum", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
            );

            base.OnModelCreating(builder);
        }

    }
}
