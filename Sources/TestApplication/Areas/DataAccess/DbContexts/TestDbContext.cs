using Microsoft.EntityFrameworkCore;
using Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Infrastructure.Initialization;
using Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Infrastructure.Settings.Services;
using Mmu.Mlh.ServiceProvisioning.Areas.Provisioning.Services;

namespace Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Areas.DataAccess.DbContexts
{
    // DbContext is created for Migrations, therefore it must not have a Constructor with parameters
    public class TestDbContext : DbContext
    {
        public TestDbContext()
        {
            AppInitializationService.AssureAppIsInitialized();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var settingsProvider = ServiceLocatorSingleton.Instance.GetService<IEntityFrameworkDbSettingsProvider>();
            var connectionStrinng = settingsProvider.ProvideEntityFrameworkDbSettings().ConnectionString;
            optionsBuilder.UseSqlServer(connectionStrinng);
            optionsBuilder.ConfigureWarnings(warnings => warnings.Throw());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TestDbContext).Assembly);
        }
    }
}