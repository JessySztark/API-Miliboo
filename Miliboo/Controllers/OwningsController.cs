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
    public class OwningsController : ControllerBase {
        private readonly IDataRepository<Owning> _repository;

        public OwningsController(IDataRepository<Owning> dataRepo) {
            _repository = dataRepo;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Owning>>> GetOwning() {
            return await _repository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Owning>> GetOwning(int id) {
            var Owning = await _repository.GetByIdAsync(id);

            if (Owning == null) {
                return NotFound();
            }

            return Owning;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOwning(int id, Owning objt) {
            if (id != objt.IDAddress || id != objt.IDAccount) {
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
        public async Task<ActionResult<Owning>> PostOwning(Owning obj) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            await _repository.AddAsync(obj);

            return CreatedAtAction("GetOwningById", new { id = obj.IDAddress }, obj);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOwning(int id) {
            var obj = await _repository.GetByIdAsync(id);
            if (obj.Value == null) {
                return NotFound();
            }
            await _repository.DeleteAsync(obj.Value);
            return NoContent();
        }
    }
}
