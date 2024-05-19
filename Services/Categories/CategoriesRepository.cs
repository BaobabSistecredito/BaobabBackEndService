using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaobabBackEndSerice.Data;
using BaobabBackEndSerice.Models;
using BaobabBackEndService.Utils;

namespace BaobabBackEndService.Services.Categories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly BaobabDataBaseContext _context;

        public CategoriesRepository(BaobabDataBaseContext context)
        {
            _context = context;
        }

        public IEnumerable<Category>  GetCategories()
        {
            return _context.Categories.ToList();
        }

        public Category  GetCategory(string id)
        {
            throw new NotImplementedException();
        }
    }
}