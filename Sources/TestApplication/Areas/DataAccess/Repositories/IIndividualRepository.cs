using Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Areas.Domain.Models;
using Mmu.Mlh.DomainExtensions.Areas.Repositories;

namespace Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Areas.DataAccess.Repositories
{
    public interface IIndividualRepository : IRepository<Individual, long>
    {
    }
}