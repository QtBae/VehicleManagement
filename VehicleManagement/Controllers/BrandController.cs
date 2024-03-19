using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.ApiModels;
using VehicleManagement.Services;

namespace VehicleManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        /// <summary>
        /// Get all brands
        /// </summary>
        /// <returns> A list of brands if found or not found if no brands are found</returns>

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllBrandsAsync()
        {
            var brands = await _brandService.GetAllBrandsAsync();
            if (brands == null)
            {
                return NotFound();
            }

            return Ok(brands);
        }

        [HttpGet("{id}")]
        [ActionName("GetBrandByIdAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetBrandByIdAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var brand = await _brandService.GetBrandByIdAsync(id);
            if (brand == null)
            {
                return NotFound();
            }

            return Ok(brand);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateBrandAsync([FromBody] BrandModel brand)
        {
            if (brand == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdBrand = await _brandService.CreateBrandAsync(brand);
            if (createdBrand == null)
            {
                return BadRequest();
            }

            return CreatedAtAction("GetBrandByIdAsync", new { id = createdBrand.Id }, createdBrand);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateBrandAsync([FromBody] BrandModel brand)
        {
            if (brand == null)
            {
                return BadRequest();
            }


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedBrand = await _brandService.UpdateBrandAsync(brand);
            if (updatedBrand == null)
            {
                return BadRequest();
            }

            return Ok(updatedBrand);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteBrandAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var deleted = await _brandService.DeleteBrandAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
