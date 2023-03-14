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
    public class DeliveryMethodsController : ControllerBase
    {
        private readonly IDataRepository<DeliveryMethod> dataRepository;
        public DeliveryMethodsController(IDataRepository<DeliveryMethod> dataRepo)
        {
            dataRepository = dataRepo;
        }

        // GET: api/Countries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeliveryMethod>>> GetDeliveryMethods()
        {
            return await dataRepository.GetAllAsync();
        }

        // GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DeliveryMethod>> GetDeliveryMethodtById(int id)
        {
            var country = await dataRepository.GetByIdAsync(id);
            if (country == null || country.Value == null)  //marche pas sinon 
            {
                return NotFound();
            }
            return country;
        }

        // PUT: api/DeliveryMethods/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*[HttpPut("{id}")]
        public async Task<IActionResult> PutDeliveryMethod(int id, DeliveryMethod deliveryMethod)
        {
            if (id != deliveryMethod.IdDeliveryMethod)
            {
                return BadRequest();
            }

            _context.Entry(deliveryMethod).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeliveryMethodExists(id))
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

        // POST: api/DeliveryMethods
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DeliveryMethod>> PostDeliveryMethod(DeliveryMethod deliveryMethod)
        {
            _context.DeliveryMethods.Add(deliveryMethod);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDeliveryMethod", new { id = deliveryMethod.IdDeliveryMethod }, deliveryMethod);
        }

        // DELETE: api/DeliveryMethods/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeliveryMethod(int id)
        {
            var deliveryMethod = await _context.DeliveryMethods.FindAsync(id);
            if (deliveryMethod == null)
            {
                return NotFound();
            }

            _context.DeliveryMethods.Remove(deliveryMethod);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DeliveryMethodExists(int id)
        {
            return _context.DeliveryMethods.Any(e => e.IdDeliveryMethod == id);
        }*/
    }
}
