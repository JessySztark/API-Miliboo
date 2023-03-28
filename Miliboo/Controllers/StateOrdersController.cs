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

        [HttpPut("{id}")]
        public async Task<IActionResult> PutStateOrder(int id, StateOrder objt) {
            if (id != objt.StateOrderID) {
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
        public async Task<ActionResult<StateOrder>> PostStateOrder(StateOrder obj) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(obj);

            return CreatedAtAction("GetStateOrderById", new { id = obj.StateOrderID }, obj);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStateOrder(int id) {
            var obj = await dataRepository.GetByIdAsync(id);
            if (obj == null) {
                return NotFound();
            }
            await dataRepository.DeleteAsync(obj.Value);
            return Ok(obj);
        }
    }
}
