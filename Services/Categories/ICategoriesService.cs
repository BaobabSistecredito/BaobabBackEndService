using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaobabBackEndSerice.Models;
using BaobabBackEndService.Utils;
using Microsoft.AspNetCore.Mvc;

namespace BaobabBackEndService.Services.categories
{
    public interface ICategoriesServices
    {
        ResponseUtils<Category> GetAllCategories();
        Category GetCategory(string id);
        Task<ResponseUtils<Category>> UpdateCategory(string id, CategoryRequest category);
        // -------------------------- SEARCH FUNCTION:
        Task<ResponseUtils<Category>> SearchCategory(string category);
        // -------------------------------------------
        Task<ResponseUtils<Category>> CreateCategoria(Category category);
        Task<ResponseUtils<Category>> GetCategoriesAsync(string number);
        Task<bool> ValidateCategoryStatusChange(int categoryid);
    }
}