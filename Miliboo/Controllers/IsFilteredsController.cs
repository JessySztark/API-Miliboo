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
    public class IsFilteredsController : ControllerBase
    {
        private readonly IDataRepository<IsFiltered> dataRepository;

        public IsFilteredsController(IDataRepository<IsFiltered> dataRepo)
        {
            dataRepository = dataRepo;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IsFiltered>>> GetIsFiltereds()
        {
            return await dataRepository.GetAllAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IsFiltered>> GetIsFilteredById(int id)
        {
            var isFiltered = await dataRepository.GetByIdAsync(id);

            if (isFiltered == null || isFiltered.Value == null)  //marche pas sinon 
            {
                return NotFound();
            }

            return isFiltered;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutIsFiltered(int id, IsFiltered objt) {
            if (id != objt.IsFilteredId) {
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
        public async Task<ActionResult<IsFiltered>> PostIsFiltered(IsFiltered obj) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            await dataRepository.AddAsync(obj);

            return CreatedAtAction("GetIsFilteredById", new { id = obj.IsFilteredId }, obj);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIsFiltered(int id) {
            var obj = await dataRepository.GetByIdAsync(id);
            if (obj == null) {
                return NotFound();
            }
            await dataRepository.DeleteAsync(obj.Value);
            return Ok(obj);
        }
    }
}
