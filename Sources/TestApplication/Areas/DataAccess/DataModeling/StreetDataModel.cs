﻿using System.Collections.Generic;
using Mmu.Mlh.DataAccess.Areas.DataModeling.Models;

namespace Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Areas.DataAccess.DataModeling
{
    public class StreetDataModel : ValueObjectDataModel<StreetDataModel>
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