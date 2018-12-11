using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mmu.Mlh.ConsoleExtensions.Areas.Commands.Models;
using Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Areas.DataAccess.Repositories;
using Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Areas.Domain.Models;
using Newtonsoft.Json;

namespace Mmu.Mlh.DataAccess.EntityFramework.TestConsole.Areas.ConsoleCommands
{
    public class RemoveAddress : IConsoleCommand
    {
        private readonly IIndividualRepository _individualRepository;
        public string Description { get; } = "Address - Remove";
        public ConsoleKey Key { get; } = ConsoleKey.D7;

        public RemoveAddress(IIndividualRepository individualRepository)
        {
            _individualRepository = individualRepository;
        }

        public async Task ExecuteAsync()
        {
            var individual = await _individualRepository.LoadByIdAsync(6);
            var newAddress = new Address(
                "Faketown",
                1234,
                new List<Street>
                {
                    new Street("Fakestreet " + DateTime.Now.Ticks, 4321),
                    new Street("AnotherFakestreet", 42)
                },
                0);
            individual.AddAddress(newAddress);

            await _individualRepository.SaveAsync(individual);

            var addressFromSursee = individual.SearchAddress(f => f.City == "Faketown");
            addressFromSursee.Evaluate(adr => individual.RemoveAddress(adr));

            var returnedIndividual = await _individualRepository.SaveAsync(individual);
            Console.WriteLine(JsonConvert.SerializeObject(returnedIndividual));
        }
    }
}