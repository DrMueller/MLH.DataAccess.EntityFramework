using System;
using System.Collections.Generic;
using System.Linq;
using Mmu.Mlh.DomainExtensions.Areas.DomainModeling;
using Mmu.Mlh.LanguageExtensions.Areas.DeepCopying;
using Mmu.Mlh.LanguageExtensions.Areas.Invariance;
using Mmu.Mlh.LanguageExtensions.Areas.Types.Maybes;

namespace Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Areas.Domain.Models
{
    public class Individual : AggregateRoot<long>
    {
        private List<Address> _addresses;
        private string _firstName;

        public IReadOnlyCollection<Address> Addresses
        {
            get
            {
                return _addresses.Select(f => f.DeepCopy()).ToList();
            }
        }

        public DateTime Birthdate { get; }

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                Guard.ObjectNotNull(() => FirstName);
            }
        }

        public string LastName { get; }

        public Individual(
            string firstName,
            string lastName,
            DateTime birthdate,
            List<Address> addresses,
            long id)
            : base(id)
        {
            Guard.ObjectNotNull(() => lastName);
            Guard.ObjectNotNull(() => addresses);

            FirstName = firstName;
            LastName = lastName;
            Birthdate = birthdate;
            _addresses = addresses;
        }

        public void AddAddress(Address address)
        {
            _addresses.Add(address);
        }

        public void RemoveAddress(Address address)
        {
            _addresses.Remove(address);
        }

        public Maybe<Address> SearchAddress(Func<Address, bool> match)
        {
            var address = _addresses.FirstOrDefault(match)?.DeepCopy();
            return Maybe.CreateFromNullable(address);
        }

        public void UpdateAddress(Address address)
        {
            var existingAddress = _addresses.Single(existingAdr => existingAdr == address);
            existingAddress.City = address.City;
            existingAddress.Zip = address.Zip;

            var removedStreets = existingAddress.Streets.Except(address.Streets).ToList();
            removedStreets.ForEach(removedStreet => existingAddress.RemoveStreet(removedStreet));

            var addedStreets = address.Streets.Except(existingAddress.Streets).ToList();
            addedStreets.ForEach(addedStreet => existingAddress.AddStreet(addedStreet));
        }
    }
}