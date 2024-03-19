using AutoMapper;
using Shared.ApiModels;
namespace VehicleManagement.Profiles
{
    public class MaintainanceProfile : Profile
    {
        public MaintainanceProfile()
        {
            CreateMap<Entities.MaintainanceEntity, MaintainanceModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Works, opt => opt.MapFrom(src => src.Works))
                .ForMember(dest => dest.Mileage, opt => opt.MapFrom(src => src.Mileage))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Vehicle, opt => opt.MapFrom(src => src.Vehicle));


            CreateMap<MaintainanceModel, Entities.MaintainanceEntity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Works, opt => opt.MapFrom(src => src.Works))
                .ForMember(dest => dest.Mileage, opt => opt.MapFrom(src => src.Mileage))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Vehicle, opt => opt.MapFrom(src => src.Vehicle));
        }
    }
}
