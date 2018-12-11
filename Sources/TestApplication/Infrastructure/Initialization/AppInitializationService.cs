using System.Reflection;
using Mmu.Mlh.ServiceProvisioning.Areas.Initialization.Models;
using Mmu.Mlh.ServiceProvisioning.Areas.Initialization.Services;

namespace Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Infrastructure.Initialization
{
    public static class AppInitializationService
    {
        private static bool _isAppInitialized;

        public static void AssureAppIsInitialized(Assembly rootAssembly = null)
        {
            if (!_isAppInitialized)
            {
                rootAssembly = rootAssembly ?? typeof(AppInitializationService).Assembly;

                ContainerInitializationService.CreateInitializedContainer(
                    new ContainerConfiguration(rootAssembly, "Mmu.Mlh.DataAccess", true));
            }

            _isAppInitialized = true;
        }
    }
}