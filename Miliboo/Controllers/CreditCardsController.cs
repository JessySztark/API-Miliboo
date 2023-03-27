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
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCreditCard(int id, CreditCard objt) {
            if (id != objt.CardID) {
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
        public async Task<ActionResult<CreditCard>> PostCreditCard(CreditCard obj) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(obj);

            return CreatedAtAction("GetCreditCardById", new { id = obj.CardID }, obj);
        }

        // DELETE: api/CreditCards/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCreditCard(int id) {
            var obj = await dataRepository.GetByIdAsync(id);
            if (obj == null) {
                return NotFound();
            }
            await dataRepository.DeleteAsync(obj.Value);
            return Ok(obj);
        }
    }
}
