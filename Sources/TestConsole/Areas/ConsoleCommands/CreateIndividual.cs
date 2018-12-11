using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mmu.Mlh.ConsoleExtensions.Areas.Commands.Models;
using Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Areas.DataAccess.Repositories;
using Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Areas.Domain.Factories;
using Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Areas.Domain.Models;
using Newtonsoft.Json;

namespace Mmu.Mlh.DataAccess.EntityFramework.TestConsole.Areas.ConsoleCommands
{
    public class CreateIndividual : IConsoleCommand
    {
        private readonly IIndividualFactory _individualFactory;
        private readonly IIndividualRepository _individualRepository;
        public string Description { get; } = "Individual - Create";
        public ConsoleKey Key { get; } = ConsoleKey.D1;

        public CreateIndividual(
            IIndividualRepository individualRepository,
            IIndividualFactory individualFactory)
        {
            _individualRepository = individualRepository;
            _individualFactory = individualFactory;
        }

        public async Task ExecuteAsync()
        {
            var addresses = new List<Address>
            {
                new Address("Alterswil", 1715, new List<Street> { new Street("Bonnetsacher", 11) }, 0),
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
            Console.WriteLine(JsonConvert.SerializeObject(individual));
        }
    }
}