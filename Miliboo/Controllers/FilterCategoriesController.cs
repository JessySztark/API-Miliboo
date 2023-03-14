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
    public class FilterCategoriesController : ControllerBase {
        private readonly IDataRepository<FilterCategory> _repository;

        public FilterCategoriesController(IDataRepository<FilterCategory> dataRepo) {
            _repository = dataRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FilterCategory>>> GetFilterCategory() {
            return await _repository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FilterCategory>> GetFilterCategory(int id) {
            var filterCategory = await _repository.GetByIdAsync(id);

            if (filterCategory == null) {
                return NotFound();
            }

            return filterCategory;
        }

        [HttpGet("{filterCategoryname}")]
        public async Task<ActionResult<FilterCategory>> GetFilterCategoryByName(string filterCategoryname) {
            var filterCategory = await _repository.GetByStringAsync(filterCategoryname);

            if (filterCategory == null) {
                return NotFound();
            }

            return filterCategory;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutFilterCategory(int id, FilterCategory objt) {
            if (id != objt.FilterCategoryId) {
                return BadRequest();
            }

            var objToUpdate = await _repository.GetByIdAsync(id);

            if (objToUpdate.Value == null) {
                return NotFound();
            }
            else {
                await _repository.UpdateAsync(objToUpdate.Value, objt);
                return NoContent();
            }
        }

        [HttpPost]
        public async Task<ActionResult<FilterCategory>> PostFilterCategory(FilterCategory obj) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            await _repository.AddAsync(obj);

            return CreatedAtAction("GetFilterCategoryById", new { id = obj.FilterCategoryId }, obj);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFilterCategory(int id) {
            var obj = await _repository.GetByIdAsync(id);
            if (obj.Value == null) {
                return NotFound();
            }
            await _repository.DeleteAsync(obj.Value);
            return NoContent();
        }
    }
}
