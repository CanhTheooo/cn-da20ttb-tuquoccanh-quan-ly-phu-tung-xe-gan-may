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
    public class CustomerService
    {
        public List<CustomerDto> GetAll()
        {
            using (var context = new LocalContext())
            {
                List<CustomerDto> data = new List<CustomerDto>();

                data = context.Customers.ToList().Select((x,index) => new CustomerDto
                {
                    Index = index+1,
                    CustomerName = x.CustomerName,
                    CustomerId = x.CustomerId,
                    CreatedDateTime = x.CreatedDateTime,
                    CustomerAddress = x.CustomerAddress,
                    CustomerPhone = x.CustomerPhone,
                    Description = x.Description
                }).ToList();
                return data;
            }
        }
        public List<CustomerDto> Get(CommonFilterDto filter)
        {
            using (var context = new LocalContext())
            {
                List<CustomerDto> data = new List<CustomerDto>();

                data = context.Customers
                    .Where(x => x.CustomerId == filter.CustomerId)
                    .Select((x, index) => new CustomerDto
                    {
                        Index = index + 1,
                        CustomerName = x.CustomerName,
                        CustomerId = x.CustomerId,
                        CreatedDateTime = x.CreatedDateTime,
                        CustomerAddress = x.CustomerAddress,
                        CustomerPhone = x.CustomerPhone,
                        Description = x.Description
                    }).ToList();
                return data;
            }
        }
        public bool IsExist(int CustId)
        {
            using (var context = new LocalContext())
            {
                return context.Customers.Any(x => x.CustomerId == CustId);
            }
        }
        public bool CreateCustomer(CustomerDto cust)
        {
            try
            {
                using (var context = new LocalContext())
                {
                    context.Customers.Add(new Customer
                    {
                        CustomerName = cust.CustomerName,
                        CustomerPhone = cust.CustomerPhone,
                        Description = cust.Description,
                        CustomerAddress = cust.CustomerAddress,
                        CreatedDateTime = DateTime.Now
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
        public bool EditCustomer(CustomerDto cust)
        {
            try
            {
                using (var context = new LocalContext())
                {
                    Customer p = context.Customers.Find(cust.CustomerId);

                    p.CustomerName = cust.CustomerName;
                    p.CustomerPhone = cust.CustomerPhone;
                    p.CustomerAddress = cust.CustomerAddress;
                    p.Description = cust.Description;

                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool DeleteCustomer(int CustId)
        {
            try
            {
                using (var context = new LocalContext())
                {
                    Customer p = context.Customers.Find(CustId);
                    context.Customers.Remove(p);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool IsInUse(int CustId)
        {
            using (var context = new LocalContext())
            {                 
                return context.OrderHeaders.Any(x => x.CustomerId == CustId);   
            }            
        }
    }
}
