using FStore.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FStore.DataAccess.IRepository
{
    public interface IProductRepository
    {
        public Product GetProductById(int id);
        public List<Product> GetProducts();
        public bool AddProduct(Product product);
        public bool UpdateProduct(Product product);
        public bool RemoveProduct(int id);
    }
}
