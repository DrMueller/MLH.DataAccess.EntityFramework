using System.Linq;
using Microsoft.EntityFrameworkCore;
using Mmu.Mlh.DataAccess.EntityFramework.Areas.DataModelRepositories.Implementation;
using Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Areas.DataAccess.DataModeling;

namespace Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Areas.DataAccess.Repositories.DataModelRepositories.Implementation
{
    public class IndividualDataModelRepository : EntityFrameworkDataModelRepository<IndividualDataModel, long>, IIndividualDataModelRepository
    {
        public IndividualDataModelRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override IQueryable<IndividualDataModel> AppendIncludes(IQueryable<IndividualDataModel> query)
        {
            return query.Include(f => f.Addresses)
                .ThenInclude(f => f.Streets);
        }
    }
}