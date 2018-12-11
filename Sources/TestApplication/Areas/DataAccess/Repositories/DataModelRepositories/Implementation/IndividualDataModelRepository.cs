using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Mmu.Mlh.DataAccess.EntityFramework.Areas.DataModelRepositories.Implementation;
using Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Areas.DataAccess.DataModeling;

namespace Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Areas.DataAccess.Repositories.DataModelRepositories.Implementation
{
    public class IndividualDataModelRepository : EntityFrameworkDataModelRepository<IndividualDataModel, long>, IIndividualDataModelRepository
    {
        private readonly DbContext _dbContext;

        public IndividualDataModelRepository(DbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        protected override IQueryable<IndividualDataModel> AppendIncludes(IQueryable<IndividualDataModel> query)
        {
            return query.Include(f => f.Addresses)
                .ThenInclude(f => f.Streets);
        }
    }
}