using System;
using System.Collections.Generic;
using Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Areas.Domain.Models;

namespace Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Areas.Domain.Factories.Implementation
{
    public class IndividualFactory : IIndividualFactory
    {
        public Individual Create(
            string firstName,
            string lastName,
            DateTime birthdate,
            List<Address> addresses,
            long? id = null)
        {
            return new Individual(
                firstName,
                lastName,
                birthdate,
                addresses,
                id ?? 0);
        }
    }
}