using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Miliboo.Models.EntityFramework;
using Miliboo.Models.Repository;

namespace MilibooAPI.Models.DataManager
{
    public class RegroupManager : IDataRepository<Regroup>
    {
        readonly MilibooDBContext? milibooDbContext;
        public RegroupManager() { }
        public RegroupManager(MilibooDBContext context)
        {
            milibooDbContext = context;
        }

        public Task AddAsync(Regroup entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Regroup entity)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<IEnumerable<Regroup>>> GetAllAsync()
        {
            return await milibooDbContext.Regroups.ToListAsync();
        }

        public async Task<ActionResult<Regroup>> GetByIdAsync(int id)
        {
            return await milibooDbContext.Regroups.FindAsync(id);
        }

        public Task<ActionResult<Regroup>> GetByStringAsync(string str)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Regroup entityToUpdate, Regroup entity)
        {
            throw new NotImplementedException();
        }
    }
}
