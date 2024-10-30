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
    public class CategoryRepository : ICategoryRepository
    {
        public bool AddCategory(Category category)
        {
            return CategoryDAO.Instance.AddCategory(category);
        }

        public List<Category> GetCategories()
        {
            return CategoryDAO.Instance.GetCategories();
        }

        public Category GetCategoryById(int id)
        {
            return CategoryDAO.Instance.GetCategoryById(id);
        }

        public bool RemoveCategory(int id)
        {
            return CategoryDAO.Instance.RemoveCategory(id);
        }

        public bool UpdateCategory(Category category)
        {
            return CategoryDAO.Instance.UpdateCategory(category);
        }
    }
}
