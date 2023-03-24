using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Miliboo.Models.EntityFramework;
using Miliboo.Models.Repository;

namespace Miliboo.Models.DataManager {
    public class DiscountManager : IDataRepository<Discount> {
        readonly MilibooDBContext? milibooDBContext;
        public DiscountManager() { }
        public DiscountManager(MilibooDBContext context) {
            milibooDBContext = context;
        }
        public async Task<ActionResult<IEnumerable<Discount>>> GetAllAsync() {
            return await milibooDBContext.Discount.ToListAsync();
        }
        public async Task<ActionResult<Discount>> GetByIdAsync(int id) {
            return await milibooDBContext.Discount.FindAsync(id);
        }

        public async Task AddAsync(Discount entity) {
            await milibooDBContext.Discount.AddAsync(entity);
            await milibooDBContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Discount discount, Discount entity) {
            milibooDBContext.Entry(discount).State = EntityState.Modified;
            discount.DiscountID = entity.DiscountID;
            discount.DiscountName = entity.DiscountName;
            discount.IsActive = entity.IsActive;
            discount.DiscountValue = entity.DiscountValue;
            await milibooDBContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Discount discount) {
            milibooDBContext.Discount.Remove(discount);
            await milibooDBContext.SaveChangesAsync();
        }

        public Task<ActionResult<Discount>> GetByStringAsync(string str) {
            throw new NotImplementedException();
        }
    }
}
