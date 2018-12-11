using Mmu.Mlh.DataAccess.Areas.DatabaseAccess;
using Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Areas.DataAccess.DataModeling;

namespace Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Areas.DataAccess.Repositories.DataModelRepositories
{
    public interface IIndividualDataModelRepository : IDataModelRepository<IndividualDataModel, long>
    {
    }
}