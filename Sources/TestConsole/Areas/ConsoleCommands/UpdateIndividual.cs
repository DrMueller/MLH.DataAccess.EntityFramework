using System;
using System.Threading.Tasks;
using Mmu.Mlh.ConsoleExtensions.Areas.Commands.Models;
using Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Areas.DataAccess.Repositories;
using Newtonsoft.Json;

namespace Mmu.Mlh.DataAccess.EntityFramework.TestConsole.Areas.ConsoleCommands
{
    public class UpdateIndividual : IConsoleCommand
    {
        private readonly IIndividualRepository _individualRepository;
        public string Description { get; } = "Individual - Update";
        public ConsoleKey Key { get; } = ConsoleKey.D4;

        public UpdateIndividual(IIndividualRepository individualRepository)
        {
            _individualRepository = individualRepository;
        }

        public async Task ExecuteAsync()
        {
            var individual = await _individualRepository.LoadByIdAsync(2);
            individual.FirstName = "Matthias " + DateTime.Now.Ticks;
            var returnedIndividual = await _individualRepository.SaveAsync(individual);
            Console.WriteLine(JsonConvert.SerializeObject(returnedIndividual));
        }
    }
}