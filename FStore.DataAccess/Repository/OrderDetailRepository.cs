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
    public class OrderDetailRepository : IOrderDetailRepository
    {
        public bool AddOrderDetail(OrderDetail orderDetail)
        {
            return OrderDetailDAO.Instance.AddOrderDetail(orderDetail);
        }

        public OrderDetail GetOrderDetailById(int orderId, int productId)
        {
            return OrderDetailDAO.Instance.GetOrderDetailById(orderId, productId);
        }

        public List<OrderDetail> GetOrderDetails()
        {
            return OrderDetailDAO.Instance.GetOrderDetails();
        }
        public List<OrderDetail> GetOrderDetails(int orderId)
        {
            return OrderDetailDAO.Instance.GetOrderDetails(orderId);
        }

        public bool RemoveOrderDetail(int orderId, int productId)
        {
            return OrderDetailDAO.Instance.RemoveOrderDetail(orderId, productId);
        }

        public bool UpdateOrderDetail(OrderDetail orderDetail)
        {
            return OrderDetailDAO.Instance.UpdateOrderDetail(orderDetail);
        }
    }
}
