using FStore.BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace FStore.DataAccess.DAO
{
    public class ProductDAO
    {
        private static ProductDAO instance = null;
        public static ProductDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ProductDAO();
                }
                return instance;
            }
        }

        private ProductDAO() { }

        public Product GetProductById(int productId)
        {
            using (var appDbContext = new AppDbContext())
            {
                return appDbContext.Products.SingleOrDefault(p => p.ProductId == productId);
            }
        }

        public List<Product> GetProducts()
        {
            using (var appDbContext = new AppDbContext())
            {
                return appDbContext.Products.ToList();
            }
        }

        public bool AddProduct(Product product)
        {
            bool isSuccess = false;
            try
            {
                using (var appDbContext = new AppDbContext())
                {
                    appDbContext.Products.Add(product);
                    appDbContext.SaveChanges();
                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error adding product: " + ex.Message);
            }
            return isSuccess;
        }

        public bool UpdateProduct(Product product)
        {
            bool isSuccess = false;
            try
            {
                using (var appDbContext = new AppDbContext())
                {
                    var productToUpdate = appDbContext.Products.Find(product.ProductId);
                    if (productToUpdate != null)
                    {
                        appDbContext.Update(product);
                        appDbContext.SaveChanges();
                        appDbContext.Entry(product).State = EntityState.Detached;
                        isSuccess = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error updating product: " + ex.Message);
            }
            return isSuccess;
        }

        public bool RemoveProduct(int productId)
        {
            bool isSuccess = false;
            try
            {
                using (var appDbContext = new AppDbContext())
                {
                    var product = appDbContext.Products.Find(productId);
                    if (product != null)
                    {
                        appDbContext.Products.Remove(product);
                        appDbContext.SaveChanges();
                        isSuccess = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error removing product: " + ex.Message);
            }
            return isSuccess;
        }
    }
}
