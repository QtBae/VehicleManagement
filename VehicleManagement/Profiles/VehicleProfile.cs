using AutoMapper;
using Shared.ApiModels;
namespace VehicleManagement.Profiles
{
    public class VehicleProfile : Profile
    {
        public VehicleProfile()
        {
            CreateMap<Entities.VehicleEntity, VehicleModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model))
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand))
                .ForMember(dest => dest.LicensePlate, opt => opt.MapFrom(src => src.LicensePlate))
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year))
                .ForMember(dest => dest.Mileage, opt => opt.MapFrom(src => src.Mileage))
                .ForMember(dest => dest.Energy, opt => opt.MapFrom(src => src.Energy))
                .ForMember(dest => dest.ModelId, opt => opt.MapFrom(src => src.ModelId))
                .ForMember(dest => dest.BrandId, opt => opt.MapFrom(src => src.BrandId))
                .ForMember(dest => dest.Maintainances, opt => opt.MapFrom(src => src.Maintainances))
                .ForMember(dest => dest.Lateness, opt => opt.MapFrom(src => src.Lateness))
                ;


            CreateMap<VehicleModel, Entities.VehicleEntity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model))
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand))
                .ForMember(dest => dest.LicensePlate, opt => opt.MapFrom(src => src.LicensePlate))
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year))
                .ForMember(dest => dest.Mileage, opt => opt.MapFrom(src => src.Mileage))
                .ForMember(dest => dest.Energy, opt => opt.MapFrom(src => src.Energy))
                .ForMember(dest => dest.ModelId, opt => opt.MapFrom(src => src.ModelId))
                .ForMember(dest => dest.BrandId, opt => opt.MapFrom(src => src.BrandId))
                .ForMember(dest => dest.Maintainances, opt => opt.MapFrom(src => src.Maintainances))
                .ForMember(dest => dest.Lateness, opt => opt.MapFrom(src => src.Lateness))
                ;
        }
    }

}
