using ListaKontaktow.BusinessLayer.Services;
using ListaKontaktow.DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ListaKontaktow.Controllers
{
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoriesService _categoriesService;

        public CategoryController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        [HttpGet]
        public List<Category> GetAllCategories()
        {
            return _categoriesService.GetAll();
        }

        [HttpGet("{id}")]
        public List<Subcategory> GetALLSubcategoriesByCategoryId(int id)
        {
            return _categoriesService.GetAllSubcategoryByCategoryId(id);
        }
    }
}
