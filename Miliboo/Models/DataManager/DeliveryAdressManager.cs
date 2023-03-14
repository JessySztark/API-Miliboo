using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Miliboo.Models.EntityFramework;
using Miliboo.Models.Repository;

namespace Miliboo.Models.DataManager {
    public class DeliveryAdressManager : IDataRepository<DeliveryAdress> {
        readonly MilibooDBContext? milibooDBContext;
        public DeliveryAdressManager() { }
        public DeliveryAdressManager(MilibooDBContext context) {
            milibooDBContext = context;
        }
        public async Task<ActionResult<IEnumerable<DeliveryAdress>>> GetAllAsync() {
            return await milibooDBContext.DeliveryAdress.ToListAsync();
        }
        public async Task<ActionResult<DeliveryAdress>> GetByIdAsync(int id) {
            return await milibooDBContext.DeliveryAdress.FindAsync(id);
        }
        public async Task<ActionResult<DeliveryAdress>> GetByStringAsync(string str) {
            return await milibooDBContext.DeliveryAdress.FirstOrDefaultAsync(a => a.FavAdressName.ToUpper() == str.ToUpper());
        }
        public async Task AddAsync(DeliveryAdress entity) {
            await milibooDBContext.DeliveryAdress.AddAsync(entity);
            await milibooDBContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(DeliveryAdress deliveryAdress, DeliveryAdress entity) {
            milibooDBContext.Entry(deliveryAdress).State = EntityState.Modified;
            deliveryAdress.IdDeliveryAdress = entity.IdDeliveryAdress;
            deliveryAdress.AccountID = entity.AccountID;
            deliveryAdress.FavAdressName = entity.FavAdressName;
            await milibooDBContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(DeliveryAdress DeliveryAdress) {
            milibooDBContext.DeliveryAdress.Remove(DeliveryAdress);
            await milibooDBContext.SaveChangesAsync();
        }
    }
}
