using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Miliboo.Models.EntityFramework;
using Miliboo.Models.Repository;

namespace Miliboo.Models.DataManager
{
    public class ConcernedManager : IDataRepository<Concerned>
    {
        readonly MilibooDBContext? milibooDBContext;
        public ConcernedManager() { }
        public ConcernedManager(MilibooDBContext context)
        {
            milibooDBContext = context;
        }
        public async Task<ActionResult<IEnumerable<Concerned>>> GetAllAsync()
        {
            return await milibooDBContext.Concerned.ToListAsync();
        }
        public async Task<ActionResult<Concerned>> GetByIdAsync(int id)
        {
            return await milibooDBContext.Concerned.FindAsync(id);
        }
        public async Task AddAsync(Concerned entity)
        {
            await milibooDBContext.Concerned.AddAsync(entity);
            await milibooDBContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Concerned concerned, Concerned entity)
        {
            milibooDBContext.Entry(concerned).State = EntityState.Modified;
            concerned.ConcernedId = entity.ConcernedId;
            concerned.Quantity = entity.Quantity;
            await milibooDBContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Concerned concerned)
        {
            milibooDBContext.Concerned.Remove(concerned);
            await milibooDBContext.SaveChangesAsync();
        }

        public Task<ActionResult<Concerned>> GetByStringAsync(string str)
        {
            throw new NotImplementedException();
        }
    }
}
