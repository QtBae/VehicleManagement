using Shared.ApiModels;
using AutoMapper;
namespace VehicleManagement.Profiles
{
    public class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<Entities.BrandEntity, BrandModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.BrandName));

            CreateMap<BrandModel, Entities.BrandEntity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.BrandName));
        }
    }
}
