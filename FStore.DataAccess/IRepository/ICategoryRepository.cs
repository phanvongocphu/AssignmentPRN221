using FStore.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FStore.DataAccess.IRepository
{
    public interface ICategoryRepository
    {
        public Category GetCategoryById(int id);
        public List<Category> GetCategories();
        public bool AddCategory(Category category);
        public bool UpdateCategory(Category category);
        public bool RemoveCategory(int id);
    }
}
