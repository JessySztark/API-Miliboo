using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Miliboo.Models.EntityFramework;
using Miliboo.Models.Repository;

namespace Miliboo.Models.DataManager {
    public class ProductTypeManager : IDataRepository<ProductType> {
        readonly MilibooDBContext? milibooDBContext;
        public ProductTypeManager() { }
        public ProductTypeManager(MilibooDBContext context) {
            milibooDBContext = context;
        }
        public async Task<ActionResult<IEnumerable<ProductType>>> GetAllAsync() {
            return await milibooDBContext.ProductType.ToListAsync();
        }
        public async Task<ActionResult<ProductType>> GetByIdAsync(int id) {
            return await milibooDBContext.ProductType.FindAsync(id);
        }
        public async Task<ActionResult<ProductType>> GetByStringAsync(string str) {
            return await milibooDBContext.ProductType.FirstOrDefaultAsync(o => o.ProductTypeName.ToUpper() == str.ToUpper());
        }
        public async Task AddAsync(ProductType entity) {
            await milibooDBContext.ProductType.AddAsync(entity);
            await milibooDBContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(ProductType productType, ProductType entity) {
            milibooDBContext.Entry(productType).State = EntityState.Modified;
            productType.ProductTypeId = entity.ProductTypeId;
            productType.ProductTypeName = entity.ProductTypeName;
            productType.PTMaintenanceComment = entity.PTMaintenanceComment;
            await milibooDBContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(ProductType productType) {
            milibooDBContext.ProductType.Remove(productType);
            await milibooDBContext.SaveChangesAsync();
        }
    }
}
