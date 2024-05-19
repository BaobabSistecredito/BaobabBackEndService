using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaobabBackEndSerice.Models;

namespace BaobabBackEndService.BusinessLogic
{
    public interface ICategoriesService
    {
        IEnumerable<Category> GetCategories();

        Category GetCategory(string id);
    }
}