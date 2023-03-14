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
    public class CreditCardsController : ControllerBase
    {
        private readonly IDataRepository<CreditCard> dataRepository;
        public CreditCardsController(IDataRepository<CreditCard> dataRepo)
        {
            dataRepository = dataRepo;
        }

        // GET: api/CreditCards
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CreditCard>>> GetCreditCards()
        {
            return await dataRepository.GetAllAsync();
        }

        // GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CreditCard>> GetCreditCardById(int id)
        {
            var card = await dataRepository.GetByIdAsync(id);
            if (card == null || card.Value == null)  //marche pas sinon 
            {
                return NotFound();
            }
            return card;
        }

        // PUT: api/CreditCards/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /* [HttpPut("{id}")]
         public async Task<IActionResult> PutCreditCard(int id, CreditCard creditCard)
         {
             if (id != creditCard.CardID)
             {
                 return BadRequest();
             }

             _context.Entry(creditCard).State = EntityState.Modified;

             try
             {
                 await _context.SaveChangesAsync();
             }
             catch (DbUpdateConcurrencyException)
             {
                 if (!CreditCardExists(id))
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

         // POST: api/CreditCards
         // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
         [HttpPost]
         public async Task<ActionResult<CreditCard>> PostCreditCard(CreditCard creditCard)
         {
             _context.CreditCards.Add(creditCard);
             await _context.SaveChangesAsync();

             return CreatedAtAction("GetCreditCard", new { id = creditCard.CardID }, creditCard);
         }

         // DELETE: api/CreditCards/5
         [HttpDelete("{id}")]
         public async Task<IActionResult> DeleteCreditCard(int id)
         {
             var creditCard = await _context.CreditCards.FindAsync(id);
             if (creditCard == null)
             {
                 return NotFound();
             }

             _context.CreditCards.Remove(creditCard);
             await _context.SaveChangesAsync();

             return NoContent();
         }

         private bool CreditCardExists(int id)
         {
             return _context.CreditCards.Any(e => e.CardID == id);
         }*/
    }
}
