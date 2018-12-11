using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mmu.Mlh.ConsoleExtensions.Areas.Commands.Models;
using Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Areas.DataAccess.Repositories;
using Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Areas.Domain.Models;
using Newtonsoft.Json;

namespace Mmu.Mlh.DataAccess.EntityFramework.TestConsole.Areas.ConsoleCommands
{
    public class AddAddress : IConsoleCommand
    {
        private readonly IIndividualRepository _individualRepository;
        public string Description { get; } = "Address - Add";

        public ConsoleKey Key { get; }
            = ConsoleKey.D5;

        public AddAddress(IIndividualRepository individualRepository)
        {
            _individualRepository = individualRepository;
        }

        public async Task ExecuteAsync()
        {
            var individual = await _individualRepository.LoadByIdAsync(3);
            var newAddress = new Address(
                "Sursee",
                6210,
                new List<Street>
                {
                    new Street("Grubenmatte", 5),
                    new Street("Wassergraben", 6)
                },
                0);

            individual.AddAddress(newAddress);
            var returnedIndividual = await _individualRepository.SaveAsync(individual);
            Console.WriteLine(JsonConvert.SerializeObject(returnedIndividual));
        }
    }
}