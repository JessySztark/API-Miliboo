using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Miliboo.Models.EntityFramework;
using Miliboo.Models.Repository;

namespace MilibooAPI.Models.DataManager
{
    public class CountryManager : IDataRepository<CreditCard>
    {
        readonly MilibooDBContext? milibooDbContext;
        public CountryManager() { }
        public CountryManager(MilibooDBContext context)
        {
            milibooDbContext = context;
        }

        public Task AddAsync(CreditCard entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(CreditCard entity)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<IEnumerable<CreditCard>>> GetAllAsync()
        {
            return await milibooDbContext.CreditCards.ToListAsync();
        }

        public async Task<ActionResult<CreditCard>> GetByIdAsync(int id)
        {
            return await milibooDbContext.CreditCards.FindAsync(id);
        }

        public Task<ActionResult<CreditCard>> GetByStringAsync(string str)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(CreditCard entityToUpdate, CreditCard entity)
        {
            throw new NotImplementedException();
        }
    }
}
