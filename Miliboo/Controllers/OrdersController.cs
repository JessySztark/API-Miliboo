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
    public class OrdersController : ControllerBase
    {
        private readonly IDataRepository<Order> _repository;

        public OrdersController(IDataRepository<Order> dataRepo)
        {
            _repository = dataRepo;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _repository.GetAllAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderById(int id)
        {
            var order = await _repository.GetByIdAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        [HttpGet("{cellphone}")]
        public async Task<ActionResult<Order>> GetOrderByCellphone(String cellphone)
        {
            var order = await _repository.GetByStringAsync(cellphone);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetOrderWithAccount(int id)
        {
            var account = await _repository.GetByIdAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            return account.Value.AccountOrder;
        }

        // PUT: api/Addresses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order objt)
        {
            if (id != objt.OrderID)
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

        // POST: api/Addresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            /*_context.Utilisateurs.Add(utilisateur);
            await _context.SaveChangesAsync();*/

            await _repository.AddAsync(obj);

            return CreatedAtAction("GetOrderById", new { id = obj.OrderID }, obj);
        }

        // DELETE: api/Addresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
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
