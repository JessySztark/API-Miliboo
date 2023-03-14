using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Miliboo.Models.EntityFramework;
using Miliboo.Models.Repository;

namespace MilibooAPI.Models.DataManager
{
    public class StateOrderManager : IDataRepository<StateOrder>
    {

        readonly MilibooDBContext? milibooDbContext;
        public StateOrderManager() { }
        public StateOrderManager(MilibooDBContext context)
        {
            milibooDbContext = context;
        }

        public Task AddAsync(StateOrder entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(StateOrder entity)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<IEnumerable<StateOrder>>> GetAllAsync()
        {
            return await milibooDbContext.StateOrders.ToListAsync();
        }

        public async Task<ActionResult<StateOrder>> GetByIdAsync(int id)
        {
            return await milibooDbContext.StateOrders.FindAsync(id);
        }

        public Task<ActionResult<StateOrder>> GetByStringAsync(string str)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(StateOrder entityToUpdate, StateOrder entity)
        {
            throw new NotImplementedException();
        }
    }
}
