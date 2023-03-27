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
        public async Task<ActionResult<DeliveryMethod>> GetDeliveryMethodById(int id)
        {
            var country = await dataRepository.GetByIdAsync(id);
            if (country == null || country.Value == null)  //marche pas sinon 
            {
                return NotFound();
            }
            return country;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeliveryMethod(int id, DeliveryMethod objt) {
            if (id != objt.IdDeliveryMethod) {
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
        public async Task<ActionResult<DeliveryMethod>> PostDeliveryMethod(DeliveryMethod obj) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            await dataRepository.AddAsync(obj);
            return CreatedAtAction("GetDeliveryMethodById", new { id = obj.IdDeliveryMethod }, obj);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeliveryMethod(int id) {
            var obj = await dataRepository.GetByIdAsync(id);
            if (obj == null) {
                return NotFound();
            }
            await dataRepository.DeleteAsync(obj.Value);
            return Ok(obj);
        }
    }
}
