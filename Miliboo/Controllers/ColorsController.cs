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
    public class ColorsController : ControllerBase
    {
        private readonly IDataRepository<Color> dataRepository;
        public ColorsController(IDataRepository<Color> dataRepo)
        {
            dataRepository = dataRepo;
        }

        // GET: api/Colors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Color>>> GetColors()
        {
            return await dataRepository.GetAllAsync();
        }

        // GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Color>> GetColorById(int id)
        {
            var color = await dataRepository.GetByIdAsync(id);

            if (color == null || color.Value == null)  //marche pas sinon 
            {
                return NotFound();
            }
            return color;
        }

        // PUT: api/Comments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutColor(int id, Color color) {
            if (id != color.ColorId) {
                return BadRequest();
            }
            var colorToUpdate = await dataRepository.GetByIdAsync(id);
            if (colorToUpdate == null) {
                return NotFound();
            }
            else {
                await dataRepository.UpdateAsync(colorToUpdate.Value, color);
                return Ok(color);
            }
        }

        // POST: api/Comments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Color>> PostColor(Color color) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            await dataRepository.AddAsync(color);
            return CreatedAtAction("GetById", new { id = color.ColorId }, color);
        }

        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteColor(int id) {
            var color = await dataRepository.GetByIdAsync(id);
            if (color == null) {
                return NotFound();
            }
            await dataRepository.DeleteAsync(color.Value);
            return Ok(color);
        }
    }
}
