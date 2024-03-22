using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.ApiModels;
using Swashbuckle.AspNetCore.Annotations;
using VehicleManagement.Services;

namespace VehicleManagement.Controllers
{
    [Route("api/Brand")]
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


        /// <summary>
        /// Get a brand by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("{id}")]
        [ActionName("GetBrandByIdAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetBrandByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
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

        /// <summary>
        /// Create a new brand
        /// </summary>
        /// <param name="brand"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Update a brand
        /// </summary>
        /// <param name="brand"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Delete a brand
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteBrandAsync(Guid id)
        {
            if (id == Guid.Empty)
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
