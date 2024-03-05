using ApiCleanArchiDTO;
using AutoMapper;

namespace APICleanArchi.Profiles
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<Entities.UserEntity, UserDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<UserDTO, Entities.UserEntity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        }
    }
}
