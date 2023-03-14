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
        public async Task<ActionResult<IEnumerable<Filter>>> GetFilter() {
            return await _repository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Filter>> GetFilter(int id) {
            var Filter = await _repository.GetByIdAsync(id);

            if (Filter == null) {
                return NotFound();
            }

            return Filter;
        }

        [HttpGet("{filtername}")]
        public async Task<ActionResult<Filter>> GetFilterByName(string filtername) {
            var filter = await _repository.GetByStringAsync(filtername);

            if (filter == null) {
                return NotFound();
            }

            return filter;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutFilter(int id, Filter objt) {
            if (id != objt.FilterId) {
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
            if (obj.Value == null) {
                return NotFound();
            }
            await _repository.DeleteAsync(obj.Value);
            return NoContent();
        }
    }
}
