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
    public class PhotosController : ControllerBase
    {
        private readonly IDataRepository<Photo> dataRepository;

        public PhotosController(IDataRepository<Photo> dataRepo)
        {
            dataRepository = dataRepo;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Photo>>> GetPhotos()
        {
            return await dataRepository.GetAllAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Photo>> GetPhotoById(int id)
        {
            var photo = await dataRepository.GetByIdAsync(id);

            if (photo == null || photo.Value == null)  //marche pas sinon 
            {
                return NotFound();
            }

            return photo;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPhoto(int id, Photo objt) {
            if (id != objt.PhotoID){
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
        public async Task<ActionResult<Photo>> PostPhoto(Photo obj) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            await dataRepository.AddAsync(obj);

            return CreatedAtAction("GetPhotoById", new { id = obj.PhotoID }, obj);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhoto(int id) {
            var obj = await dataRepository.GetByIdAsync(id);
            if (obj == null) {
                return NotFound();
            }
            await dataRepository.DeleteAsync(obj.Value);
            return Ok(obj);
        }
    }
}
