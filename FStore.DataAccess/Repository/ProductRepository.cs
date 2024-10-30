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
    public class ProductRepository : IProductRepository
    {
        public bool AddProduct(Product product)
        {
            return ProductDAO.Instance.AddProduct(product);
        }

        public Product GetProductById(int id)
        {
            return ProductDAO.Instance.GetProductById(id);
        }

        public List<Product> GetProducts()
        {
            return ProductDAO.Instance.GetProducts();
        }

        public bool RemoveProduct(int id)
        {
            return ProductDAO.Instance.RemoveProduct(id);
        }

        public bool UpdateProduct(Product product)
        {
            return ProductDAO.Instance.UpdateProduct(product);
        }
    }
}
