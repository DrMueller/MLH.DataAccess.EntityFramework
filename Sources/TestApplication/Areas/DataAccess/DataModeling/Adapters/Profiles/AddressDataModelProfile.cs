using AutoMapper;
using Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Areas.Domain.Models;

namespace Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Areas.DataAccess.DataModeling.Adapters.Profiles
{
    public class AddressDataModelProfile : Profile
    {
        public AddressDataModelProfile()
        {
            CreateMap<Address, AddressDataModel>()
                .ForMember(d => d.City, c => c.MapFrom(f => f.City))
                .ForMember(d => d.Id, c => c.MapFrom(f => f.Id))
                .ForMember(d => d.Streets, c => c.Ignore())
                .ForMember(d => d.Zip, c => c.MapFrom(f => f.Zip));
        }
    }
}