using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.ApiModels;
using VehicleManagement.Services;

namespace VehicleManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaintainanceController : ControllerBase
    {
        private readonly IMaintainanceService _maintainanceService;

        public MaintainanceController(IMaintainanceService maintainanceService)
        {
            _maintainanceService = maintainanceService;
        }

        /// <summary>
        /// Get all maintainances
        /// </summary>
        /// <returns> A list of maintainances if found or not found if no maintainances are found</returns>

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllMaintainancesAsync()
        {
            var maintainances = await _maintainanceService.GetAllMaintainancesAsync();
            if (maintainances == null)
            {
                return NotFound();
            }

            return Ok(maintainances);
        }

        [HttpGet("{id}")]
        [ActionName("GetMaintainanceByIdAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetMaintainanceByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var maintainance = await _maintainanceService.GetMaintainanceByIdAsync(id);
            if (maintainance == null)
            {
                return NotFound();
            }

            return Ok(maintainance);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateMaintainanceAsync([FromBody] MaintainanceModel maintainance)
        {
            if (maintainance == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdMaintainance = await _maintainanceService.CreateMaintainanceAsync(maintainance);
            if (createdMaintainance == null)
            {
                return BadRequest();
            }

            return CreatedAtAction("GetMaintainanceByIdAsync", new { id = createdMaintainance.Id }, createdMaintainance);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateMaintainanceAsync([FromBody] MaintainanceModel maintainance)
        {
            if (maintainance == null)
            {
                return BadRequest();
            }


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedMaintainance = await _maintainanceService.UpdateMaintainanceAsync(maintainance);
            if (updatedMaintainance == null)
            {
                return BadRequest();
            }

            return Ok(updatedMaintainance);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteMaintainanceAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var deleted = await _maintainanceService.DeleteMaintainanceAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
