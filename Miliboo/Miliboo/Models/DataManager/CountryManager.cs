using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Miliboo.Models.EntityFramework;
using Miliboo.Models.Repository;

namespace MilibooAPI.Models.DataManager
{
    public class CountryManager : IDataRepository<Country>
    {
        readonly MilibooDBContext? milibooDbContext;
        public CountryManager() { }
        public CountryManager(MilibooDBContext context)
        {
            milibooDbContext = context;
        }

        public Task AddAsync(Country entity) {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Country entity) {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<IEnumerable<Country>>> GetAllAsync()
        {
            return await milibooDbContext.Countries.ToListAsync();
        }

        public async Task<ActionResult<Country>> GetByIdAsync(int id)
        {
            return await milibooDbContext.Countries.FindAsync(id);
        }

        public Task<ActionResult<Country>> GetByStringAsync(string str) {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Country entityToUpdate, Country entity) {
            throw new NotImplementedException();
        }
    }
}
