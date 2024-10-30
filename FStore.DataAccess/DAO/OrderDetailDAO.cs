using FStore.BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FStore.DataAccess.DAO
{
    public class OrderDetailDAO
    {
        private AppDbContext appDbContext;
        private static OrderDetailDAO instance = null;

        public static OrderDetailDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OrderDetailDAO();
                }
                return instance;
            }
        }

        public OrderDetailDAO()
        {
            appDbContext = new AppDbContext();
        }

        public OrderDetail GetOrderDetailById(int orderId, int productId)
        {
            return appDbContext.OrderDetails
                .SingleOrDefault(od => od.OrderId == orderId && od.ProductId == productId);
        }

        public List<OrderDetail> GetOrderDetails()
        {
            return appDbContext.OrderDetails.ToList();
        }
        public List<OrderDetail> GetOrderDetails(int orderId)
        {
            using (var context = new AppDbContext())
            {
                return context.OrderDetails
                              .Include(od => od.Product)
                              .Where(od => od.OrderId == orderId)
                              .ToList();
            }
        }

        public bool AddOrderDetail(OrderDetail orderDetail)
        {
            bool isSuccess = false;
            try
            {
                appDbContext.OrderDetails.Add(orderDetail);
                appDbContext.SaveChanges();
                isSuccess = true;
            }
            catch (Exception ex) { }
            return isSuccess;
        }

        public bool UpdateOrderDetail(OrderDetail orderDetail)
        {
            bool isSuccess = false;
            try
            {
                OrderDetail orderDetailToUpdate = GetOrderDetailById(orderDetail.OrderId, orderDetail.ProductId);
                if (orderDetailToUpdate != null)
                {
                    appDbContext.Entry(orderDetailToUpdate).State = EntityState.Detached;
                    appDbContext.Update(orderDetail);
                    appDbContext.SaveChanges();
                    isSuccess = true;
                }
            }
            catch (Exception ex) { }
            return isSuccess;
        }

        public bool RemoveOrderDetail(int orderId, int productId)
        {
            bool isSuccess = false;
            try
            {
                OrderDetail orderDetail = GetOrderDetailById(orderId, productId);
                if (orderDetail != null)
                {
                    appDbContext.Entry(orderDetail).State = EntityState.Detached;
                    appDbContext.OrderDetails.Remove(orderDetail);
                    appDbContext.SaveChanges();
                    isSuccess = true;
                }
            }
            catch (Exception ex) { }
            return isSuccess;
        }
    }
}
