using APICleanArchi.Entities;
using APICleanArchi.Repositories;
using ApiCleanArchiDTO;
using AutoMapper;

namespace APICleanArchi.Services
{
    public class GradleService: IGradleService
    {
        private readonly IGradleRepository _gradleRepository;
        private readonly IMapper _mapper;

        public GradleService(IGradleRepository gradleRepository, IMapper mapper)
        {
            _gradleRepository = gradleRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GradleDTO>> GetAllGradlesAsync()
        {
            var gradles = await _gradleRepository.GetAllGradlesAsync();
            return _mapper.Map<IEnumerable<GradleDTO>>(gradles);
        }

        public async Task CreateGradleAsync(GradleDTO gradle)
        {
            var gradleEntity = _mapper.Map<GradleEntity>(gradle);
            await _gradleRepository.CreateGradleAsync(gradleEntity);
        }
    }

    public interface IGradleService
    {
        Task<IEnumerable<GradleDTO>> GetAllGradlesAsync();
        Task CreateGradleAsync(GradleDTO gradle);
    }
}
