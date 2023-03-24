using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Miliboo.Models.EntityFramework;
using Miliboo.Models.Repository;
using EntityState = Microsoft.EntityFrameworkCore.EntityState;

namespace Miliboo.Models.DataManager {
    public class TechnicalAspectManager : IDataRepository<TechnicalAspect> {
        readonly MilibooDBContext? milibooDBContext;
        public TechnicalAspectManager() { }
        public TechnicalAspectManager(MilibooDBContext context) {
            milibooDBContext = context;
        }
        public async Task<ActionResult<IEnumerable<TechnicalAspect>>> GetAllAsync() {
            return await milibooDBContext.TechnicalAspect.ToListAsync();
        }
        public async Task<ActionResult<TechnicalAspect>> GetByIdAsync(int id) {
            return await milibooDBContext.TechnicalAspect.FindAsync(id);
        }

        public async Task<ActionResult<TechnicalAspect>> GetByStringAsync(string str) {
            return await milibooDBContext.TechnicalAspect.FirstOrDefaultAsync(o => o.TechnicalAspectName.ToUpper() == str.ToUpper());
        }

        public async Task AddAsync(TechnicalAspect entity) {
            await milibooDBContext.TechnicalAspect.AddAsync(entity);
            await milibooDBContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(TechnicalAspect technicalAspect, TechnicalAspect entity) {
            milibooDBContext.Entry(technicalAspect).State = EntityState.Modified;
            technicalAspect.TechnicalAspectId = entity.TechnicalAspectId;
            technicalAspect.TechnicalAspectName = entity.TechnicalAspectName;
            await milibooDBContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(TechnicalAspect TechnicalAspect) {
            milibooDBContext.TechnicalAspect.Remove(TechnicalAspect);
            await milibooDBContext.SaveChangesAsync();
        }
    }
}
