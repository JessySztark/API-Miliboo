using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Miliboo.Models.EntityFramework;
using Miliboo.Models.Repository;

namespace MilibooAPI.Models.DataManager
{
    public class ColorManager : IDataRepository<Color>
    {

        readonly MilibooDBContext? milibooDbContext;
        public ColorManager() { }
        public ColorManager(MilibooDBContext context)
        {
            milibooDbContext = context;
        }

        public Task AddAsync(Color entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Color entity)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<IEnumerable<Color>>> GetAllAsync()
        {
            return await milibooDbContext.Colors.ToListAsync();

        }

        public async Task<ActionResult<Color>> GetByIdAsync(int id)
        {
            return await milibooDbContext.Colors.FindAsync(id);

        }

        public Task<ActionResult<Color>> GetByStringAsync(string str)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Color entityToUpdate, Color entity)
        {
            throw new NotImplementedException();
        }
    }
}
