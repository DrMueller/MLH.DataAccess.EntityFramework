using Microsoft.EntityFrameworkCore;
using Mmu.Mlh.DataAccess.Areas.DataModeling.Services;
using Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Areas.DataAccess.DbContexts;
using Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Infrastructure.Settings.Services;
using StructureMap;

namespace Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Infrastructure.DependencyInjection
{
    public class TestApplicationRegistry : Registry
    {
        public TestApplicationRegistry()
        {
            Scan(
                scanner =>
                {
                    scanner.AssemblyContainingType(typeof(TestApplicationRegistry));
                    scanner.WithDefaultConventions();

                    scanner.AddAllTypesOf(typeof(IDataModelAdapter<,,>));
                });

            For<IEntityFrameworkDbSettingsProvider>().Singleton();
            For<DbContext>().Use<TestDbContext>().Transient();
        }
    }
}