using APICleanArchi.Services;
using ApiCleanArchiDTO;
using Microsoft.AspNetCore.Mvc;

namespace APICleanArchi.Controllers
{
    [ApiController]
    [Route("api/graduations")]
    public class GraduationsController: ControllerBase
    {
        private readonly IGraduationServices graduationServices;

        public GraduationsController(IGraduationServices graduationServices)
        {
            this.graduationServices = graduationServices;
        }

        [HttpGet]
        public async Task<IEnumerable<GraduationDTO>> GetAllGraduationsAsync()
        {
            return await graduationServices.GetAllGraduationsAsync();
        }

        [HttpPost]
        public async Task CreateGraduationAsync(GraduationDTO graduation)
        {
            await graduationServices.CreateGraduationAsync(graduation);
        }
    }
}
