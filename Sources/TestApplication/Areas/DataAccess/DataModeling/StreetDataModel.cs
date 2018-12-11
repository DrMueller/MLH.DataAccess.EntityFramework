using System.Collections.Generic;
using Mmu.Mlh.DataAccess.Areas.DataModeling.Models;
using Mmu.Mlh.DataAccess.EntityFramework.Areas;

namespace Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Areas.DataAccess.DataModeling
{
    public class StreetDataModel : ValueObjectDataModelBase<StreetDataModel>
    {
        public AddressDataModel Address { get; set; }
        public long AddressId { get; set; }
        public string StreetName { get; set; }
        public int StreetNumber { get; set; }

        protected override IReadOnlyCollection<string> PropertyNamesToCompare
        => new List<string>
        {
            nameof(AddressId),
            nameof(StreetName),
            nameof(StreetNumber)
        };
    }
}