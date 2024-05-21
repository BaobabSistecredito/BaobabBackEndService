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

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }
        public async Task UpdateCategoryAsync(Category category)
        {
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        // ----------------------- SEARCH ACTION:
        public async Task <IEnumerable<Category>> GetAllCategoriesAsync(string? category)
        {
            // Se trae la información de la entidad 'Categories':
            var categories = await _context.Categories.ToListAsync();
            var categoriesFiltered = categories.Where(c => c.CategoryName.ToLower().Contains(category) || c.Status.ToLower().Contains(category));
            
            return categoriesFiltered;
        }
        // -----------------------------------------------
    }
}