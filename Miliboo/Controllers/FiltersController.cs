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
    public class FiltersController : ControllerBase {
        private readonly IDataRepository<Filter> _repository;

        public FiltersController(IDataRepository<Filter> dataRepo) {
            _repository = dataRepo;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Filter>>> GetFilters() {
            return await _repository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Filter>> GetFilterById(int id) {
            var Filter = await _repository.GetByIdAsync(id);

            if (Filter == null) {
                return NotFound();
            }

            return Filter;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutFilter(int id, Filter objt) {
            if (id != objt.FilterId) {
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
        public async Task<ActionResult<Filter>> PostFilter(Filter obj) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            await _repository.AddAsync(obj);

            return CreatedAtAction("GetFilterById", new { id = obj.FilterId }, obj);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFilter(int id) {
            var obj = await _repository.GetByIdAsync(id);
            if (obj == null) {
                return NotFound();
            }
            await _repository.DeleteAsync(obj.Value);
            return Ok(obj);
        }
    }
}
