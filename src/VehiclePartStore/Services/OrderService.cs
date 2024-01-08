using Newtonsoft.Json;
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
    public class OrderService
    {
        public List<OrderHeaderDto> GetAllOrderHeader()
        {
            using (var context = new LocalContext())
            {
                return context.OrderHeaders.AsEnumerable().Select((x,index) => new OrderHeaderDto 
                {
                    CustomerId = x.CustomerId,
                    Index = index +1,
                    CustomerName = x.CustomerName,
                    InternalOrderNum = x.InternalOrderNum,
                    OrderDate = x.OrderDate,
                    PaymentDate = x.PaymentDate,
                    Description = x.Description
                })
                .ToList();
            }
        }
        public List<OrderHeaderDto> GetById(int InternalOrderNum)
        {
            using (var context = new LocalContext())
            {
                return context.OrderHeaders
                    .Where(x => x.InternalOrderNum == InternalOrderNum)
                    .Select((x, index) => new OrderHeaderDto
                    {
                        Customer = new CustomerDto
                        {
                            CustomerId = x.Customer.CustomerId,
                            CustomerName = x.Customer.CustomerName,
                            CustomerPhone = x.Customer.CustomerPhone,
                            CustomerAddress = x.Customer.CustomerAddress,
                            Description = x.Customer.Description
                        },
                        CustomerId = x.CustomerId,
                        Index = index + 1,
                        CustomerName = x.CustomerName,
                        InternalOrderNum = x.InternalOrderNum,
                        OrderDate = x.OrderDate
                    })
                .ToList();
            }
        }
        public bool CreateOrderHeader(OrderHeaderDto ohd)
        {
            try
            {
                using (var context = new LocalContext())
                {
                    OrderHeader od = new OrderHeader();
                    od.CustomerId = ohd.CustomerId;
                    od.CustomerName = ohd.CustomerName;
                    od.OrderDate = DateTime.Now;
                    od.Description = ohd.Description;
                    context.OrderHeaders.Add(od);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool EditOrderHeader(OrderHeaderDto od)
        {
            using (var context = new LocalContext())
            {
                var data = context.OrderHeaders.Find(od.InternalOrderNum);
                if (data != null)
                {
                    if (!data.PaymentDate.HasValue)
                    {
                        data.PaymentDate = DateTime.Now;
                        context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                  
                }
                else
                {
                    return false;
                }
            }
        }
        public List<OrderDetailDto> GetOrderDetail(int InternalOrderNum)
        {
            using (var context = new LocalContext())
            {
                var data = context.OrderDetails
                    .Where(x => x.InternalOrderNum == InternalOrderNum)                    
                    .Select(x => new OrderDetailDto 
                    {
                        InternalOrderLineNum = x.InternalOrderLineNum,
                        InternalOrderNum = x.InternalOrderNum,
                        CategoryId = x.CategoryId,
                        CategoryName = x.CategoryName,
                        Category = new CategoryDto 
                        {
                            CategoryId = x.Category.CategoryId,
                            CategoryName = x.Category.CategoryName,
                            CategoryDescription = x.Category.CategoryDescription
                        },
                        OrderQty = x.OrderQty,
                        ProductId = x.ProductId,
                        ProductName = x.Product.ProductName,
                        UnitPrice = x.UnitPrice,
                        TotalPrice = x.TotalPrice                        
                    })
                    .ToList();
                return data;
            }
        }
        public bool CreateOrderDetail(OrderDetailDto entity)
        {
            using (var context = new LocalContext())
            {
                try
                {
                    var product = context.Products.Find(entity.ProductId);
                    
                    // Find if in order has been created Product
                    var FoundProductOrder = context.OrderDetails
                        .Where(x => 
                                    x.InternalOrderNum == entity.InternalOrderNum 
                                    && x.ProductId == entity.ProductId 
                                    && x.CategoryId == entity.CategoryId
                                    && x.UnitPrice == product.UnitPrice
                                    )
                        .ToList();
                    // if found. Findout exactly the row for edit 
                    if (FoundProductOrder.Count() > 0)
                    {
                        var entityAdd = context.OrderDetails.Find(FoundProductOrder.FirstOrDefault().InternalOrderLineNum);
                        if (entityAdd != null)
                        {
                            entityAdd.OrderQty += entity.OrderQty;
                            entityAdd.TotalPrice += entity.OrderQty * entityAdd.UnitPrice; 
                        }
                    }
                    else
                    {
                        // Product didn't create. So create the product
                        OrderDetail od = new OrderDetail
                        {
                            CategoryId = entity.CategoryId,
                            CategoryName = entity.CategoryName,
                            OrderQty = entity.OrderQty,
                            ProductId = entity.ProductId,
                            ProductName = entity.ProductName,
                            TotalPrice = entity.TotalPrice,
                            UnitPrice = entity.UnitPrice,
                            InternalOrderNum = entity.InternalOrderNum
                        };
                        context.OrderDetails.Add(od);                        
                    }

                    context.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }            
        }
        public bool EditOrderDetail(OrderDetailDto entity)
        {
            using (var context = new LocalContext())
            {
                var data = context.OrderDetails.Find(entity.InternalOrderLineNum);
                if (data != null)
                {
                    data.OrderQty = entity.OrderQty;
                    data.TotalPrice = data.UnitPrice * entity.OrderQty;
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public bool DeleteOrderDetail(OrderDetailDto entity)
        {
            using (var context = new LocalContext())
            {
                var data = context.OrderDetails.Find(entity.InternalOrderLineNum);
                if (data != null)
                {
                    try
                    {
                        context.OrderDetails.Remove(data);
                        context.SaveChanges();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
