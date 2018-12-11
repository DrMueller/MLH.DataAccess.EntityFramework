using System;
using System.Collections.Generic;
using Mmu.Mlh.DataAccess.Areas.DataModeling.Models;

namespace Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Areas.DataAccess.DataModeling
{
    public class IndividualDataModel : DataModelBase<long>
    {
        public List<AddressDataModel> Addresses { get; set; }
        public DateTime Birthdate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}