using System;
using System.Threading.Tasks;
using Mmu.Mlh.ConsoleExtensions.Areas.Commands.Models;
using Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Areas.DataAccess.Repositories;
using Newtonsoft.Json;

namespace Mmu.Mlh.DataAccess.EntityFramework.TestConsole.Areas.ConsoleCommands
{
    public class LoadIndividualById : IConsoleCommand
    {
        private readonly IIndividualRepository _individualRepository;
        public string Description { get; } = "Individual - Load by ID";
        public ConsoleKey Key { get; } = ConsoleKey.D3;

        public LoadIndividualById(IIndividualRepository individualRepository)
        {
            _individualRepository = individualRepository;
        }

        public async Task ExecuteAsync()
        {
            var individual = await _individualRepository.LoadByIdAsync(2);
            Console.WriteLine(JsonConvert.SerializeObject(individual));
        }
    }
}