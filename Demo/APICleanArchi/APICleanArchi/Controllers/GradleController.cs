using APICleanArchi.Services;
using ApiCleanArchiDTO;
using Microsoft.AspNetCore.Mvc;

namespace APICleanArchi.Controllers
{
    [ApiController]
    [Route("api/gradles")]
    public class GradleController: ControllerBase
    {
        private readonly IGradleService _gradleService;

        public GradleController(IGradleService gradleService)
        {
            _gradleService = gradleService;
        }

        [HttpGet]
        public async Task<IEnumerable<GradleDTO>> GetAllGradlesAsync()
        {
            return await _gradleService.GetAllGradlesAsync();
        }

        [HttpPost]
        public async Task CreateGradleAsync(GradleDTO gradle)
        {
            await _gradleService.CreateGradleAsync(gradle);
        }


    }
}
