using ListaKontaktow.DataLayer;
using ListaKontaktow.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ListaKontaktow.BusinessLayer.Services
{
    public interface ICategoriesService
    {
        void CreateCategories();
        void CreateSubcategories();
        List<Category> GetAll();
        List<Subcategory> GetAllSubcategoryByCategoryId(int id);
    }

    public class CategoriesService : ICategoriesService
    {
        private readonly Func<IContactListDbContext> _contactListDbContextFactoryMethod;

        public CategoriesService(Func<IContactListDbContext> contactListDbContextFactoryMethod)
        {
            _contactListDbContextFactoryMethod = contactListDbContextFactoryMethod;
        }

        public List<Category> GetAll()
        {
            using (var context = _contactListDbContextFactoryMethod())
            {
                return context.Categories.ToList();
            }
        }

        public List<Subcategory> GetAllSubcategoryByCategoryId(int id)
        {
            using (var context = _contactListDbContextFactoryMethod())
            {
                return context.Subcategories
                    .Where(sub => sub.CategoryId == id)
                    .ToList();
            }
        }

        public void CreateCategories()
        {
            List<Category> newCategories = new List<Category> {
                new Category { Name = "Służbowy" },
                new Category { Name = "Prywatny" },
                new Category { Name = "Inny" }
            };

            AddCategories(newCategories);
        }

        private void AddCategories(List<Category> newCategories)
        {
            using (var context = _contactListDbContextFactoryMethod())
            {
                var existingCategories = context.Categories.ToList();

                var newCategoriesToAdd = newCategories.Where(item => !existingCategories.Any(c => c.Name == item.Name)).ToList();
                context.Categories.AddRange(newCategoriesToAdd);

                context.SaveChanges();
            }
        }

        public void CreateSubcategories()
        {
            List<Subcategory> newSubcategories = new List<Subcategory> {
                new Subcategory { Name = "Szef", CategoryId = 1 },
                new Subcategory { Name = "Klient", CategoryId = 1 },
                new Subcategory { Name = "Tester", CategoryId = 1 },
                new Subcategory { Name = "Programista", CategoryId = 1 },
                new Subcategory { Name = "HR", CategoryId = 1 },
                new Subcategory { Name = "Dom", CategoryId = 2 },
                new Subcategory { Name = "Żona", CategoryId = 2 },
                new Subcategory { Name = "Mąż", CategoryId = 2 },
                new Subcategory { Name = "Dzieci", CategoryId = 2 },
                new Subcategory { Name = "Inny", CategoryId = 3 }};

            AddSubcategories(newSubcategories);
        }

        private void AddSubcategories(List<Subcategory> newSubcategories)
        {
            using (var context = _contactListDbContextFactoryMethod())
            {
                var existingSubcategories = context.Subcategories.ToList();

                var newSubcategoriesToAdd = newSubcategories.Where(item => !existingSubcategories.Any(c => c.Name == item.Name)).ToList();
                context.Subcategories.AddRange(newSubcategoriesToAdd);

                context.SaveChanges();
            }
        }
    }
}
