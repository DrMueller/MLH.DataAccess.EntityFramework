using System.Collections.Generic;
using Mmu.Mlh.DataAccess.Areas.DataModeling.Models;

namespace Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Areas.DataAccess.DataModeling
{
    public class AddressDataModel : EntityDataModel<long>
    {
        public string City { get; set; }
        public IndividualDataModel Individual { get; set; }
        public long IndividualId { get; set; }
        public List<StreetDataModel> Streets { get; set; }
        public int Zip { get; set; }
    }
}