using FStore.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FStore.DataAccess.IRepository
{
    public interface IOrderRepository
    {
        public Order GetOrderById(int id);
        public List<Order> GetOrderByMemberId(int memberId);
        public List<Order> GetOrders();
        public bool AddOrder(Order order);
        public bool UpdateOrder(Order order);
        public bool RemoveOrder(int id);
    }
}
