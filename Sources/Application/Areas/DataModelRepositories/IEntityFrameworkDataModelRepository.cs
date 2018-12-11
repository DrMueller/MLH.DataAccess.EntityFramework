using Mmu.Mlh.DataAccess.Areas.DatabaseAccess;
using Mmu.Mlh.DataAccess.Areas.DataModeling.Models;

namespace Mmu.Mlh.DataAccess.EntityFramework.Areas.DataModelRepositories
{
    public interface IEntityFrameworkDataModelRepository<T, TId> : IDataModelRepository<T, TId>
        where T : DataModelBase<TId>
    {
    }
}