using System;
using System.Collections.Generic;
using Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Areas.Domain.Models;

namespace Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Areas.Domain.Factories
{
    public interface IIndividualFactory
    {
        Individual Create(
            string firstName,
            string lastName,
            DateTime birthdate,
            List<Address> addresses,
            long? id = null);
    }
}