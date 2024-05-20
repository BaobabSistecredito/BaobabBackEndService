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

        Task UpdateCategoryAsync(Category category);
        Task<Category> GetCategoryByIdAsync(int id);
        //ResponseUtils<Category> AddCategory(Category category);

        //ResponseUtils<Category> UpdateCategory(Category category);

        //ResponseUtils<Category> DeleteCategory(string id); // en este caso no se elimina la categoria solo se cambia de estado


    }

}