using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Miliboo.Models.EntityFramework;
using Miliboo.Models.Repository;

namespace MilibooAPI.Models.DataManager
{
    public class IsFilteredManager : IDataRepository<IsFiltered>
    {
        readonly MilibooDBContext? milibooDbContext;
        public IsFilteredManager() { }
        public IsFilteredManager(MilibooDBContext context)
        {
            milibooDbContext = context;
        }

        public Task AddAsync(IsFiltered entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(IsFiltered entity)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<IEnumerable<IsFiltered>>> GetAllAsync()
        {
            return await milibooDbContext.IsFiltereds.ToListAsync();
        }

        public async Task<ActionResult<IsFiltered>> GetByIdAsync(int id)
        {
            return await milibooDbContext.IsFiltereds.FindAsync(id);
        }

        public Task<ActionResult<IsFiltered>> GetByStringAsync(string str)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(IsFiltered entityToUpdate, IsFiltered entity)
        {
            throw new NotImplementedException();
        }
    }
}
