using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Miliboo.Models.EntityFramework;
using Miliboo.Models.Repository;

namespace Miliboo.Controllers {
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductTypesController : ControllerBase {
        private readonly IDataRepository<ProductType> _repository;

        public ProductTypesController(IDataRepository<ProductType> dataRepo) {
            _repository = dataRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductType>>> GetProductTypes() {
            return await _repository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductType>> GetProductTypeById(int id) {
            var productType = await _repository.GetByIdAsync(id);

            if (productType == null) {
                return NotFound();
            }

            return productType;
        }

        [HttpGet("{productTypename}")]
        public async Task<ActionResult<ProductType>> GetProductTypeByName(string productTypename) {
            var productType = await _repository.GetByStringAsync(productTypename);

            if (productType == null) {
                return NotFound();
            }

            return productType;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductType(int id, ProductType objt) {
            if (id != objt.ProductTypeId) {
                return BadRequest();
            }

            var objToUpdate = await _repository.GetByIdAsync(id);

            if (objToUpdate == null) {
                return NotFound();
            }
            else {
                await _repository.UpdateAsync(objToUpdate.Value, objt);
                return Ok(objt);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ProductType>> PostProductType(ProductType obj) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            await _repository.AddAsync(obj);

            return CreatedAtAction("GetProductTypeById", new { id = obj.ProductTypeId }, obj);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductType(int id) {
            var obj = await _repository.GetByIdAsync(id);
            if (obj == null) {
                return NotFound();
            }
            await _repository.DeleteAsync(obj.Value);
            return Ok(obj);
        }
    }
}
