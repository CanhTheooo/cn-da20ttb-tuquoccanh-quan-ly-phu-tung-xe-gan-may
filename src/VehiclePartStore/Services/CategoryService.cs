using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehiclePartStore.Domain;
using VehiclePartStore.Dto;
using VehiclePartStore.Infrastructure;

namespace VehiclePartStore.Services
{
    public class CategoryService
    {
        public List<CategoryDto> GetAll()
        {
            using (var context = new LocalContext())
            {
                List<CategoryDto> data = new List<CategoryDto>();

                data = context.Categories.Select(x => new CategoryDto
                {
                    CategoryId = x.CategoryId,
                    CategoryName = x.CategoryName,
                    CategoryDescription = x.CategoryDescription
                }).ToList();
                return data;
            }
        }
        public List<CategoryDto> Get(CommonFilterDto filter)
        {
            using (var context = new LocalContext())
            {
                List<CategoryDto> data = new List<CategoryDto>();

                data = context.Categories
                    .Where(x => x.CategoryId == filter.CategoryId)
                    .Select(x => new CategoryDto
                    {
                        CategoryId = x.CategoryId,
                        CategoryName = x.CategoryName,
                        CategoryDescription = x.CategoryDescription
                    })
                    .ToList();
                return data;
            }
        }
        public bool IsCategoryExist(int CatId)
        {
            using (var context = new LocalContext())
            {
                return context.Categories.Any(x => x.CategoryId == CatId);
            }
        }
        public bool CreateCategory(CategoryDto cat)
        {
            try
            {
                using (var context = new LocalContext())
                {
                    context.Categories.Add(new Category
                    {
                        CategoryName = cat.CategoryName,
                        CategoryDescription = cat.CategoryDescription
                    });
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool EditCategory(CategoryDto cat)
        {
            try
            {
                using (var context = new LocalContext())
                {
                    Category p = context.Categories.Find(cat.CategoryId);
                    p.CategoryName = cat.CategoryName;
                    p.CategoryDescription = cat.CategoryDescription;
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool DeleteCategory(int CatId)
        {
            try
            {
                using (var context = new LocalContext())
                {
                    Category p = context.Categories.Find(CatId);
                    context.Categories.Remove(p);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool IsInUse(int CatId)
        {
            using (var context = new LocalContext())
            {
                bool isValidate = false;

                var data = context.Categories.Where(x => x.CategoryId == CatId).SelectMany(x => x.Products).ToList();
                if (data.Count() > 0)
                {
                    isValidate = true;
                }
                return isValidate;
            }
             
        }
    }
}
