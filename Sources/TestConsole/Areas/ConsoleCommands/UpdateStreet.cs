using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mmu.Mlh.ConsoleExtensions.Areas.Commands.Models;
using Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Areas.DataAccess.Repositories;
using Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Areas.Domain.Factories;
using Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Areas.Domain.Models;

namespace Mmu.Mlh.DataAccess.EntityFramework.TestConsole.Areas.ConsoleCommands
{
    public class UpdateStreet : IConsoleCommand
    {
        private readonly IIndividualFactory _individualFactory;
        private readonly IIndividualRepository _individualRepository;
        public string Description { get; } = "Steet - Update";
        public ConsoleKey Key { get; } = ConsoleKey.D9;

        public UpdateStreet(IIndividualRepository individualRepository, IIndividualFactory individualFactory)
        {
            _individualRepository = individualRepository;
            _individualFactory = individualFactory;
        }

        public async Task ExecuteAsync()
        {
            var addresses = new List<Address>
            {
                new Address(
                    "Sursee",
                    6210,
                    new List<Street>
                    {
                        new Street("Grubenmatte", 5),
                        new Street("Wassergraben", 6)
                    },
                    0)
            };

            var newIndividual = _individualFactory.Create("Matthias", "Müller", new DateTime(1986, 12, 29), addresses);
            var individual = await _individualRepository.SaveAsync(newIndividual);

            var addressFromAlterswil = individual.SearchAddress(f => f.City == "Sursee");
            addressFromAlterswil.Evaluate(
                async adr =>
                {
                    var streetGrubenmatte = adr.Streets.Single(f => f.StreetName == "Grubenmatte");
                    adr.RemoveStreet(streetGrubenmatte);
                    adr.AddStreet(new Street("Grubenmatte Again", 123));

                    var streetWasserGraben = adr.Streets.Single(f => f.StreetName == "Wassergraben");
                    adr.RemoveStreet(streetWasserGraben);

                    adr.AddStreet(new Street("Completely new One", 1234));

                    individual.UpdateAddress(adr);
                    await _individualRepository.SaveAsync(individual);
                });
        }
    }
}