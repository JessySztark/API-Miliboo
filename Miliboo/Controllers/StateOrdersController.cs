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
    public class StateOrdersController : ControllerBase
    {
        private readonly IDataRepository<StateOrder> dataRepository;

        public StateOrdersController(IDataRepository<StateOrder> dataRepo)
        {
            dataRepository = dataRepo;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StateOrder>>> GetStateOrders()
        {
            return await dataRepository.GetAllAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StateOrder>> GetStateOrderById(int id)
        {
            var product = await dataRepository.GetByIdAsync(id);

            if (product == null || product.Value == null)  //marche pas sinon 
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/StateOrders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*[HttpPut("{id}")]
        public async Task<IActionResult> PutStateOrder(int id, StateOrder stateOrder)
        {
            if (id != stateOrder.StateOrderID)
            {
                return BadRequest();
            }

            _context.Entry(stateOrder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StateOrderExists(id))
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

        // POST: api/StateOrders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StateOrder>> PostStateOrder(StateOrder stateOrder)
        {
            _context.StateOrders.Add(stateOrder);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStateOrder", new { id = stateOrder.StateOrderID }, stateOrder);
        }

        // DELETE: api/StateOrders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStateOrder(int id)
        {
            var stateOrder = await _context.StateOrders.FindAsync(id);
            if (stateOrder == null)
            {
                return NotFound();
            }

            _context.StateOrders.Remove(stateOrder);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StateOrderExists(int id)
        {
            return _context.StateOrders.Any(e => e.StateOrderID == id);
        }*/
    }
}
