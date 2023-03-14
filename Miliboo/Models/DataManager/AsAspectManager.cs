using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Miliboo.Models.EntityFramework;
using Miliboo.Models.Repository;

namespace Miliboo.Models.DataManager
{
    public class AsAspectManager : IDataRepository<AsAspect>
    {
        readonly MilibooDBContext? milibooDBContext;
        public AsAspectManager() { }
        public AsAspectManager(MilibooDBContext context)
        {
            milibooDBContext = context;
        }
        public async Task<ActionResult<IEnumerable<AsAspect>>> GetAllAsync()
        {
            return await milibooDBContext.AsAspect.ToListAsync();
        }
        public async Task<ActionResult<AsAspect>> GetByIdAsync(int id)
        {
            return await milibooDBContext.AsAspect.FindAsync(id);
        }
        public async Task AddAsync(AsAspect entity)
        {
            await milibooDBContext.AsAspect.AddAsync(entity);
            await milibooDBContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(AsAspect asAspect, AsAspect entity)
        {
            milibooDBContext.Entry(asAspect).State = EntityState.Modified;
            asAspect.ProductTypeId = entity.ProductTypeId;
            asAspect.TechnicalAspectId = entity.TechnicalAspectId;
            asAspect.AspectDescription = entity.AspectDescription;
            await milibooDBContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(AsAspect asAspect)
        {
            milibooDBContext.AsAspect.Remove(asAspect);
            await milibooDBContext.SaveChangesAsync();
        }

        public Task<ActionResult<AsAspect>> GetByStringAsync(string str)
        {
            throw new NotImplementedException();
        }
    }
}
