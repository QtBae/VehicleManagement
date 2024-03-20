using AutoMapper;

namespace APICleanArchi.Profiles
{
    public class GradleProfile: Profile
    {
        public GradleProfile()
        {
            CreateMap<Entities.GradleEntity, ApiCleanArchiDTO.GradleDTO>().ReverseMap();
        }
    }
}
