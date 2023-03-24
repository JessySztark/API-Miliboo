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
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AsAspectsController : ControllerBase
    {
        private readonly IDataRepository<AsAspect> _repository;

        public AsAspectsController(IDataRepository<AsAspect> dataRepo)
        {
            _repository = dataRepo;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<AsAspect>>> GetAsAspect()
        {
            return await _repository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AsAspect>> GetAsAspect(int id)
        {
            var AsAspect = await _repository.GetByIdAsync(id);

            if (AsAspect == null)
            {
                return NotFound();
            }

            return AsAspect;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsAspect(int id, AsAspect objt)
        {
            if (id != objt.ProductTypeId)
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
        public async Task<ActionResult<AsAspect>> PostAsAspect(AsAspect obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _repository.AddAsync(obj);

            return CreatedAtAction("GetAsAspectById", new { id = obj.ProductTypeId }, obj);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsAspect(int id)
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
