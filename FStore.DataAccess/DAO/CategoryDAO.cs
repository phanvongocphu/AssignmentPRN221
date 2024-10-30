using FStore.BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FStore.DataAccess.DAO
{
    public class CategoryDAO
    {
        private AppDbContext appDbContext;
        private static CategoryDAO instance = null;

        public static CategoryDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CategoryDAO();
                }
                return instance;
            }
        }

        public CategoryDAO()
        {
            appDbContext = new AppDbContext();
        }

        public Category GetCategoryById(int categoryId)
        {
            return appDbContext.Categories.SingleOrDefault(c => c.CategoryId == categoryId);
        }

        public List<Category> GetCategories()
        {
            return appDbContext.Categories.ToList();
        }

        public bool AddCategory(Category category)
        {
            bool isSuccess = false;
            try
            {
                appDbContext.Categories.Add(category);
                appDbContext.SaveChanges();
                isSuccess = true;
            }
            catch (Exception ex) { }
            return isSuccess;
        }

        public bool UpdateCategory(Category category)
        {
            bool isSuccess = false;
            try
            {
                Category categoryToUpdate = GetCategoryById(category.CategoryId);
                if (categoryToUpdate != null)
                {
                    appDbContext.Entry(categoryToUpdate).State = EntityState.Detached;
                    appDbContext.Update(category);
                    appDbContext.SaveChanges();
                    isSuccess = true;
                }
            }
            catch (Exception ex) { }
            return isSuccess;
        }

        public bool RemoveCategory(int categoryId)
        {
            bool isSuccess = false;
            try
            {
                Category category = GetCategoryById(categoryId);
                if (category != null)
                {
                    appDbContext.Entry(category).State = EntityState.Detached;
                    appDbContext.Categories.Remove(category);
                    appDbContext.SaveChanges();
                    isSuccess = true;
                }
            }
            catch (Exception ex) { }
            return isSuccess;
        }
    }
}
