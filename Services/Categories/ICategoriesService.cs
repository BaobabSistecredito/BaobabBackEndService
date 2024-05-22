using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaobabBackEndSerice.Models;
using BaobabBackEndService.Utils;

namespace BaobabBackEndService.Services.categories
{
    public interface ICategoriesServices
    {
        IEnumerable<Category> GetCategories();

        Category GetCategory(string id);

        Task<ResponseUtils<Category>> UpdateCategory(string id, Category category);
        // -------------------------- SEARCH FUNCTION:
        Task<ResponseUtils<Category>> SearchCategory(string category);
        // -------------------------------------------
        

    }
}