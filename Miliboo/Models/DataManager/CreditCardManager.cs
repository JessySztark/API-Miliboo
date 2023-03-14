using Microsoft.AspNetCore.Mvc;
using Miliboo.Models.EntityFramework;
using Miliboo.Models.Repository;

namespace MilibooAPI.Models.DataManager
{
    public class CreditCardManager : IDataRepository<Account>
    {

        readonly MilibooDBContext? milibooDbContext;
        public CreditCardManager() { }
        public CreditCardManager(MilibooDBContext context)
        {
            milibooDbContext = context;
        }

        public Task AddAsync(Account entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Account entity)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<IEnumerable<Account>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<Account>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<Account>> GetByStringAsync(string str)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Account entityToUpdate, Account entity)
        {
            throw new NotImplementedException();
        }
    }
}
