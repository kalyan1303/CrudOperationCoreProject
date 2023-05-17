using CrudOperationsInNetCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudOperationsInNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly BrandContext _dbContext;
        public BrandController(BrandContext dbContext)
        {
            _dbContext = dbContext;
        }
        //private IBrandRepository _brandRepository;
        //public BrandController(IBrandRepository brandRepository)
        //{
        //    _brandRepository = brandRepository;
        //}
       

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brand>>> GetBrands()
        {
            if (_dbContext.Brands == null)
            {
                return NotFound();

            }
            return await _dbContext.Brands.ToListAsync();/* GetBrands(pagingParameters)   Brands.ToListAsync();*/

        }

        //[HttpGet("{id}/page/{pageNumber}")]
        //public async Task<ActionResult<Brand>>GetBrand(int id ,PagingParameters pagingParameters)
        //{
        //  var data= await _brandRepository.GetBrand(id, pagingParameters);

        //    return data;
        //}
        //[HttpGet]
        //public IActionResult GetBrand(Guid Id, [FromQuery] PagingParameters pagingParameters)
        //{
        //    var accounts = _brandRepository.GetBrand(Id, pagingParameters);

        //    var metadata = new
        //    {
        //        accounts.TotalCount,
        //        accounts.PageSize,
        //        accounts.CurrentPage,
        //        accounts.TotalPages,
        //        accounts.HasNext,
        //        accounts.HasPrevious
        //    };

        //    Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

        //    _logger.LogInfo($"Returned {accounts.TotalCount} owners from database.");

        //    return Ok(accounts);
        //}

        [HttpGet("{id}")]
        public async Task<ActionResult<Brand>> GetBrand(int id)
        {
            if (_dbContext.Brands == null)
            {
                return NotFound();

            }
            var brand =await _dbContext.Brands.FindAsync(id);   
          if (brand == null)
           {
               return NotFound();
             }
            return brand;

        }
        [HttpPost]
        public async Task<ActionResult<Brand>> PostBrands(Brand brand)
        {
            _dbContext.Brands.Add(brand);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBrand), new { id = brand.ID }, brand);

        }
        [HttpPut]
        public async Task<ActionResult> UpdateBrand(int id, Brand brand)
        {
            if (id != brand.ID)
            {
                return NotFound();

            }
            _dbContext.Entry(brand).State = EntityState.Modified;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrandAvailable(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok();
        }
        private bool BrandAvailable(int id)
        {
            return (_dbContext.Brands?.Any(x => x.ID == id)).GetValueOrDefault();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBrand(int id)
        {
            if (_dbContext.Brands == null)
            {
                return NotFound();

            }
            var brand = await _dbContext.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            _dbContext.Brands.Remove(brand);
            await _dbContext.SaveChangesAsync();
            return Ok();

        }
    }
   
}
