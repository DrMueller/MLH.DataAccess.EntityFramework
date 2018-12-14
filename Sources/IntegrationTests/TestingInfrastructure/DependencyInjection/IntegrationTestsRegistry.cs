using Microsoft.EntityFrameworkCore;
using Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Areas.DataAccess.DbContexts;
using StructureMap;

namespace Mmu.Mlh.DataAccess.EntityFramework.IntegrationTests.TestingInfrastructure.DependencyInjection
{
    public class IntegrationTestsRegistry : Registry
    {
        public IntegrationTestsRegistry()
        {
            Scan(
                scanner =>
                {
                    scanner.AssemblyContainingType<IntegrationTestsRegistry>();
                    scanner.WithDefaultConventions();
                });

            var options = new DbContextOptionsBuilder<TestDbContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;

            var inMemoryDbContext = new TestDbContext(options);
            inMemoryDbContext.Database.EnsureDeleted();
            inMemoryDbContext.Database.EnsureCreated();
            For<DbContext>().Use(inMemoryDbContext);
        }
    }
}