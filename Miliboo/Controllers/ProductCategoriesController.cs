using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Miliboo.Models.EntityFramework;
using Miliboo.Models.Repository;
using NuGet.Protocol.Core.Types;

namespace Miliboo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoriesController : ControllerBase
    {
        private readonly IDataRepository<ProductCategory> _repository;

        public ProductCategoriesController(IDataRepository<ProductCategory> dataRepo)
        {
            _repository = dataRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductCategory>>> GetProductCategory()
        {
            return await _repository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductCategory>> GetProductCategory(int id)
        {
            var productCategory = await _repository.GetByIdAsync(id);

            if (productCategory == null)
            {
                return NotFound();
            }

            return productCategory;
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<ProductCategory>> GetOrderByCategoryName(String name)
        {
            var category = await _repository.GetByStringAsync(name);

            if (category == null)
            {
                return NotFound();
            }
            return category;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductCategory(int id, ProductCategory objt)
        {
            if (id != objt.ProductCategoryId)
            {
                return BadRequest();
            }

            var objToUpdate = await _repository.GetByIdAsync(id);

            if (objToUpdate.Value == null)
            {
                return NotFound();
            }
            else
            {
                await _repository.UpdateAsync(objToUpdate.Value, objt);
                return NoContent();
            }
        }

        [HttpPost]
        public async Task<ActionResult<Address>> PostOrder(ProductCategory obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repository.AddAsync(obj);

            return CreatedAtAction("GetProductCategoryById", new { id = obj.ProductCategoryId }, obj);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryProduct(int id)
        {
            var obj = await _repository.GetByIdAsync(id);
            if (obj.Value == null)
            {
                return NotFound();
            }
            await _repository.DeleteAsync(obj.Value);
            return NoContent();
        }
    }
}
