using System;
using System.Collections.Generic;
using Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Areas.Domain.Factories;
using Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Areas.Domain.Models;

namespace Mmu.Mlh.DataAccess.EntityFramework.IntegrationTests.TestingInfrastructure.DataBuilders
{
    public class TestIndividualBuilder
    {
        private readonly IIndividualFactory _individualFactory;

        public TestIndividualBuilder(IIndividualFactory individualFactory)
        {
            _individualFactory = individualFactory;
        }

        public Individual BuildFullIndividual()
        {
            var streetsAlterswil = new List<Street>
            {
                new Street("Bonnetsacher", 11),
                new Street("Hauptstrasse", 3)
            };

            var streetsSursee = new List<Street>
            {
                new Street("Grubenmatte", 5),
                new Street("Wassergraben", 6)
            };

            var addresses = new List<Address>
            {
                new Address("Alterswil", 1715, streetsAlterswil, 0),
                new Address("Sursee", 6210, streetsSursee, 0)
            };

            var individual = _individualFactory.Create("Matthias", "Müller", new DateTime(1986, 12, 29), addresses);
            return individual;
        }
    }
}