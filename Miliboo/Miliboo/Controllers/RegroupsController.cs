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
        public async Task<ActionResult<IEnumerable<Regroup>>> GetProducts()
        {
            return await dataRepository.GetAllAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Regroup>> GetProductById(int id)
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
        /*[HttpPut("{id}")]
        public async Task<IActionResult> PutRegroup(int id, Regroup regroup)
        {
            if (id != regroup.RegroupId)
            {
                return BadRequest();
            }

            _context.Entry(regroup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegroupExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Regroups
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Regroup>> PostRegroup(Regroup regroup)
        {
            _context.Regroups.Add(regroup);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRegroup", new { id = regroup.RegroupId }, regroup);
        }

        // DELETE: api/Regroups/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegroup(int id)
        {
            var regroup = await _context.Regroups.FindAsync(id);
            if (regroup == null)
            {
                return NotFound();
            }

            _context.Regroups.Remove(regroup);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RegroupExists(int id)
        {
            return _context.Regroups.Any(e => e.RegroupId == id);
        }*/
    }
}
