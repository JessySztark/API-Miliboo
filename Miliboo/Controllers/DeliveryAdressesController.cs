using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Miliboo.Models.EntityFramework;
using Miliboo.Models.Repository;

namespace Miliboo.Controllers {
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DeliveryAdressesController : ControllerBase {
        private readonly IDataRepository<DeliveryAdress> _repository;

        public DeliveryAdressesController(IDataRepository<DeliveryAdress> dataRepo) {
            _repository = dataRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeliveryAdress>>> GetDeliveryAdresses() {
            return await _repository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DeliveryAdress>> GetDeliveryAdressById(int id) {
            var DeliveryAdress = await _repository.GetByIdAsync(id);
            if (DeliveryAdress == null) {
                return NotFound();
            }
            return DeliveryAdress;
        }

        [HttpGet("{favname}")]
        public async Task<ActionResult<DeliveryAdress>> GetDeliveryAddressFromFavName(string favname) {
            var address = await _repository.GetByStringAsync(favname);
            if (address == null) {
                return NotFound();
            }
            return address;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeliveryAdress(int id, DeliveryAdress objt) {
            if (id != objt.IdDeliveryAdress) {
                return BadRequest();
            }
            var objToUpdate = await _repository.GetByIdAsync(id);
            if (objToUpdate == null) {
                return NotFound();
            }
            else {
                await _repository.UpdateAsync(objToUpdate.Value, objt);
                return Ok(objt);
            }
        }

        [HttpPost]
        public async Task<ActionResult<DeliveryAdress>> PostDeliveryAdress(DeliveryAdress obj) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            await _repository.AddAsync(obj);
            return CreatedAtAction("GetDeliveryAdressById", new { id = obj.IdDeliveryAdress }, obj);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeliveryAdress(int id) {
            var obj = await _repository.GetByIdAsync(id);
            if (obj == null) {
                return NotFound();
            }
            await _repository.DeleteAsync(obj.Value);
            return Ok(obj);
        }
    }
}
