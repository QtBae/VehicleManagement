using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.ApiModels;
using VehicleManagement.Services;

namespace VehicleManagement.Controllers
{
    [Route("api/Model")]
    [ApiController]
    public class ModelController : ControllerBase
    {
        private readonly ICarService _carService;

        public ModelController(ICarService carService)
        {
            _carService = carService;
        }

        /// <summary>
        /// Get all cars
        /// </summary>
        /// <returns> A list of cars if found or not found if no cars are found</returns>

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllCarsAsync()
        {
            var cars = await _carService.GetAllCarsAsync();
            if (cars == null)
            {
                return NotFound();
            }

            return Ok(cars);
        }

        /// <summary>
        /// Get a model by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ActionName("GetCarByIdAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCarByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var car = await _carService.GetCarByIdAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            return Ok(car);
        }

        /// <summary>
        /// Create a new model
        /// </summary>
        /// <param name="car"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCarAsync([FromBody] CarModel car)
        {
            if (car == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdCar = await _carService.CreateCarAsync(car);
            if (createdCar == null)
            {
                return BadRequest();
            }

            return CreatedAtAction("GetCarByIdAsync", new { id = createdCar.Id }, createdCar);
        }

        /// <summary>
        /// Update a model
        /// </summary>
        /// <param name="car"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCarAsync([FromBody] CarModel car)
        {
            if (car == null)
            {
                return BadRequest();
            }


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedCar = await _carService.UpdateCarAsync(car);
            if (updatedCar == null)
            {
                return BadRequest();
            }

            return Ok(updatedCar);
        }

        /// <summary>
        /// Delete a model
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteCarAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var deleted = await _carService.DeleteCarAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
