using Desafio.Domain.Entity;
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
            base.OnModelCreating(builder);
        }

    }
}
