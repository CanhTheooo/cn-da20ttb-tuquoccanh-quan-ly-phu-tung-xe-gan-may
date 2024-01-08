using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehiclePartStore.Domain;
using VehiclePartStore.Dto;
using VehiclePartStore.Infrastructure;
using VehiclePartStore.Ultils;

namespace VehiclePartStore.Services
{
    public class ProductService
    {
        public List<ProductDto> GetAll()
        {
            using (var context = new LocalContext())
            {
                List<ProductDto> data = new List<ProductDto>();

                data = context.Products.ToList().Select((x,index) => new ProductDto 
                {
                    Index = index + 1,
                    CategoryId = x.CategoryId,
                    Category = new CategoryDto 
                    {
                        CategoryId = x.Category.CategoryId,
                        CategoryName = x.Category.CategoryName,
                        CategoryDescription = x.Category.CategoryDescription
                    },
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                    UnitPrice = x.UnitPrice
                }).ToList();

                return data;
            }
        }
        public List<ProductDto> Get(CommonFilterDto filter)
        {
            using (var context = new LocalContext())
            {
                List<ProductDto> data = new List<ProductDto>();

                IQueryable<Product> query = context.Products;

                if (filter.CategoryId != -1)
                {
                    query = query.Where(x => x.CategoryId == filter.CategoryId);
                }
                if (filter.ProductId != -1)
                {
                    query = query.Where(x => x.ProductId == filter.ProductId);
                }
                data = query.AsEnumerable()
                  .Select((x, index) => new ProductDto
                  {
                      Index = index + 1,
                      CategoryId = x.CategoryId,
                      ProductId = x.ProductId,
                      ProductName = x.ProductName,
                      UnitPrice = x.UnitPrice
                  })
                  .ToList();
                return data;
            }
        }
        public bool IsProductExist(int ProductId)
        {
            using (var context = new LocalContext())
            {
                return context.Products.Any(x => x.ProductId == ProductId);
            }
        }
        public bool CreateProduct(ProductDto product)
        {
            try
            {
                using (var context = new LocalContext())
                {
                    context.Products.Add(new Product
                    {
                        CategoryId = product.CategoryId,
                        ProductName = product.ProductName,
                        UnitPrice = product.UnitPrice, 
                        
                    });
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex )
            {
                return false;
            }
        }
        public bool EditProduct(ProductDto product)
        {
            try
            {
                using (var context = new LocalContext())
                {
                    Product p = context.Products.Find(product.ProductId);
                    p.ProductName = product.ProductName;
                    p.UnitPrice = product.UnitPrice;
                    p.CategoryId = product.CategoryId;
                    p.UnitPrice = product.UnitPrice;
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public string IsInUse(List<int> ProductId)
        {
            using (var context = new LocalContext())
            {
                var data = context.OrderDetails.Where(x => ProductId.Contains(x.ProductId)).Select(x => x.ProductName).Distinct().ToList();
                if (data.Count > 0)
                {
                    return string.Join("\n", data);
                }
                else
                {
                    return Constants.OK;
                }
            }            
        }
        public bool DeleteProduct(List<int> ProductId)
        {
            try
            {
                using (var context = new LocalContext())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        foreach (var i in ProductId)
                        {
                            Product p = context.Products.Find(i);

                            context.Products.Remove(p);
                        }
                        context.SaveChanges();
                        transaction.Commit();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
