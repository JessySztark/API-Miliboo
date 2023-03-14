using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Miliboo.Models.EntityFramework;
using Miliboo.Models.Repository;

namespace Miliboo.Models.DataManager
{
    public class AddressesManager : IDataRepository<Address>
    {
        readonly MilibooDBContext? milibooDBContext;
        public AddressesManager() { }
        public AddressesManager(MilibooDBContext context)
        {
            milibooDBContext = context;
        }
        public async Task<ActionResult<IEnumerable<Address>>> GetAllAsync()
        {
            return await milibooDBContext.Address.ToListAsync();
        }
        public async Task<ActionResult<Address>> GetByIdAsync(int id)
        {
            return await milibooDBContext.Address.FindAsync(id);
        }
        public async Task<ActionResult<Address>> GetByStringAsync(string postalcode)
        {
            return await milibooDBContext.Address.FirstOrDefaultAsync(u => u.PostalCode.ToUpper() == postalcode.ToUpper());
        }
        public async Task AddAsync(Address entity)
        {
            await milibooDBContext.Address.AddAsync(entity);
            await milibooDBContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Address Address, Address entity)
        {
            milibooDBContext.Entry(Address).State = EntityState.Modified;
            Address.AddressID = entity.AddressID;
            Address.Wording = entity.Wording;
            Address.PostalCode = entity.PostalCode;
            Address.City = entity.City;
            Address.Longitude = entity.Longitude;
            Address.Latitude = entity.Latitude;
            Address.CountryID = entity.CountryID;
            await milibooDBContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Address Address)
        {
            milibooDBContext.Address.Remove(Address);
            await milibooDBContext.SaveChangesAsync();
        }
    }
}
