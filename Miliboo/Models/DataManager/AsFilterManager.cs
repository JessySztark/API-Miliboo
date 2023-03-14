using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Miliboo.Models.EntityFramework;
using Miliboo.Models.Repository;

namespace Miliboo.Models.DataManager
{
    public class AsFilterManager : IDataRepository<AsFilter>
    {
        readonly MilibooDBContext? milibooDBContext;
        public AsFilterManager() { }
        public AsFilterManager(MilibooDBContext context)
        {
            milibooDBContext = context;
        }
        public async Task<ActionResult<IEnumerable<AsFilter>>> GetAllAsync()
        {
            return await milibooDBContext.AsFilter.ToListAsync();
        }
        public async Task<ActionResult<AsFilter>> GetByIdAsync(int id)
        {
            return await milibooDBContext.AsFilter.FindAsync(id);
        }

        public async Task AddAsync(AsFilter entity)
        {
            await milibooDBContext.AsFilter.AddAsync(entity);
            await milibooDBContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(AsFilter asFilter, AsFilter entity)
        {
            milibooDBContext.Entry(asFilter).State = EntityState.Modified;
            asFilter.FilterCategoryId = entity.FilterCategoryId;
            asFilter.ProductCategoryId = entity.ProductCategoryId;
            await milibooDBContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(AsFilter asFilter)
        {
            milibooDBContext.AsFilter.Remove(asFilter);
            await milibooDBContext.SaveChangesAsync();
        }

        public Task<ActionResult<AsFilter>> GetByStringAsync(string str)
        {
            throw new NotImplementedException();
        }
    }
}
