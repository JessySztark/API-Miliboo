using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Miliboo.Models.EntityFramework;
using Miliboo.Models.Repository;
using System.Data.Entity;
using EntityState = Microsoft.EntityFrameworkCore.EntityState;

namespace Miliboo.Models.DataManager
{
    public class ProductCategoryManager : IDataRepository<ProductCategory>
    {
        readonly MilibooDBContext? milibooDBContext;
        public ProductCategoryManager() { }
        public ProductCategoryManager(MilibooDBContext context)
        {
            milibooDBContext = context;
        }
        public async Task<ActionResult<IEnumerable<ProductCategory>>> GetAllAsync()
        {
            return await milibooDBContext.ProductCategory.ToListAsync();
        }
        public async Task<ActionResult<ProductCategory>> GetByIdAsync(int id)
        {
            return await milibooDBContext.ProductCategory.FindAsync(id);
        }
        public async Task<ActionResult<ProductCategory>> GetByStringAsync(string str)
        {
            return await milibooDBContext.ProductCategory.FirstOrDefaultAsync(p => p.ProductCategoryName.ToUpper() == str.ToUpper());
        }

        public async Task AddAsync(ProductCategory entity)
        {
            await milibooDBContext.ProductCategory.AddAsync(entity);
            await milibooDBContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductCategory productCategory, ProductCategory entity)
        {
            milibooDBContext.Entry(productCategory).State = EntityState.Modified;
            productCategory.ProductCategoryId = entity.ProductCategoryId;
            productCategory.ProductCategoryName = entity.ProductCategoryName;
            productCategory.ParentCategoryId = entity.ParentCategoryId;
            await milibooDBContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(ProductCategory ProductCategory)
        {
            milibooDBContext.ProductCategory.Remove(ProductCategory);
            await milibooDBContext.SaveChangesAsync();
        }
    }
}
