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
    public class AccountsController : ControllerBase
    {
        private readonly IDataRepository<Account> dataRepository;
        public AccountsController(IDataRepository<Account> dataRepo)
        {
            dataRepository = dataRepo;
        }
        // GET: api/Accounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
        {
            return await dataRepository.GetAllAsync();
        }

        // GET: api/Accounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccount(int id)
        {
            var account = await dataRepository.GetByIdAsync(id);

            if (account == null || account.Value == null)  //marche pas sinon 
            {
                return NotFound();
            }
            return account;
        }

        // PUT: api/Accounts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount(int id, Account account)
        {
            if (id != account.AccountID)
            {
                return BadRequest();
            }
            var accountToUpdate = await dataRepository.GetByIdAsync(id);
            if (accountToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.UpdateAsync(accountToUpdate.Value, account);
                return NoContent();
            }
        }

        // POST: api/Accounts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Account>> PostAccount(Account account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepository.AddAsync(account);
            return CreatedAtAction("GetById", new { id = account.AccountID }, account);
        }

        // DELETE: api/Accounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            var account = await dataRepository.GetByIdAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            await dataRepository.DeleteAsync(account.Value);
            return NoContent();
        }

       /* private bool AccountExists(int id)
        {
            return _context.Account.Any(e => e.AccountID == id);
        }*/
    }
}
