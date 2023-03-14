using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Miliboo.Models.EntityFramework;
using Miliboo.Models.Repository;

namespace Miliboo.Models.DataManager {
    public class FilterManager : IDataRepository<Filter> {
        readonly MilibooDBContext? milibooDBContext;
        public FilterManager() { }
        public FilterManager(MilibooDBContext context) {
            milibooDBContext = context;
        }
        public async Task<ActionResult<IEnumerable<Filter>>> GetAllAsync() {
            return await milibooDBContext.Filter.ToListAsync();
        }
        public async Task<ActionResult<Filter>> GetByIdAsync(int id) {
            return await milibooDBContext.Filter.FindAsync(id);
        }

        public async Task AddAsync(Filter entity) {
            await milibooDBContext.Filter.AddAsync(entity);
            await milibooDBContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Filter filter, Filter entity) {
            milibooDBContext.Entry(filter).State = EntityState.Modified;
            filter.FilterId = entity.FilterId;
            filter.FilterName = entity.FilterName;
            await milibooDBContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Filter filter) {
            milibooDBContext.Filter.Remove(filter);
            await milibooDBContext.SaveChangesAsync();
        }

        public Task<ActionResult<Filter>> GetByStringAsync(string str) {
            throw new NotImplementedException();
        }
    }
}
