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
    public class PaymentMethodsController : ControllerBase
    {
        private readonly IDataRepository<PaymentMethod> dataRepository;

        public PaymentMethodsController(IDataRepository<PaymentMethod> dataRepo)
        {
            dataRepository = dataRepo;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentMethod>>> GetPaymentMethods()
        {
            return await dataRepository.GetAllAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentMethod>> GetPaymentMethodById(int id)
        {
            var paymentMethod = await dataRepository.GetByIdAsync(id);

            if (paymentMethod == null || paymentMethod.Value == null)  //marche pas sinon 
            {
                return NotFound();
            }
            return paymentMethod;
        }

        // PUT: api/PaymentMethods/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaymentMethod(int id, PaymentMethod objt) {
            if (id != objt.Paymentmethodid) {
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
        public async Task<ActionResult<PaymentMethod>> PostPaymentMethod(PaymentMethod obj) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            await dataRepository.AddAsync(obj);

            return CreatedAtAction("GetPaymentMethodById", new { id = obj.Paymentmethodid }, obj);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentMethod(int id) {
            var obj = await dataRepository.GetByIdAsync(id);
            if (obj == null) {
                return NotFound();
            }
            await dataRepository.DeleteAsync(obj.Value);
            return Ok(obj);
        }
    }
}
