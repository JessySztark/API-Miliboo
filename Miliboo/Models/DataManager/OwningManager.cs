using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Miliboo.Models.EntityFramework;
using Miliboo.Models.Repository;
using System.Data.Entity;
using EntityState = Microsoft.EntityFrameworkCore.EntityState;

namespace Miliboo.Models.DataManager {
    public class OwningManager : IDataRepository<Owning> {
        readonly MilibooDBContext? milibooDBContext;
        public OwningManager() { }
        public OwningManager(MilibooDBContext context) {
            milibooDBContext = context;
        }
        public async Task<ActionResult<IEnumerable<Owning>>> GetAllAsync() {
            return await milibooDBContext.Owning.ToListAsync();
        }
        public async Task<ActionResult<Owning>> GetByIdAsync(int id) {
            return await milibooDBContext.Owning.FindAsync(id);
        }

        public async Task AddAsync(Owning entity) {
            await milibooDBContext.Owning.AddAsync(entity);
            await milibooDBContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Owning owning, Owning entity) {
            milibooDBContext.Entry(owning).State = EntityState.Modified;
            owning.AddressID = entity.AddressID;
            owning.AccountID = entity.AccountID;
            await milibooDBContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Owning Owning) {
            milibooDBContext.Owning.Remove(Owning);
            await milibooDBContext.SaveChangesAsync();
        }

        public Task<ActionResult<Owning>> GetByStringAsync(string str) {
            throw new NotImplementedException();
        }
    }
}
