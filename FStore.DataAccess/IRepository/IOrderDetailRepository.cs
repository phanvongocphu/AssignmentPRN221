using FStore.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FStore.DataAccess.IRepository
{
    public interface IOrderDetailRepository
    {
        public OrderDetail GetOrderDetailById(int orderId, int productId);
        public List<OrderDetail> GetOrderDetails();
        public List<OrderDetail> GetOrderDetails(int orderId);
        public bool AddOrderDetail(OrderDetail orderDetail);
        public bool UpdateOrderDetail(OrderDetail orderDetail);
        public bool RemoveOrderDetail(int orderId, int productId);
    }
}
