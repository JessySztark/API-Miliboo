using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Miliboo.Models.EntityFramework;
using Miliboo.Models.Repository;

namespace Miliboo.Models.DataManager
{
    public class CompositeProductManager : IDataRepository<CompositeProduct>
    {
        readonly MilibooDBContext? milibooDBContext;
        public CompositeProductManager() { }
        public CompositeProductManager(MilibooDBContext context)
        {
            milibooDBContext = context;
        }
        public async Task<ActionResult<IEnumerable<CompositeProduct>>> GetAllAsync()
        {
            return await milibooDBContext.CompositeProduct.ToListAsync();
        }
        public async Task<ActionResult<CompositeProduct>> GetByIdAsync(int id)
        {
            return await milibooDBContext.CompositeProduct.FindAsync(id);
        }

        public async Task AddAsync(CompositeProduct entity)
        {
            await milibooDBContext.CompositeProduct.AddAsync(entity);
            await milibooDBContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(CompositeProduct compositeProduct, CompositeProduct entity)
        {
            milibooDBContext.Entry(compositeProduct).State = EntityState.Modified;
            compositeProduct.CompositeID = entity.CompositeID;
            compositeProduct.ProductId = entity.ProductId;
            compositeProduct.CompositeproductID = entity.CompositeproductID;
            compositeProduct.CompositeDescription = entity.CompositeDescription;
            await milibooDBContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(CompositeProduct compositeProduct)
        {
            milibooDBContext.CompositeProduct.Remove(compositeProduct);
            await milibooDBContext.SaveChangesAsync();
        }

        public Task<ActionResult<CompositeProduct>> GetByStringAsync(string str)
        {
            throw new NotImplementedException();
        }
    }
}
