using Mmu.Mlh.DataAccess.Areas.DatabaseAccess;
using Mmu.Mlh.DataAccess.EntityFramework.Areas.DataModelRepositories;
using StructureMap;

namespace Mmu.Mlh.DataAccess.EntityFramework.Infrastructure.DependencyInjection
{
    public class EntityFrameworkRegistry : Registry
    {
        public EntityFrameworkRegistry()
        {
            Scan(
                scanner =>
                {
                    scanner.AssemblyContainingType(typeof(EntityFrameworkRegistry));
                    scanner.WithDefaultConventions();

                    scanner.AddAllTypesOf(typeof(IEntityFrameworkDataModelRepository<,>));
                });
        }
    }
}