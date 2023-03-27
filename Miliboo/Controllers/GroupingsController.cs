using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Miliboo.Models.EntityFramework;
using Miliboo.Models.Repository;

namespace MilibooAPI.Controllers {
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GroupingsController : ControllerBase {
        private readonly IDataRepository<Grouping> dataRepository;

        public GroupingsController(IDataRepository<Grouping> dataRepo) {
            dataRepository = dataRepo;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Grouping>>> GetGroupings() {
            return await dataRepository.GetAllAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Grouping>> GetGroupingById(int id) {
            var grouping = await dataRepository.GetByIdAsync(id);

            if (grouping == null || grouping.Value == null) {
                return NotFound();
            }

            return grouping;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutGrouping(int id, Grouping objt) {
            if (id != objt.GroupingId) {
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
        public async Task<ActionResult<Grouping>> PostGrouping(Grouping obj) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            await dataRepository.AddAsync(obj);

            return CreatedAtAction("GetGroupingById", new { id = obj.GroupingId }, obj);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGrouping(int id) {
            var obj = await dataRepository.GetByIdAsync(id);
            if (obj == null) {
                return NotFound();
            }
            await dataRepository.DeleteAsync(obj.Value);
            return Ok(obj);
        }
    }
}
