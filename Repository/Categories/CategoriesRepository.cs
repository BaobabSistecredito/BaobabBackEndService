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


        //crear categoria
        public Category CreateCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return category;
        }

        //Validar si el nombre de la categoria existe en la base de datos
        public async Task<Category>GetCategoryByNameAsync(string name)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.CategoryName == name);
        }

    }
}