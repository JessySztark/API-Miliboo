using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Miliboo.Models.EntityFramework;
using Miliboo.Models.Repository;

namespace MilibooAPI.Models.DataManager
{
    public class DeliveryMethodManager : IDataRepository<DeliveryMethod>
    {
        readonly MilibooDBContext? milibooDbContext;
        public DeliveryMethodManager() { }
        public DeliveryMethodManager(MilibooDBContext context)
        {
            milibooDbContext = context;
        }

        public Task AddAsync(DeliveryMethod entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(DeliveryMethod entity)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<IEnumerable<DeliveryMethod>>> GetAllAsync()
        {
            return await milibooDbContext.DeliveryMethods.ToListAsync();
        }

        public async Task<ActionResult<DeliveryMethod>> GetByIdAsync(int id)
        {
            return await milibooDbContext.DeliveryMethods.FindAsync(id);
        }

        public Task<ActionResult<DeliveryMethod>> GetByStringAsync(string str)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(DeliveryMethod entityToUpdate, DeliveryMethod entity)
        {
            throw new NotImplementedException();
        }
    }
}
