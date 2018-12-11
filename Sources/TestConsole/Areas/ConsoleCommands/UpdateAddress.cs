using System;
using System.Threading.Tasks;
using Mmu.Mlh.ConsoleExtensions.Areas.Commands.Models;
using Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Areas.DataAccess.Repositories;

namespace Mmu.Mlh.DataAccess.EntityFramework.TestConsole.Areas.ConsoleCommands
{
    public class UpdateAddress : IConsoleCommand
    {
        private readonly IIndividualRepository _individualRepository;
        public string Description { get; } = "Address - Update";
        public ConsoleKey Key { get; } = ConsoleKey.D6;

        public UpdateAddress(IIndividualRepository individualRepository)
        {
            _individualRepository = individualRepository;
        }

        public async Task ExecuteAsync()
        {
            var individual = await _individualRepository.LoadByIdAsync(6);
            var addressFromAlterswil = individual.SearchAddress(f => f.City == "Alterswil");

            addressFromAlterswil.Evaluate(
                adr =>
                {
                    adr.City = "Alterswil 3";
                    individual.UpdateAddress(addressFromAlterswil);
                });

            await _individualRepository.SaveAsync(individual);

            var individualAgain = await _individualRepository.LoadByIdAsync(6);
            var address2 = individual.SearchAddress(adr => adr.City == "Alterswil 3");
            address2.Evaluate(
                adr =>
                {
                    adr.City = "Alterswil";
                    individualAgain.UpdateAddress(address2);
                });

            await _individualRepository.SaveAsync(individualAgain);
        }
    }
}