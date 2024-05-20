
using BaobabBackEndSerice.Models;
using BaobabBackEndService.Repository.Categories;
using BaobabBackEndService.Utils;
using Microsoft.EntityFrameworkCore;

namespace BaobabBackEndService.Services.categories
{
    public class CategoryServices : ICategoriesServices
    {
        private readonly ICategoriesRepository _categoriesRepository;

        public CategoryServices(ICategoriesRepository categoriesRepository)
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
        public async Task<ResponseUtils<Category>> UpdateCategory(string id, Category category)
        {
            // Validaciones de entrada por id
            if (!int.TryParse(id, out int num) || num <= 0)
            {
                return new ResponseUtils<Category>(false, message: "ID de categoría no válido.");
            }

            // Validaciones de entrada por campos de cat
            if (category == null)
            {
                return new ResponseUtils<Category>(false, message: "El objeto de categoría es nulo.");
            }

            // Validaciones de entrada por campos que no sean vacios 
            if (string.IsNullOrWhiteSpace(category.CategoryName))
            {
                return new ResponseUtils<Category>(false, message: "El nombre de categoría es requerido.");
            }

            // Validaciones de entrada por campos que no sean vacios 
            if (string.IsNullOrWhiteSpace(category.Status))
            {
                return new ResponseUtils<Category>(false, message: "El estado de la categoría es requerido.");
            }

            // Buscar la categoría en la base de datos
            var result = await _categoriesRepository.GetCategoryByIdAsync(num);
            if (result == null)
            {
                return new ResponseUtils<Category>(false, message: "No se encontró la categoría para actualizar.");
            }

            // Actualizar la categoría
            result.CategoryName = category.CategoryName.ToLower();
            result.Status = category.Status;

            try
            {
                await _categoriesRepository.UpdateCategoryAsync(result);
                return new ResponseUtils<Category>(true, new List<Category> { result }, message: "La categoría se actualizó correctamente.");
            }
            catch (DbUpdateException ex)
            {
                return new ResponseUtils<Category>(false, message: "Error al actualizar la categoría en la base de datos: " + ex.InnerException.Message);
            }
        }

    }
}