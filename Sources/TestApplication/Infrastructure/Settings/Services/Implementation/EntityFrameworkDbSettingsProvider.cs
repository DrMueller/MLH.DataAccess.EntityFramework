using Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Infrastructure.Settings.Models;

namespace Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Infrastructure.Settings.Services.Implementation
{
    public class EntityFrameworkDbSettingsProvider : IEntityFrameworkDbSettingsProvider
    {
        public EntityFrameworkDbSettings ProvideEntityFrameworkDbSettings()
        {
            return new EntityFrameworkDbSettings
            {
                ConnectionString = "Server=tcp:mmu-server.database.windows.net,1433;Database=mmu-db;User ID=mmu;Password=Joker1joker1;Encrypt=true;Connection Timeout=30;"
            };
        }
    }
}