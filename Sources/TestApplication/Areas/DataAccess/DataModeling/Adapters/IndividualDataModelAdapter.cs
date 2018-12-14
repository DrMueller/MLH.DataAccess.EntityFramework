using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Mmu.Mlh.DataAccess.Areas.DataModeling.Services.Implementation;
using Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Areas.Domain.Factories;
using Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Areas.Domain.Models;

namespace Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Areas.DataAccess.DataModeling.Adapters
{
    public class IndividualDataModelAdapter : DataModelAdapterBase<IndividualDataModel, Individual, long>
    {
        private readonly IIndividualFactory _individualFactory;

        public IndividualDataModelAdapter(IIndividualFactory individualFactory, IMapper mapper) : base(mapper)
        {
            _individualFactory = individualFactory;
        }

        public override Individual Adapt(IndividualDataModel dataModel)
        {
            var addresses = dataModel.Addresses?.Select(AdaptAddress).ToList() ?? new List<Address>();

            return _individualFactory.Create(
                dataModel.FirstName,
                dataModel.LastName,
                dataModel.Birthdate,
                addresses,
                dataModel.Id);
        }

        public override IndividualDataModel Adapt(Individual aggregateRoot)
        {
            var dataModel = base.Adapt(aggregateRoot);

            if (!aggregateRoot.Addresses.Any())
            {
                return dataModel;
            }

            dataModel.Addresses = aggregateRoot.Addresses.Select(
                adr => new AddressDataModel
                {
                    City = adr.City,
                    Id = adr.Id,
                    IndividualId = aggregateRoot.Id,
                    Streets = adr.Streets.Select(
                        str => new StreetDataModel
                        {
                            StreetName = str.StreetName,
                            StreetNumber = str.StreetNumber,
                            AddressId = adr.Id
                        }).ToList(),
                    Zip = adr.Zip
                }).ToList();

            return dataModel;
        }

        private static Address AdaptAddress(AddressDataModel dataModel)
        {
            var streets = dataModel.Streets?.Select(f => new Street(f.StreetName, f.StreetNumber)).ToList() ?? new List<Street>();
            return new Address(dataModel.City, dataModel.Zip, streets, dataModel.Id);
        }
    }
}