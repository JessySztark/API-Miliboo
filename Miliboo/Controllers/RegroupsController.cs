using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Miliboo.Models.EntityFramework;
using Miliboo.Models.Repository;

namespace MilibooAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegroupsController : ControllerBase
    {
        private readonly IDataRepository<Regroup> dataRepository;

        public RegroupsController(IDataRepository<Regroup> dataRepo)
        {
            dataRepository = dataRepo;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Regroup>>> GetRegroups()
        {
            return await dataRepository.GetAllAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Regroup>> GetRegroupById(int id)
        {
            var regroup = await dataRepository.GetByIdAsync(id);

            if (regroup == null || regroup.Value == null)  //marche pas sinon 
            {
                return NotFound();
            }

            return regroup;
        }

        // PUT: api/Regroups/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRegroup(int id, Regroup objt) {
            if (id != objt.RegroupId) {
                return BadRequest();
            }

            var objToUpdate = await dataRepository.GetByIdAsync(id);

            if (objToUpdate == null) {
                return NotFound();
            }
            else {
                await dataRepository.UpdateAsync(objToUpdate.Value, objt);
                return Ok(objt);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Regroup>> PostRegroup(Regroup obj) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            await dataRepository.AddAsync(obj);

            return CreatedAtAction("GetRegroupById", new { id = obj.RegroupId }, obj);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegroup(int id) {
            var obj = await dataRepository.GetByIdAsync(id);
            if (obj == null) {
                return NotFound();
            }
            await dataRepository.DeleteAsync(obj.Value);
            return Ok(obj);
        }
    }
}
