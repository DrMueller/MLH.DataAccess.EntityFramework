using Mmu.Mlh.ConsoleExtensions.Areas.Commands.Services;
using Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Infrastructure.Initialization;
using Mmu.Mlh.ServiceProvisioning.Areas.Provisioning.Services;

namespace Mmu.Mlh.DataAccess.EntityFramework.TestConsole
{
    public static class Program
    {
        public static void Main()
        {
            AppInitializationService.AssureAppIsInitialized(typeof(Program).Assembly);

            ServiceLocatorSingleton
                .Instance
                .GetService<IConsoleCommandsStartupService>()
                .Start();
        }
    }
}