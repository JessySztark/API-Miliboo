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
    public class TechnicalAspectsController : ControllerBase {
        private readonly IDataRepository<TechnicalAspect> _repository;

        public TechnicalAspectsController(IDataRepository<TechnicalAspect> dataRepo) {
            _repository = dataRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TechnicalAspect>>> GetTechnicalAspect() {
            return await _repository.GetAllAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<TechnicalAspect>> GetTechnicalAspect(int id) {
            var technicalAspect = await _repository.GetByIdAsync(id);

            if (technicalAspect == null) {
                return NotFound();
            }

            return technicalAspect;
        }

        [HttpGet("{aspectname}")]
        public async Task<ActionResult<TechnicalAspect>> GetTechnicalAspectFromPostalCode(string aspectname) {
            var technicalAspect = await _repository.GetByStringAsync(aspectname);

            if (technicalAspect == null) {
                return NotFound();
            }

            return technicalAspect;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTechnicalAspect(int id, TechnicalAspect objt) {
            if (id != objt.TechnicalAspectId) {
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
        public async Task<ActionResult<TechnicalAspect>> PostTechnicalAspect(TechnicalAspect obj) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            await _repository.AddAsync(obj);

            return CreatedAtAction("GetTechnicalAspectById", new { id = obj.TechnicalAspectId }, obj);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTechnicalAspect(int id) {
            var obj = await _repository.GetByIdAsync(id);
            if (obj.Value == null) {
                return NotFound();
            }
            await _repository.DeleteAsync(obj.Value);
            return NoContent();
        }
    }
}
