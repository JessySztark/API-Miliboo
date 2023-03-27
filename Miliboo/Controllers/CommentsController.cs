using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
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
    public class CommentsController : ControllerBase
    {
        private readonly IDataRepository<Comment> dataRepository;
        public CommentsController(IDataRepository<Comment> dataRepo)
        {
            dataRepository = dataRepo;
        }

        // GET: api/Comments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> GetComments()
        {
            return await dataRepository.GetAllAsync();
        }

        // GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetCommentById(int id)
        {
            var comment = await dataRepository.GetByIdAsync(id);

            if (comment == null || comment.Value == null)  //marche pas sinon 
            {
                return NotFound();
            }

            return comment;
        }

        // PUT: api/Comments/5
       // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComment(int id, Comment comment) {
            if (id != comment.CommentID) {
                return BadRequest();
            }
            var commentToUpdate = await dataRepository.GetByIdAsync(id);
            if (commentToUpdate == null) {
                return NotFound();
            }
            else {
                await dataRepository.UpdateAsync(commentToUpdate.Value, comment);
                return Ok(comment);
            }
        }

        // POST: api/Comments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Comment>> PostComment(Comment comment)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            await dataRepository.AddAsync(comment);
            return CreatedAtAction("GetCommentById", new { id = comment.CommentID }, comment);
        }

        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await dataRepository.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            await dataRepository.DeleteAsync(comment.Value);
            return Ok(comment);
        }
    }
}
