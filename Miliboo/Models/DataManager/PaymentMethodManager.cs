using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Miliboo.Models.EntityFramework;
using Miliboo.Models.Repository;

namespace MilibooAPI.Models.DataManager
{
    public class PaymentMethodManager : IDataRepository<PaymentMethod>
    {

        readonly MilibooDBContext? milibooDbContext;
        public PaymentMethodManager() { }
        public PaymentMethodManager(MilibooDBContext context)
        {
            milibooDbContext = context;
        }

        public Task AddAsync(PaymentMethod entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(PaymentMethod entity)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<IEnumerable<PaymentMethod>>> GetAllAsync()
        {
            return await milibooDbContext.PaymentMethods.ToListAsync();
        }

        public async Task<ActionResult<PaymentMethod>> GetByIdAsync(int id)
        {
            return await milibooDbContext.PaymentMethods.FindAsync(id);
        }

        public Task<ActionResult<PaymentMethod>> GetByStringAsync(string str)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(PaymentMethod entityToUpdate, PaymentMethod entity)
        {
            throw new NotImplementedException();
        }
    }
}
