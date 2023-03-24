using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Miliboo.Models.EntityFramework;
using Miliboo.Models.Repository;

namespace MilibooAPI.Models.DataManager
{
    public class ProductManager : IDataRepository<Product>
    {

        readonly MilibooDBContext? milibooDbContext;
        public ProductManager() { }
        public ProductManager(MilibooDBContext context)
        {
            milibooDbContext = context;
        }

        public Task AddAsync(Product entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Product entity)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<IEnumerable<Product>>> GetAllAsync()
        {
            return await milibooDbContext.Product.ToListAsync();
        }

        public async Task<ActionResult<Product>> GetByIdAsync(int id)
        {
            return await milibooDbContext.Product.FindAsync(id);
        }

        public async Task<ActionResult<Product>> GetByStringAsync(string productName)
        {
            return await milibooDbContext.Product.FirstOrDefaultAsync(u => u.ProductName.ToUpper() == productName.ToUpper());
        }

        public Task UpdateAsync(Product entityToUpdate, Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
