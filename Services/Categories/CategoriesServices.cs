
using BaobabBackEndSerice.Models;
using BaobabBackEndService.Repository.Categories;
using BaobabBackEndService.Repository.Coupons;
using BaobabBackEndService.Utils;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BaobabBackEndService.Services.categories
{
    public class CategoryServices : ICategoriesServices
    {
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly ICouponsRepository _couponsRepository;

        public CategoryServices(ICategoriesRepository categoriesRepository, ICouponsRepository couponsRepository)
        {
            _categoriesRepository = categoriesRepository;
            _couponsRepository = couponsRepository;
        }

        public ResponseUtils<Category> GetAllCategories()
        {
            var result = _categoriesRepository.GetCategories();
            return new ResponseUtils<Category>(false, new List<Category>(result), null, message: "Todo");

        }

        public async Task<ResponseUtils<Category>> GetCategoriesAsync(string number)
        {

            if (!int.TryParse(number, out int parseNumber))
            {
                return new ResponseUtils<Category>(false, message: "Dato ingresado no es valido.");
            }

            try
            {
                if (parseNumber == 1)
                {
                    return new ResponseUtils<Category>(true, new List<Category>(await _categoriesRepository.GetCategoriesAsync("Activo")), message: "Se encontro el status buscado correctamente."); //llamada base de datos
                }

                return new ResponseUtils<Category>(true, new List<Category>(await _categoriesRepository.GetCategoriesAsync("Inactivo")), message: "Se encontro el estatus buscado correctamente.");
            }
            catch (Exception ex)
            {
                return new ResponseUtils<Category>(false, message: "Error buscar el estado de la categoría en la base de datos: " + ex.InnerException.Message);
            }
        }

        public Category GetCategory(string id)
        {
            return _categoriesRepository.GetCategory(id);
        }


        public async Task<ResponseUtils<Category>> UpdateCategory(string id, CategoryRequest category)
        {
            // Validaciones de entrada por id
            if (!int.TryParse(id, out int num) || num <= 0)
            {
                return new ResponseUtils<Category>(false, message: "ID de categoría no válido.");
            }

            // Buscar la categoría en la base de datos
            var result = await _categoriesRepository.GetCategoryByIdAsync(num);
            if (result == null)
            {
                return new ResponseUtils<Category>(false, message: "No se encontró la categoría para actualizar.");
            }
            bool validateCategoryStatusChange = await ValidateCategoryStatusChange(num);
            if (category.Status == "Inactivo" && !validateCategoryStatusChange)
            {
                return new ResponseUtils<Category>(false, message: "La categoria no puede ser desactivada ya que tiene cupones activos asignados");
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

        public async Task<ResponseUtils<Category>> CreateCategoria(Category category)
        {
            var existeName = await _categoriesRepository.GetCategoryByNameAsync(category.CategoryName);

            if (existeName != null)
            {
                return new ResponseUtils<Category>(false, message: "El nombre de la categoria ya existe");

            }


            /* if (string.IsNullOrWhiteSpace(category.CategoryName) || category.CategoryName == null)
            {
                return new ResponseUtils<Category>(false, message: "El nombre de la categoria es un campo obligatorio");

            } */


            //crear categoria
            /* if (category.Status == "Activo" || category.Status == "Inactivo")
            {
            }
            else
            {
                return new ResponseUtils<Category>(false, message: "El estado ingresado no es permitido ");
            } */

            //pasar datos a minuscula
            category.CategoryName = category.CategoryName.ToLower();
            category.Status = category.Status;

            return new ResponseUtils<Category>(true, new List<Category> { _categoriesRepository.CreateCategory(category) }, null, message: "Todo oki");
        }

        // ----------------------- SEARCH ACTION:
        public async Task<ResponseUtils<Category>> SearchCategory(string? category)
        {
            try
            {
                // Se convierte la búsqueda a minúscula para hacerla case-insensitive:
                var loweredCategory = category.ToLower();
                // Se trae la información de la entidad 'Categories':
                var categories = await _categoriesRepository.GetAllCategoriesAsync(loweredCategory);
                // Se confirma que el parámetro 'category' no esté vacío:
                if (!string.IsNullOrEmpty(loweredCategory))
                {
                    // Se confirma si se han encontrado datos que coincidan con el filtrado:
                    if (categories.Any())
                    {
                        // Retorno de la respuesta éxitosa con la estructura de la clase 'ResponseUtils':
                        return new ResponseUtils<Category>(true, new List<Category>(categories), message: "¡Categorías filtradas!");
                    }
                    else
                    {
                        // Retorno de la respuesta fallida con la estructura de la clase 'ResponseUtils':
                        return new ResponseUtils<Category>(false, null, 404, message: "¡Dato no encontrado!");
                    }
                }
                else
                {
                    return new ResponseUtils<Category>(false, null, 400, message: "¡No se han ingresado datos!");
                }
            }
            catch (Exception ex)
            {
                return new ResponseUtils<Category>(false, null, null, $"Error: {ex.Message}");
            }
        }
        // -------------------------------------------------------------------------

        //Validar que la categoria este disponible para cambiar el estado
        public async Task<bool> ValidateCategoryStatusChange(int categoryid)
        {
            var result = await _couponsRepository.SearchCouponsByCategoryAsync(categoryid);
            if (result != null)
            {
                return false;
            }
            return true;
        }
    }
}
