using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaobabBackEndSerice.Data;
using BaobabBackEndSerice.Models;
using BaobabBackEndService.Utils;
using Microsoft.EntityFrameworkCore;


namespace BaobabBackEndService.Repository.Categories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly BaobabDataBaseContext _context;

        public CategoriesRepository(BaobabDataBaseContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        public Category GetCategory(string id)
        {
            throw new NotImplementedException();
        }
        
        public async Task<IEnumerable<Category>> GetCategoriesAsync(string status)
        {
            return await _context.Categories.Where(c => c.Status == status).ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

    }
}

