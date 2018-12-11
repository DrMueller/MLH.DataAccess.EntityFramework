using System.Collections.Generic;
using System.Linq;
using Mmu.Mlh.DomainExtensions.Areas.DomainModeling;
using Mmu.Mlh.LanguageExtensions.Areas.DeepCopying;

namespace Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Areas.Domain.Models
{
    public class Address : Entity<long>
    {
        private readonly List<Street> _streets;
        public string City { get; set; }
        public IReadOnlyCollection<Street> Streets => _streets.Select(f => f.DeepCopy()).ToList();
        public int Zip { get; set; }

        public Address(string city, int zip, List<Street> streets, long id)
            : base(id)
        {
            _streets = streets;
            City = city;
            Zip = zip;
        }

        public void AddStreet(Street street)
        {
            _streets.Add(street);
        }

        public void RemoveStreet(Street street)
        {
            _streets.Remove(street);
        }
    }
}