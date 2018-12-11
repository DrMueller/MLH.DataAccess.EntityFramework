using Mmu.Mlh.ConsoleExtensions.Areas.Commands.Models;
using StructureMap;

namespace Mmu.Mlh.DataAccess.EntityFramework.TestConsole.Infrastructure.DependencyInjection
{
    public class TestConsoleRegistry : Registry
    {
        public TestConsoleRegistry()
        {
            Scan(
                scanner =>
                {
                    scanner.AssemblyContainingType(typeof(TestConsoleRegistry));
                    scanner.WithDefaultConventions();

                    scanner.AddAllTypesOf<IConsoleCommand>();
                });
        }
    }
}