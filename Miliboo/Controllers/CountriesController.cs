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
    public class CountriesController : ControllerBase
    {
        private readonly IDataRepository<Country> dataRepository;
        public CountriesController(IDataRepository<Country> dataRepo)
        {
            dataRepository = dataRepo;
        }

        // GET: api/Countries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Country>>> GetCountries()
        {
            return await dataRepository.GetAllAsync();
        }

        // GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> GetCountryById(int id)
        {
            var country = await dataRepository.GetByIdAsync(id);
            if (country == null || country.Value == null)  //marche pas sinon 
            {
                return NotFound();
            }
            return country;
        }

        [HttpGet("{wording}")]
        public async Task<ActionResult<Country>> GetCountryByWording(String wording) {
            var country = await dataRepository.GetByStringAsync(wording);
            if (country == null) {
                return NotFound();
            }
            return country;
        }

        // PUT: api/Countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry(int id, Country objt) {
            if (id != objt.CountryID) {
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
        public async Task<ActionResult<Country>> PostCountry(Country obj) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(obj);

            return CreatedAtAction("GetCountryById", new { id = obj.CountryID }, obj);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id) {
            var obj = await dataRepository.GetByIdAsync(id);
            if (obj == null) {
                return NotFound();
            }
            await dataRepository.DeleteAsync(obj.Value);
            return Ok(obj);
        }
    }
}
