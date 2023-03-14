using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Miliboo.Models.EntityFramework;
using Miliboo.Models.Repository;

namespace MilibooAPI.Models.DataManager
{
    public class CommentManager : IDataRepository<Comment>
    {
        readonly MilibooDBContext? milibooDbContext;
        public CommentManager() { }
        public CommentManager(MilibooDBContext context)
        {
            milibooDbContext = context;
        }
        public async Task AddAsync(Comment comment)
        {
            await milibooDbContext.Comments.AddAsync(comment);
            await milibooDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Comment comment)
        {
            milibooDbContext.Comments.Remove(comment);
            await milibooDbContext.SaveChangesAsync();
        }

        public async Task<ActionResult<IEnumerable<Comment>>> GetAllAsync()
        {
            return await milibooDbContext.Comments.ToListAsync();

        }

        public async Task<ActionResult<Comment>> GetByIdAsync(int id)
        {
            return await milibooDbContext.Comments.FindAsync(id);

        }

        public Task<ActionResult<Comment>> GetByStringAsync(string str)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Comment entityToUpdate, Comment entity)
        {
            throw new NotImplementedException();
        }
    }
}
