using APICleanArchi.Entities;
using APICleanArchi.Repositories;
using ApiCleanArchiDTO;
using AutoMapper;

namespace APICleanArchi.Services
{
    public class GraduationServices: IGraduationServices
    {
        private readonly IGraduationRepository graduationRepository;
        private readonly IMapper mapper;

        public GraduationServices(IGraduationRepository graduationRepository, IMapper mapper)
        {
            this.graduationRepository = graduationRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<GraduationDTO>> GetAllGraduationsAsync()
        {
            var graduations = await graduationRepository.GetAllGraduationsAsync();
            return mapper.Map<IEnumerable<GraduationDTO>>(graduations);
        }

        public async Task CreateGraduationAsync(GraduationDTO graduation)
        {
            var graduationEntity = mapper.Map<GraduationEntity>(graduation);
            await graduationRepository.CreateGraduationAsync(graduationEntity);
        }
    }

    public interface IGraduationServices
    {
        Task<IEnumerable<GraduationDTO>> GetAllGraduationsAsync();
        Task CreateGraduationAsync(GraduationDTO graduation);
    }
}
