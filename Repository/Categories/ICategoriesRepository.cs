using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaobabBackEndSerice.Models;
using BaobabBackEndService.Utils;

namespace BaobabBackEndService.Repository.Categories
{
    public interface ICategoriesRepository
    {
        IEnumerable<Category> GetCategories();
        Category GetCategory(string id);
        Category CreateCategory(Category category);
        Task UpdateCategoryAsync(Category category);
        Task<Category> GetCategoryByIdAsync(int id);
        Task<IEnumerable<Category>> GetAllCategoriesAsync(string category);
        Task<Category> GetCategoryByNameAsync(string Name);
        Task<IEnumerable<Category>> GetCategoriesAsync(string status);
    }
}