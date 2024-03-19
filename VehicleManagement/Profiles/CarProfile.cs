using AutoMapper;
using Shared.ApiModels;
namespace VehicleManagement.Profiles
{
    public class CarProfile : Profile
    {
        public CarProfile()
        {
            CreateMap<Entities.CarEntity, CarModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ModelName, opt => opt.MapFrom(src => src.ModelName))
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand))
                .ForMember(dest => dest.MaintenanceFrequency, opt => opt.MapFrom(src => src.MaintenanceFrequency));


            CreateMap<CarModel, Entities.CarEntity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ModelName, opt => opt.MapFrom(src => src.ModelName))
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand))
                .ForMember(dest => dest.MaintenanceFrequency, opt => opt.MapFrom(src => src.MaintenanceFrequency));
        }
    }
}
