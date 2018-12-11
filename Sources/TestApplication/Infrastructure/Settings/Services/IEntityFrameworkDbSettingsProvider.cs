using Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Infrastructure.Settings.Models;

namespace Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Infrastructure.Settings.Services
{
    public interface IEntityFrameworkDbSettingsProvider
    {
        EntityFrameworkDbSettings ProvideEntityFrameworkDbSettings();
    }
}