using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Miliboo.Models.EntityFramework;
using Miliboo.Models.Repository;

namespace MilibooAPI.Models.DataManager
{
    public class GroupingManager : IDataRepository<Grouping>
    {
        readonly MilibooDBContext? milibooDbContext;
        public GroupingManager() { }
        public GroupingManager(MilibooDBContext context)
        {
            milibooDbContext = context;
        }

        public Task AddAsync(Grouping entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Grouping entity)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<IEnumerable<Grouping>>> GetAllAsync()
        {
            return await milibooDbContext.Groupings.ToListAsync();
        }

        public async Task<ActionResult<Grouping>> GetByIdAsync(int id)
        {
            return await milibooDbContext.Groupings.FindAsync(id);
        }

        public Task<ActionResult<Grouping>> GetByStringAsync(string str)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Grouping entityToUpdate, Grouping entity)
        {
            throw new NotImplementedException();
        }
    }
}
