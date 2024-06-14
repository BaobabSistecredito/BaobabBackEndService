using BaobabBackEndSerice.Models;
using BaobabBackEndService.DTOs;
using BaobabBackEndService.Utils;

namespace BaobabBackEndService.Services.categories
{
    public interface ICategoriesServices
    {
        ResponseUtils<Category> GetAllCategories();
        Category GetCategory(string id);
        Task<ResponseUtils<Category>> UpdateCategory(string id, CategoryDTO category);
        // -------------------------- SEARCH FUNCTION:
        Task<ResponseUtils<Category>> SearchCategory(string category);
        // -------------------------------------------
        Task<ResponseUtils<Category>> CreateCategoria(CategoryDTO category);
        Task<ResponseUtils<Category>> GetCategoriesAsync(string number);
        Task<bool> ValidateCategoryStatusChange(int categoryid);
    }
}