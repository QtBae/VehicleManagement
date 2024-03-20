using AutoMapper;

namespace APICleanArchi.Profiles
{
    public class GraduationProfile: Profile
    {
        public GraduationProfile()
        {
            CreateMap<Entities.GraduationEntity, ApiCleanArchiDTO.GraduationDTO>().ReverseMap();
        }
    }
}
