using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Miliboo.Models.EntityFramework;
using Miliboo.Models.Repository;
using System.Data.Entity;
using EntityState = Microsoft.EntityFrameworkCore.EntityState;

namespace Miliboo.Models.DataManager
{
    public class OrderManager : IDataRepository<Order>
    {
        readonly MilibooDBContext? milibooDBContext;
        public OrderManager() { }
        public OrderManager(MilibooDBContext context)
        {
            milibooDBContext = context;
        }
        public async Task<ActionResult<IEnumerable<Order>>> GetAllAsync()
        {
            return await milibooDBContext.Order.ToListAsync();
        }
        public async Task<ActionResult<Order>> GetByIdAsync(int id)
        {
            return await milibooDBContext.Order.FindAsync(id);
        }
        public async Task<ActionResult<Order>> GetByStringAsync(string phone)
        {
            return await milibooDBContext.Order.FirstOrDefaultAsync(o => o.CellPhone.ToUpper() == phone.ToUpper());
        }

        public async Task AddAsync(Order entity)
        {
            await milibooDBContext.Order.AddAsync(entity);
            await milibooDBContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Order order, Order entity)
        {
            milibooDBContext.Entry(order).State = EntityState.Modified;
            order.OrderID = entity.OrderID;
            order.OrderName = entity.OrderName;
            order.OrderFirstName = entity.OrderFirstName;
            order.AccountID = entity.AccountID;
            order.CardID = entity.CardID;
            order.IdDeliveryMethod = entity.IdDeliveryMethod;
            order.IdDeliveryAdress = entity.IdDeliveryAdress;
            order.DiscountID = entity.DiscountID;
            order.StateOrderID = entity.StateOrderID;
            order.Paymentmethodid = entity.Paymentmethodid;
            order.PhoneOrder = entity.PhoneOrder;
            order.CellPhone = entity.CellPhone;
            order.Company = entity.Company;
            order.AdressAdditional = entity.AdressAdditional;
            order.OrderInstructions = entity.OrderInstructions;
            order.OrderDate = entity.OrderDate;
            order.DeliveryPrice = entity.DeliveryPrice;
            order.Sms = entity.Sms;
            await milibooDBContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Order Order)
        {
            milibooDBContext.Order.Remove(Order);
            await milibooDBContext.SaveChangesAsync();
        }
    }
}
