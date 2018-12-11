using System;
using System.Threading.Tasks;
using Mmu.Mlh.ConsoleExtensions.Areas.Commands.Models;
using Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Areas.Domain.Models;
using Mmu.Mlh.DomainExtensions.Areas.Repositories;
using Newtonsoft.Json;

namespace Mmu.Mlh.DataAccess.EntityFramework.TestConsole.Areas.ConsoleCommands
{
    public class LoadAllIndividuals : IConsoleCommand
    {
        private readonly IRepository<Individual, long> _individualRepository;
        public string Description { get; } = "Individuals - Load all";
        public ConsoleKey Key { get; } = ConsoleKey.D2;

        public LoadAllIndividuals(IRepository<Individual, long> individualRepository)
        {
            _individualRepository = individualRepository;
        }

        public async Task ExecuteAsync()
        {
            var allIndividuals = await _individualRepository.LoadAllAsync();
            Console.WriteLine(JsonConvert.SerializeObject(allIndividuals));
        }
    }
}