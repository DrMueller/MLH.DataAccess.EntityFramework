using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mmu.Mlh.ConsoleExtensions.Areas.Commands.Models;
using Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Areas.DataAccess.Repositories;
using Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Areas.Domain.Factories;
using Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Areas.Domain.Models;

namespace Mmu.Mlh.DataAccess.EntityFramework.TestConsole.Areas.ConsoleCommands
{
    public class RemoveIndividual : IConsoleCommand
    {
        private readonly IIndividualFactory _individualFactory;
        private readonly IIndividualRepository _individualRepository;
        public string Description { get; } = "Individual - Remove";
        public ConsoleKey Key { get; } = ConsoleKey.D8;

        public RemoveIndividual(IIndividualRepository individualRepository, IIndividualFactory individualFactory)
        {
            _individualRepository = individualRepository;
            _individualFactory = individualFactory;
        }

        public async Task ExecuteAsync()
        {
            var newAddress = new Address(
                "Faketown",
                1234,
                new List<Street>
                {
                    new Street("Fakestreet " + DateTime.Now.Ticks, 4321),
                    new Street("AnotherFakestreet", 42)
                },
                0);

            var newIndividual = _individualFactory.Create("Matthias", "Müller", new DateTime(1986, 12, 29), new List<Address> { newAddress });
            var individual = await _individualRepository.SaveAsync(newIndividual);

            await _individualRepository.DeleteAsync(individual.Id);
        }
    }
}