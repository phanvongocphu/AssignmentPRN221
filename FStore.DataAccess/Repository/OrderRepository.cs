using FStore.BusinessObject;
using FStore.DataAccess.DAO;
using FStore.DataAccess.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FStore.DataAccess.Repository
{
    public class OrderRepository : IOrderRepository
    {
        public bool AddOrder(Order order)
        {
            return OrderDAO.Instance.AddOrder(order);
        }

        public Order GetOrderById(int id)
        {
            return OrderDAO.Instance.GetOrderById(id);
        }
        public List<Order> GetOrderByMemberId(int memberId)
        {
            return OrderDAO.Instance.GetOrderByMemberId(memberId);
        }
        public List<Order> GetOrders()
        {
            return OrderDAO.Instance.GetOrders();
        }

        public bool RemoveOrder(int id)
        {
            return OrderDAO.Instance.RemoveOrder(id);
        }

        public bool UpdateOrder(Order order)
        {
            return OrderDAO.Instance.UpdateOrder(order);
        }
    }
}
