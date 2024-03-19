using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.ApiModels;
using VehicleManagement.Services;

namespace VehicleManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        /// <summary>
        /// Get all vehicles
        /// </summary>
        /// <returns> A list of vehicles if found or not found if no vehicles are found</returns>

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllVehiclesAsync()
        {
            var vehicles = await _vehicleService.GetAllVehiclesAsync();
            if (vehicles == null)
            {
                return NotFound();
            }

            return Ok(vehicles);
        }

        [HttpGet("{id}")]
        [ActionName("GetVehicleByIdAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetVehicleByIdAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var vehicle = await _vehicleService.GetVehicleByIdAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return Ok(vehicle);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateVehicleAsync([FromBody] VehicleModel vehicle)
        {
            if (vehicle == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdVehicle = await _vehicleService.CreateVehicleAsync(vehicle);
            if (createdVehicle == null)
            {
                return BadRequest();
            }

            return CreatedAtAction("GetVehicleByIdAsync", new { id = createdVehicle.Id }, createdVehicle);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateVehicleAsync([FromBody] VehicleModel vehicle)
        {
            if (vehicle == null)
            {
                return BadRequest();
            }


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedVehicle = await _vehicleService.UpdateVehicleAsync(vehicle);
            if (updatedVehicle == null)
            {
                return BadRequest();
            }

            return Ok(updatedVehicle);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteVehicleAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var deleted = await _vehicleService.DeleteVehicleAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
