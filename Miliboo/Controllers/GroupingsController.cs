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
    public class GroupingsController : ControllerBase
    {
        private readonly IDataRepository<Grouping> dataRepository;

        public GroupingsController(IDataRepository<Grouping> dataRepo)
        {
            dataRepository = dataRepo;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Grouping>>> GetGroupings()
        {
            return await dataRepository.GetAllAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Grouping>> GetGroupingById(int id)
        {
            var grouping = await dataRepository.GetByIdAsync(id);

            if (grouping == null || grouping.Value == null)  //marche pas sinon 
            {
                return NotFound();
            }

            return grouping;
        }

        // PUT: api/Groupings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
       /* [HttpPut("{id}")]
        public async Task<IActionResult> PutGrouping(int id, Grouping grouping)
        {
            if (id != grouping.GroupingId)
            {
                return BadRequest();
            }

            _context.Entry(grouping).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupingExists(id))
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

        // POST: api/Groupings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Grouping>> PostGrouping(Grouping grouping)
        {
            _context.Groupings.Add(grouping);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGrouping", new { id = grouping.GroupingId }, grouping);
        }

        // DELETE: api/Groupings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGrouping(int id)
        {
            var grouping = await _context.Groupings.FindAsync(id);
            if (grouping == null)
            {
                return NotFound();
            }

            _context.Groupings.Remove(grouping);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GroupingExists(int id)
        {
            return _context.Groupings.Any(e => e.GroupingId == id);
        }*/
    }
}
