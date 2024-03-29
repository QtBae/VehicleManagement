﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.ApiModels;
using VehicleManagement.Services;

namespace VehicleManagement.Controllers
{
    [Route("api/Vehicle")]
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

        /// <summary>
        /// Get a vehicle by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ActionName("GetVehicleByIdAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetVehicleByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
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

        /// <summary>
        /// create a vehicle
        /// </summary>
        /// <param name="vehicle"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Update a vehicle
        /// </summary>
        /// <param name="vehicle"></param>
        /// <returns></returns>
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
            
            vehicle.Model = null;
            vehicle.Brand = null;

            var updatedVehicle = await _vehicleService.UpdateVehicleAsync(vehicle);
            if (updatedVehicle == null)
            {
                return BadRequest();
            }

            return Ok(updatedVehicle);
        }

        /// <summary>
        /// Delete a vehicle
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteVehicleAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var deleted = await _vehicleService.DeleteVehicleAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
