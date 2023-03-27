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
    public class DiscountsController : ControllerBase {
        private readonly IDataRepository<Discount> _repository;

        public DiscountsController(IDataRepository<Discount> dataRepo) {
            _repository = dataRepo;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Discount>>> GetDiscounts() {
            return await _repository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Discount>> GetDiscountById(int id) {
            var Discount = await _repository.GetByIdAsync(id);

            if (Discount == null) {
                return NotFound();
            }

            return Discount;
        }

        [HttpGet("{discountname}")]
        public async Task<ActionResult<Discount>> GetDeliveryAddressFromFavName(string discountname) {
            var discount = await _repository.GetByStringAsync(discountname);
            if (discount == null) {
                return NotFound();
            }
            return discount;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDiscount(int id, Discount objt) {
            if (id != objt.DiscountID) {
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
        public async Task<ActionResult<Discount>> PostDiscount(Discount obj) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            await _repository.AddAsync(obj);

            return CreatedAtAction("GetDiscountById", new { id = obj.DiscountID }, obj);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiscount(int id) {
            var obj = await _repository.GetByIdAsync(id);
            if (obj == null) {
                return NotFound();
            }
            await _repository.DeleteAsync(obj.Value);
            return Ok(obj);
        }
    }
}
