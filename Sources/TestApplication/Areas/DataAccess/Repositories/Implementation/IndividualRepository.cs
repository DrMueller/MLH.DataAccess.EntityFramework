using Mmu.Mlh.DataAccess.Areas.DataModeling.Services;
using Mmu.Mlh.DataAccess.Areas.Repositories;
using Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Areas.DataAccess.DataModeling;
using Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Areas.DataAccess.Repositories.DataModelRepositories;
using Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Areas.Domain.Models;

namespace Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Areas.DataAccess.Repositories.Implementation
{
    public class IndividualRepository : RepositoryBase<Individual, IndividualDataModel, long>, IIndividualRepository
    {
        public IndividualRepository(
            IIndividualDataModelRepository dataModelRepository,
            IDataModelAdapter<IndividualDataModel, Individual, long> dataModelAdapter)
            : base(dataModelRepository, dataModelAdapter)
        {
        }

    }
}