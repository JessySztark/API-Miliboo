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

        // PUT: api/Photos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /* [HttpPut("{id}")]
         public async Task<IActionResult> PutPhoto(int id, Photo photo)
         {
             if (id != photo.PhotoID)
             {
                 return BadRequest();
             }

             _context.Entry(photo).State = EntityState.Modified;

             try
             {
                 await _context.SaveChangesAsync();
             }
             catch (DbUpdateConcurrencyException)
             {
                 if (!PhotoExists(id))
                 {
                     return NotFound();
                 }
                 else
                 {
                     throw;
                 }
             }

             return NoContent();
         }

         // POST: api/Photos
         // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
         [HttpPost]
         public async Task<ActionResult<Photo>> PostPhoto(Photo photo)
         {
             _context.Photos.Add(photo);
             await _context.SaveChangesAsync();

             return CreatedAtAction("GetPhoto", new { id = photo.PhotoID }, photo);
         }

         // DELETE: api/Photos/5
         [HttpDelete("{id}")]
         public async Task<IActionResult> DeletePhoto(int id)
         {
             var photo = await _context.Photos.FindAsync(id);
             if (photo == null)
             {
                 return NotFound();
             }

             _context.Photos.Remove(photo);
             await _context.SaveChangesAsync();

             return NoContent();
         }

         private bool PhotoExists(int id)
         {
             return _context.Photos.Any(e => e.PhotoID == id);
         }*/
    }
}
