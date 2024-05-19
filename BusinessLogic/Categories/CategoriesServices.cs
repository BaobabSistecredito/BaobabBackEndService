
using BaobabBackEndSerice.Models;
using BaobabBackEndService.Services.Categories;

namespace BaobabBackEndService.BusinessLogic
{
    public class CategoryService : ICategoriesService
    {
        private readonly ICategoriesRepository _categoriesRepository;

        public CategoryService(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        public IEnumerable<Category> GetCategories()
        {
            // Lógica de negocio para obtener todas las categorías
            return _categoriesRepository.GetCategories();
        }

        public Category GetCategory(string id)
        {
            return _categoriesRepository.GetCategory(id);
        }
    }
}
