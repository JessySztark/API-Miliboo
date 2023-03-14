using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Miliboo.Models.EntityFramework;
using Miliboo.Models.Repository;

namespace Miliboo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsFiltersController : ControllerBase
    {
        private readonly IDataRepository<AsFilter> _repository;

        public AsFiltersController(IDataRepository<AsFilter> dataRepo)
        {
            _repository = dataRepo;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<AsFilter>>> GetAsFilter()
        {
            return await _repository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AsFilter>> GetAsFilter(int id)
        {
            var AsFilter = await _repository.GetByIdAsync(id);

            if (AsFilter == null)
            {
                return NotFound();
            }

            return AsFilter;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsFilter(int id, AsFilter objt)
        {
            if (id != objt.FilterCategoryId)
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
        public async Task<ActionResult<AsFilter>> PostAsFilter(AsFilter obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _repository.AddAsync(obj);

            return CreatedAtAction("GetAsFilterById", new { id = obj.FilterCategoryId }, obj);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsFilter(int id)
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
