using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Miliboo.Models.EntityFramework;
using Miliboo.Models.Repository;

namespace MilibooAPI.Models.DataManager
{
    public class AccountManager : IDataRepository<Account>
    {

        readonly MilibooDBContext? milibooDbContext;
        public AccountManager() { }
        public AccountManager(MilibooDBContext context)
        {
            milibooDbContext = context;
        }
        public async Task AddAsync(Account entity)
        {
            await milibooDbContext.Account.AddAsync(entity);
            await milibooDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Account account)
        {
            milibooDbContext.Account.Remove(account);
            await milibooDbContext.SaveChangesAsync();
        }

        public async Task<ActionResult<IEnumerable<Account>>> GetAllAsync()
        {
            return await milibooDbContext.Account.ToListAsync();

        }

        public async Task<ActionResult<Account>> GetByIdAsync(int id)
        {
            return await milibooDbContext.Account.FindAsync(id);

        }

        public async Task<ActionResult<Account>> GetByStringAsync(string mail)
        { 
            return await milibooDbContext.Account.FirstOrDefaultAsync(u => u.Mail.ToUpper() == mail.ToUpper());
        }

        public async Task UpdateAsync(Account account, Account entity)
        {
            milibooDbContext.Entry(account).State = EntityState.Modified;
            account.AccountID = entity.AccountID;
            account.Addresses = entity.Addresses;
            account.PhoneNumber = entity.PhoneNumber;
            account.Oath = entity.Oath;
            account.FirstName = entity.FirstName;
            account.LastName = entity.LastName;
            account.AccountComments = entity.AccountComments;
            account.CreditCardAccount = entity.CreditCardAccount;
            account.Mail = entity.Mail;
            account.Password = entity.Password;
            await milibooDbContext.SaveChangesAsync();

        }
    }
}
