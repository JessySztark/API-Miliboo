using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Miliboo.Models.EntityFramework;
using Miliboo.Models.Repository;

namespace MilibooAPI.Models.DataManager
{
    public class PhotoManager : IDataRepository<Photo>
    {
        readonly MilibooDBContext? milibooDbContext;
        public PhotoManager() { }
        public PhotoManager(MilibooDBContext context)
        {
            milibooDbContext = context;
        }
        public Task AddAsync(Photo entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Photo entity)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<IEnumerable<Photo>>> GetAllAsync()
        {
            return await milibooDbContext.Photos.ToListAsync();
        }

        public async Task<ActionResult<Photo>> GetByIdAsync(int id)
        {
            return await milibooDbContext.Photos.FindAsync(id);
        }

        public Task<ActionResult<Photo>> GetByStringAsync(string str)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Photo entityToUpdate, Photo entity)
        {
            throw new NotImplementedException();
        }
    }
}
