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
    public class ConcernedsController : ControllerBase
    {
        private readonly IDataRepository<Concerned> _repository;

        public ConcernedsController(IDataRepository<Concerned> dataRepo)
        {
            _repository = dataRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Concerned>>> GetConcerned()
        {
            return await _repository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Concerned>> GetConcernedByID(int id)
        {
            var concerned = await _repository.GetByIdAsync(id);

            if (concerned == null)
            {
                return NotFound();
            }

            return concerned;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutConcerned(int id, Concerned objt)
        {
            if (id != objt.ConcernedId)
            {
                return BadRequest();
            }

            var objToUpdate = await _repository.GetByIdAsync(id);

            if (objToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await _repository.UpdateAsync(objToUpdate.Value, objt);
                return Ok(objt);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Concerned>> PostConcerned(Concerned obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repository.AddAsync(obj);

            return CreatedAtAction("GetConcernedById", new { id = obj.ConcernedId }, obj);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConcerned(int id)
        {
            var obj = await _repository.GetByIdAsync(id);
            if (obj == null)
            {
                return NotFound();
            }
            await _repository.DeleteAsync(obj.Value);
            return Ok(obj);
        }
    }
}
