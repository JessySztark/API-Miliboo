using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Miliboo.Models.EntityFramework;
using Miliboo.Models.Repository;

namespace Miliboo.Models.DataManager {
    public class FilterCategoryManager : IDataRepository<FilterCategory> {
        readonly MilibooDBContext? milibooDBContext;
        public FilterCategoryManager() { }
        public FilterCategoryManager(MilibooDBContext context) {
            milibooDBContext = context;
        }
        public async Task<ActionResult<IEnumerable<FilterCategory>>> GetAllAsync() {
            return await milibooDBContext.FilterCategory.ToListAsync();
        }
        public async Task<ActionResult<FilterCategory>> GetByIdAsync(int id) {
            return await milibooDBContext.FilterCategory.FindAsync(id);
        }

        public async Task AddAsync(FilterCategory entity) {
            await milibooDBContext.FilterCategory.AddAsync(entity);
            await milibooDBContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(FilterCategory filterCategory, FilterCategory entity) {
            milibooDBContext.Entry(filterCategory).State = EntityState.Modified;
            filterCategory.FilterCategoryId = entity.FilterCategoryId;
            filterCategory.FilterCategoryName = entity.FilterCategoryName;
            await milibooDBContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(FilterCategory filterCategory) {
            milibooDBContext.FilterCategory.Remove(filterCategory);
            await milibooDBContext.SaveChangesAsync();
        }

        public Task<ActionResult<FilterCategory>> GetByStringAsync(string str) {
            throw new NotImplementedException();
        }
    }
}
