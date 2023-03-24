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
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IsFilteredsController : ControllerBase
    {
        private readonly IDataRepository<IsFiltered> dataRepository;

        public IsFilteredsController(IDataRepository<IsFiltered> dataRepo)
        {
            dataRepository = dataRepo;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IsFiltered>>> GetIsFiltereds()
        {
            return await dataRepository.GetAllAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IsFiltered>> GetIsFilteredById(int id)
        {
            var isFiltered = await dataRepository.GetByIdAsync(id);

            if (isFiltered == null || isFiltered.Value == null)  //marche pas sinon 
            {
                return NotFound();
            }

            return isFiltered;
        }

        // PUT: api/IsFiltereds/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
       /* [HttpPut("{id}")]
        public async Task<IActionResult> PutIsFiltered(int id, IsFiltered isFiltered)
        {
            if (id != isFiltered.IsFilteredId)
            {
                return BadRequest();
            }

            _context.Entry(isFiltered).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IsFilteredExists(id))
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

        // POST: api/IsFiltereds
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<IsFiltered>> PostIsFiltered(IsFiltered isFiltered)
        {
            _context.IsFiltereds.Add(isFiltered);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIsFiltered", new { id = isFiltered.IsFilteredId }, isFiltered);
        }

        // DELETE: api/IsFiltereds/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIsFiltered(int id)
        {
            var isFiltered = await _context.IsFiltereds.FindAsync(id);
            if (isFiltered == null)
            {
                return NotFound();
            }

            _context.IsFiltereds.Remove(isFiltered);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IsFilteredExists(int id)
        {
            return _context.IsFiltereds.Any(e => e.IsFilteredId == id);
        }*/
    }
}
