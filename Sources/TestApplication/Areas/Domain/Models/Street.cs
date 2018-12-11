using Mmu.Mlh.DomainExtensions.Areas.DomainModeling;

namespace Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Areas.Domain.Models
{
    public class Street : ValueObject<Street>
    {
        public int StreetNumber { get; }
        public string StreetName { get; }

        public Street(string streetName, int streetNumber)
        {
            StreetName = streetName;
            StreetNumber = streetNumber;
        }
    }
}